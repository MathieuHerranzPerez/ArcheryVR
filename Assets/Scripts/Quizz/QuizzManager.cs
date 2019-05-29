using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QuizzManager : MonoBehaviour
{
    [Range(1, 20)]
    [SerializeField]
    private int nbWave = 1;

    [Header("Setup")]
    [SerializeField]
    private GameObject wavePrefab = default;
    [SerializeField]
    private QuizzEditorWrap quizzWrap = default;

    private List<Tuple<Subject,int>> resultTab = new List<Tuple<Subject, int>>();

    // ---- INTERN ----
    private int currentWaveNum = 0;
    private List<List<Quizz>> listAllQuizzScholarLevelAndDifficulty; 

    public void StartSpawningForSchoolAndLevel()
    {
        LoadQuestions();
    }

    // called when loadquestions is finished
    public void continueSpawningForSchoolAndLevel()
    {
        quizzWrap.quizzContainer = SaveJsonSystem.quizzContainer;

        GetQuizzesForScolarLevelAndDifficulty();

        DisplayListQuizzes(); // affD

        SpawnWave();
    }

    public void saveWaveStat(Subject subject, int nbGoodAnswer, int nbWrongAnswer)
    {
        resultTab.Add(new Tuple<Subject,int>(subject, nbGoodAnswer * 100 / (nbGoodAnswer + nbWrongAnswer)));
    }

    public void NotifyWaveEnd()
    {
        double res = 0;

        SendResult(ProfileManager.PROFIL.id, res, 0, 0);
        SendProgression(ProfileManager.PROFIL.id, res, 0, 0);
        
    }

    private void SendResult(int idProfil,double resMath, double resFrancais, double resAnglais)
    {
        // ajoute le resultat dans l'historique des résultats

        Resultat res = new Resultat();

        res.profilId = idProfil;
        res.gradeId = ProfileManager.PROGRESSION.gradeId;
        res.dateResultat = DateTime.Now;
        res.difficulteMaths = ProfileManager.PROGRESSION.difficulteMaths;
        res.resMaths = resMath;
        res.difficulteFrancais = ProfileManager.PROGRESSION.difficulteFrancais;
        res.resFrancais = resFrancais;
        res.difficulteAnglais = ProfileManager.PROGRESSION.difficulteAnglais;
        res.resAnglais = resAnglais;

        string json = JsonUtility.ToJson(res);

        UnityWebRequest wwwRes = UnityWebRequest.Post("https://archeryvr2019.azurewebsites.net/api/ResultatAPI", json); // TODO Mettre vraie URL
    }

    private void SendProgression(int idProfil, double resMath, double resFrancais, double resAnglais)
    {
        // applique bonus en fonction du résultat
        if (resMath >= 80.0)
            ProfileManager.PROGRESSION.xpmaths += 3;
        else if (resMath >= 60.0)
            ProfileManager.PROGRESSION.xpmaths += 1;
        else if (resMath <= 40.0 && ProfileManager.PROGRESSION.xpmaths > 0)
            ProfileManager.PROGRESSION.xpmaths -= 1;

        // change la difficultée si 10 xp
        if (ProfileManager.PROGRESSION.xpmaths >= 10 || ProfileManager.PROGRESSION.difficulteMaths <= 4)
        {
            ProfileManager.PROGRESSION.xpmaths = 0;
            ProfileManager.PROGRESSION.difficulteMaths++;
        }

        string json = JsonUtility.ToJson(res);

        UnityWebRequest www = UnityWebRequest.Put("https://archeryvr2019.azurewebsites.net/api/ProgressionAPI/" + ProfileManager.PROFIL.id, json); 
    }

    private void LoadQuestions()
    {
        StartCoroutine(SaveJsonSystem.LoadQuizzesFromDB(this));
    }

    private void GetQuizzesForScolarLevelAndDifficulty()
    {
        this.listAllQuizzScholarLevelAndDifficulty = new List<List<Quizz>>();

        // create temporary list with all the quizzes for scholar level and difficulty
        List<Quizz> listAllQuizzScholarLevelAndDifficulty = new List<Quizz>();
        // retient les quizzes avec le bon niveau de scolarite et bon niveau dans les matieres
        foreach (Quizz q in quizzWrap.quizzContainer.listQuizz)
        {
            if (q.scholarLevel == (ScholarLevel) ProfileManager.PROGRESSION.gradeId && q.subject == Subject.MATHS && q.difficulty == ProfileManager.PROGRESSION.difficulteMaths) // on a que des questions de maths pour le moment (TODO faire les autres matieres si besoin)
            {
                listAllQuizzScholarLevelAndDifficulty.Add(q);
            }
        }

        foreach (Subject subject in Enum.GetValues(typeof(Subject)))
        {
            List<Quizz> listQuizzS = new List<Quizz>();
            foreach(Quizz q in listAllQuizzScholarLevelAndDifficulty)
            {
                if(q.subject == subject)
                {
                    listQuizzS.Add(q);
                }
            }

            if(listQuizzS.Count > 0)
                this.listAllQuizzScholarLevelAndDifficulty.Add(listQuizzS);
        }
    }

    // affD
    private void DisplayListQuizzes()
    {
        foreach(List<Quizz> lq in listAllQuizzScholarLevelAndDifficulty)
        {
            foreach(Quizz q in lq)
            {
                Debug.Log("quizz : " + q.question);
            }
        }
    }

    private void SpawnWave()
    {
        GameObject waveCloneGO = (GameObject)Instantiate(wavePrefab);
        Wave waveClone = waveCloneGO.GetComponent<Wave>();

        waveClone.SetQuizzFromMananger(GetRandomQuizz(), this);
    }

    private Quizz GetRandomQuizz()
    {
        if (listAllQuizzScholarLevelAndDifficulty.Count == 0)
            throw new Exception("quizz list is empty");

        int indexSubject = UnityEngine.Random.Range(0, listAllQuizzScholarLevelAndDifficulty.Count);
        int indexQuizz = UnityEngine.Random.Range(0, listAllQuizzScholarLevelAndDifficulty[indexSubject].Count);
        Quizz res = listAllQuizzScholarLevelAndDifficulty[indexSubject][indexQuizz];

        listAllQuizzScholarLevelAndDifficulty[indexSubject].Remove(res); // remove it from the list
        if(listAllQuizzScholarLevelAndDifficulty[indexSubject].Count == 0)
        {
            listAllQuizzScholarLevelAndDifficulty.Remove(listAllQuizzScholarLevelAndDifficulty[indexSubject]);
        }

        return res;
    }

}

[Serializable]
public partial class Progression
{
    public int id;
    public int gradeId;
    public int profilId;
    public int difficulteMaths;
    public int xpmaths;
    public int difficulteFrancais;
    public int xpfrancais;
    public int difficulteAnglais;
    public int xpanglais;

}

[Serializable]
public partial class Resultat
{
    public int id;
    public int profilId;
    public int gradeId;
    public DateTime dateResultat;
    public int difficulteMaths;
    public double resMaths;
    public int difficulteFrancais;
    public double resFrancais;
    public int difficulteAnglais;
    public double resAnglais;

}

[Serializable]
public partial class Profil
{
    public int id;
    public int genre;
    public string nom;
    public string couleur;
    public bool estDroitier;
}

[Serializable]
public partial class Grade
{
    public int id;
    public string nom;
}



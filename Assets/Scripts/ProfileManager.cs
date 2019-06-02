using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager Instance { get; private set; }

    public int xpToLevelUp = 6;

    [Header("Don't need to be filled")]
    public Profil profil;
    public Grade grade;
    public Progression progression;

    void Awake()
    {
        Instance = this;
    }

    public IEnumerator LoadProfileInformation(int id, GameManager gm)
    {
        Debug.Log("get profile..."); // affD
        yield return StartCoroutine(LoadProfil(id));
        Debug.Log("get progression..."); // affD
        yield return StartCoroutine(LoadProgression(id));
        Debug.Log("get grade..."); // affD
        yield return StartCoroutine(LoadGrade(progression.gradeId));

        // continue le déroulement du programme une fois les données chargées
        gm.ContinueStart();
    }


    private IEnumerator LoadProfil(int id)
    {

        using (UnityWebRequest webrequest = UnityWebRequest.Get("https://archeryvr.azurewebsites.net/api/ProfilAPI/" + id))
        {
            yield return webrequest.SendWebRequest();

            if (webrequest.isNetworkError)
            {
                Debug.Log("error connection : ");
                Debug.Log(webrequest.error);
            }
            else
            {
                string json = webrequest.downloadHandler.text;
                Debug.Log(json);
                profil = JsonUtility.FromJson<Profil>(json);
            }

        }
    }

    private IEnumerator LoadProgression(int id)
    {

        using (UnityWebRequest webrequest = UnityWebRequest.Get("https://archeryvr.azurewebsites.net/api/ProgressionAPI/" + id))
        {
            yield return webrequest.SendWebRequest();

            if (webrequest.isNetworkError)
            {
                Debug.Log("error connection : ");
                Debug.Log(webrequest.error);
            }
            else
            {
                string json = webrequest.downloadHandler.text;
                progression = JsonUtility.FromJson<Progression>(json);

                if (progression.difficulteMaths == 0)
                    progression.difficulteMaths = 1;
            }

        }
    }

    private IEnumerator LoadGrade(int id)
    {

        using (UnityWebRequest webrequest = UnityWebRequest.Get("https://archeryvr.azurewebsites.net/api/GradeAPI/" + id)) // TODO remettre en Post et le form en param
        {
            yield return webrequest.SendWebRequest();

            if (webrequest.isNetworkError)
            {
                Debug.Log("error connection : ");
                Debug.Log(webrequest.error);
            }
            else
            {
                string json = webrequest.downloadHandler.text;
                grade = JsonUtility.FromJson<Grade>(json);
            }
        }
    }



    public void SaveRes(float res)
    {
        // arrondi a 1 décimale
        res = (float) Math.Round((double) res,1);

        SendResult(res);
        SendProgression(res);
    }

    private void SendResult(float result)
    {
        // ajoute le resultat dans l'historique des résultats

        Resultat res = new Resultat
        {
            profilId = profil.id,
            gradeId = progression.gradeId,
            difficulteMaths = progression.difficulteMaths,
            resMaths = result,
            difficulteFrancais = progression.difficulteFrancais,
            resFrancais = 0,
            difficulteAnglais = progression.difficulteAnglais,
            resAnglais = 0
        };

        string json = JsonUtility.ToJson(res);
        
        UnityWebRequest wwwRes = UnityWebRequest.Put("https://archeryvr.azurewebsites.net/api/ResultatAPI", json);
        wwwRes.SetRequestHeader("Content-Type", "application/json");
        wwwRes.method = "POST";
        wwwRes.SendWebRequest();


    }

    private void SendProgression(float result)
    {
        // applique bonus en fonction du résultat
        if (result >= 80.0)
            progression.xpmaths += 3;
        else if (result >= 60.0)
            progression.xpmaths += 1;
        else
        {
            if (result < 60.0 && result > 40.0)
                progression.xpmaths -= 1;
            else
                progression.xpmaths -= 2;


            if (progression.xpmaths < 0)
                progression.xpmaths = 0;
        }

        // change la difficultée si xpToLevelUp xp
        if (progression.xpmaths >= xpToLevelUp && progression.difficulteMaths <= 4)
        {
            progression.xpmaths = 0;
            progression.difficulteMaths++;
        }

        string json = JsonUtility.ToJson(progression);
        Debug.Log("res send : " + json);
        UnityWebRequest www = UnityWebRequest.Put("https://archeryvr.azurewebsites.net/api/ProgressionAPI/" + profil.id, json);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SendWebRequest();
    }


    private void SendProfile()
    {
        // mets à jour le profil de l'utilisateur

        string json = JsonUtility.ToJson(ProfileManager.Instance.profil);

        UnityWebRequest wwwRes = UnityWebRequest.Put("https://archeryvr.azurewebsites.net/api/ProfilAPI/" + ProfileManager.Instance.profil.id, json);
        wwwRes.SetRequestHeader("Content-Type", "application/json");
        wwwRes.SendWebRequest();

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
    public int profilId;
    public int gradeId;
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
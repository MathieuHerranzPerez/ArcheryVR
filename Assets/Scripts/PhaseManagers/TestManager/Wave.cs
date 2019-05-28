using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField]
    private float minSpeed = 3f;
    [SerializeField]
    private float maxSpeed = 8f;
    [SerializeField]
    private int nbGoodAnswerToGenerate = 8;
    [SerializeField]
    private int nbWrongAnswerToGenerate = 10;

    [Header("Setup")]    
    [SerializeField]
    private GameObject spherePrefab = default;
    [SerializeField]
    private GameObject spawnPointContainer = default;
    [SerializeField]
    private RecapScreen recapScreen = default;
    [SerializeField]
    private GameObject sphereContainer = default;

    [SerializeField]
    private List<QuestionScreen> listTextQuestion = new List<QuestionScreen>();

    // ---- INTERN ----
    private TestPhaseManager testPhaseManager;
    private Multiplication multiplication;
    //private Quizz quizz;

    private string question;
    private string explanation;

    private List<SpawnPoint> listSpawnPoint = new List<SpawnPoint>();
    private int nbGoodAnswer = 0;
    private int nbWrongAnswer = 0;
    private int nbSphereEndPath = 0;
    private int nbRightSphereEndOfPath = 0;


    public void SetQuizzFromMananger(Multiplication multiplication, TestPhaseManager testPhaseManager)
    {
        this.testPhaseManager = testPhaseManager;
        this.multiplication = multiplication;

        Init();

        foreach (QuestionScreen qs in listTextQuestion)
        {
            qs.SetQuestion(question);
            qs.gameObject.SetActive(true);
        }

        StartSpawn();
    }

    /*
    public void SetQuizzFromMananger(Quizz quizz, TestPhaseManager quizzManager)
    {
        this.quizzManager = quizzManager;
        this.quizz = quizz;
        foreach(QuestionScreen qs in listTextQuestion)
        {
            qs.SetQuestion(quizz.question);
            qs.gameObject.SetActive(true);
        }

        StartSpawn();
    }
    */


    public void NotifySphereExplodeWithArrow(Sphere sphere)
    {
        if (sphere.IsCorrect())
        {
            ++nbGoodAnswer;
            // the user has shot all the right answers
            if(nbGoodAnswer == nbGoodAnswerToGenerate)
            {
                FinishWave();
            }
        }
        else
        {
            ++nbWrongAnswer;
        }
    }

    
    public void NotifySphereExplodeEndPath(Sphere sphere)
    {
        if (sphere.IsCorrect())
        {
            ++nbWrongAnswer;
            ++nbRightSphereEndOfPath;
        }
        else
        {
            ++nbSphereEndPath;
        }

        // if there is no sphere in the field (ie all have been destroyed)
        if(nbGoodAnswer + nbWrongAnswer + nbSphereEndPath >= nbGoodAnswerToGenerate + nbWrongAnswerToGenerate)
        {
            FinishWave();
        }
        else if(nbRightSphereEndOfPath + nbGoodAnswer + nbSphereEndPath >= nbGoodAnswerToGenerate + nbWrongAnswerToGenerate)
        {
            FinishWave();
        }
    }

    
    // TODO call with btn
    public void DestroyWave()
    {
        testPhaseManager.NotifyWaveDestroyed();
        Destroy(gameObject);
    }

    /*
    private void StartSpawn()
    {
        Init();

        if (listSpawnPoint.Count < quizz.listBadAnswer.Count + quizz.listAnswer.Count)
            throw new Exception("Not enought spawn points to handle all the answers");

        // tmp list to shuffle the answers
        List<Tuple<string, bool>> listQuizzToSpawn = new List<Tuple<string, bool>>();
        foreach(string str in quizz.listAnswer)
        {
            listQuizzToSpawn.Add(new Tuple<string, bool>(str, true));
        }
        foreach (string str in quizz.listBadAnswer)
        {
            listQuizzToSpawn.Add(new Tuple<string, bool>(str, false));
        }

        Utils.Shuffle(listQuizzToSpawn);

        float speed = quizz.difficulty > 2 ? maxSpeed : minSpeed;
        StartCoroutine(SpawnSpheres(listQuizzToSpawn, speed));
    }
    */
    private void StartSpawn()
    {
        if (listSpawnPoint.Count < nbGoodAnswerToGenerate + nbWrongAnswerToGenerate)
            throw new Exception("Not enought spawn points to handle all the answers");

        // tmp list to shuffle the answers
        List<Tuple<string, bool>> listQuizzToSpawn = new List<Tuple<string, bool>>();

        foreach (int i in GenerateAnswers(true))
        {
            listQuizzToSpawn.Add(new Tuple<string, bool>(i.ToString(), true));
        }

        foreach (int i in GenerateAnswers(false))
        {
            listQuizzToSpawn.Add(new Tuple<string, bool>(i.ToString(), false));
        }

        Utils.Shuffle(listQuizzToSpawn);

        StartCoroutine(SpawnSpheres(listQuizzToSpawn, minSpeed));
    }

    private List<int> GenerateAnswers(bool isCorrectAnswer)
    {
        List<int> listMult = new List<int>();
        for(int i = 0; i <= 12; ++i)
        {
            listMult.Add(i);
        }

        List<int> res = new List<int>();
        
        if (isCorrectAnswer)
        {
            for (int i = 0; i <= nbGoodAnswerToGenerate; ++i)
            {
                int randomIndex = UnityEngine.Random.Range(0, listMult.Count);
                res.Add(listMult[randomIndex] * multiplication.num);

                listMult.RemoveAt(randomIndex);

            }
        }
        else
        {
            listMult.Remove(multiplication.num);
            for (int i = 0; i <= nbWrongAnswerToGenerate; ++i)
            {
                bool isOk = false;
                while (!isOk)
                {
                    int wrongAnswer = listMult[UnityEngine.Random.Range(0, listMult.Count)] * listMult[UnityEngine.Random.Range(0, listMult.Count)];
                    if (wrongAnswer % multiplication.num != 0)
                    {
                        isOk = true;
                        res.Add(wrongAnswer);
                    }
                }
            }
        }

        return res;
    }


    private IEnumerator SpawnSpheres(List<Tuple<string, bool>> listQuizzToSpawn, float speed)
    {
        int nbSphereSpawned = 0;
        while (nbSphereSpawned < listQuizzToSpawn.Count)
        {
            SpawnPoint spawnPoint = listSpawnPoint[UnityEngine.Random.Range(0, listSpawnPoint.Count)];
            listSpawnPoint.Remove(spawnPoint);

            GameObject sphereCloneGO = Instantiate(spherePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation, sphereContainer.transform);
            Sphere sphereClone = sphereCloneGO.GetComponent<Sphere>();
            sphereClone.InitAndStart(this, listQuizzToSpawn[nbSphereSpawned].Item1, listQuizzToSpawn[nbSphereSpawned].Item2, speed, spawnPoint.path);

            ++nbSphereSpawned;
            yield return new WaitForSeconds(0.5f);
        }
    }
    

    private void FinishWave()
    {
        Debug.Log("Wave finished");

        foreach(Transform tSphere in sphereContainer.transform)
        {
            Destroy(tSphere.gameObject);    // todo anim
        }

        // hide the question
        foreach (QuestionScreen qs in listTextQuestion)
        {
            qs.gameObject.SetActive(false);
        }

        // display the stats
        recapScreen.SetValues(this.question, nbGoodAnswer, nbWrongAnswer, multiplication.explanation);
        recapScreen.gameObject.SetActive(true);

        testPhaseManager.NotifyWaveEnd(nbGoodAnswer, nbWrongAnswer);
    }

    
    private void Init()
    {
        question = "Eclate les multiples de " + multiplication.num + " !";


        foreach (Transform t in spawnPointContainer.transform)
        {
            listSpawnPoint.Add(t.GetComponent<SpawnPoint>());
        }
    }
    
}

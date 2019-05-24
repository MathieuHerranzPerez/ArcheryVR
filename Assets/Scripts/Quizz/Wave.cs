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

    [Header("Setup")]    
    [SerializeField]
    private GameObject spherePrefab = default;
    [SerializeField]
    private TargetPointSphere target = default;
    [SerializeField]
    private GameObject spawnPointContainer = default;
    [SerializeField]
    private RecapScreen recapScreen = default;
    [SerializeField]
    private GameObject sphereContainer = default;

    [SerializeField]
    private List<QuestionScreen> listTextQuestion = new List<QuestionScreen>();

    // ---- INTERN ----
    private QuizzManager quizzManager;
    private Quizz quizz;

    private List<SpawnPoint> listSpawnPoint = new List<SpawnPoint>();
    private int nbGoodAnswer = 0;
    private int nbWrongAnswer = 0;
    private int nbSphereEndPath = 0;
    private int nbRightSphereEndOfPath = 0;


    public void SetQuizzFromMananger(Quizz quizz, QuizzManager quizzManager)
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

    public void NotifySphereExplodeWithArrow(Sphere sphere)
    {
        if (sphere.IsCorrect())
        {
            ++nbGoodAnswer;
            // the user has shot all the right answers
            if(nbGoodAnswer == quizz.listAnswer.Count)
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
        if(nbGoodAnswer + nbWrongAnswer + nbSphereEndPath >= quizz.listAnswer.Count + quizz.listBadAnswer.Count)
        {
            FinishWave();
        }
        else if(nbRightSphereEndOfPath + nbGoodAnswer + nbSphereEndPath >= quizz.listAnswer.Count + quizz.listBadAnswer.Count)
        {
            FinishWave();
        }
    }

    // TODO call with btn
    public void DestroyWave()
    {
        quizzManager.NotifyWaveEnd();
        Destroy(gameObject);
    }

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
        recapScreen.SetValues(quizz.question, nbGoodAnswer, nbWrongAnswer, quizz.Explanation);
        recapScreen.gameObject.SetActive(true);

        // TODO Save stats
    }

    private void Init()
    {
        foreach (Transform t in spawnPointContainer.transform)
        {
            listSpawnPoint.Add(t.GetComponent<SpawnPoint>());
        }
    }
}

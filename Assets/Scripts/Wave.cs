using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField]
    private float minSpeed = 1f;
    [SerializeField]
    private float maxSpeed = 5f;

    [Header("Setup")]    
    [SerializeField]
    private GameObject spherePrefab = default;
    [SerializeField]
    private Transform target = default;
    [SerializeField]
    private GameObject SpawnPointContainer;

    // ---- INTERN ----
    private Quizz quizz;

    private List<Transform> listSpawnPoint = new List<Transform>();
    private int nbGoodAnswer = 0;
    private int nbWrongAnswer = 0;


    public void SetQuizz(Quizz quizz)
    {
        this.quizz = quizz;

        StartSpawn();
    }

    public void NotifySphereExplodeWithArrow(Sphere sphere)
    {

    }

    public void NotifySphereExplodeEndPath()
    {

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
        foreach (string str in quizz.listAnswer)
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
            Transform spawnPoint = listSpawnPoint[UnityEngine.Random.Range(0, listSpawnPoint.Count)];
            listSpawnPoint.Remove(spawnPoint);

            GameObject sphereCloneGO = Instantiate(spherePrefab, spawnPoint.position, spawnPoint.rotation, transform);
            Sphere sphereClone = sphereCloneGO.GetComponent<Sphere>();
            sphereClone.InitAndStart(this, listQuizzToSpawn[nbSphereSpawned].Item1, listQuizzToSpawn[nbSphereSpawned].Item2, speed, target);

            ++nbSphereSpawned;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Init()
    {
        foreach (Transform t in SpawnPointContainer.transform)
        {
            listSpawnPoint.Add(t);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private FactoryPhaseManager factoryPhaseManager = default;
    [SerializeField]
    private DamPhaseManager damPhaseManager = default;
    [SerializeField]
    private TestPhaseManager testPhaseManager = default;

    [SerializeField]
    private GameObject player = default;

    // ---- INTERN ----
    private Multiplication multiplication;


    void Start()
    {
        InitForLevel(2);    // todo get level in DB

        StartFirstPhase();
        // StartSecondPhase();
        // StartThirdPhase();
    }

    private void StartFirstPhase()
    {
        PutPlayerInSpawnPoint(factoryPhaseManager);
        factoryPhaseManager.StartWithMultiplicationTable(multiplication);
    }

    private void StartSecondPhase()
    {
        PutPlayerInSpawnPoint(damPhaseManager);
        damPhaseManager.StartWithMultiplicationTable(multiplication);
    }

    private void StartThirdPhase()
    {
        PutPlayerInSpawnPoint(testPhaseManager);
        testPhaseManager.StartWithMultiplicationTable(multiplication);
    }


    private void InitForLevel(int level)
    {
        multiplication = MultiplicationTable.Instance.GetRandomMultiplicationForLevel(level);
        Debug.Log("Multiplication chosen : " + multiplication.num);
    }

    private void PutPlayerInSpawnPoint(PhaseManager phaseManager)
    {
        // move the player to the spawnPoint
        player.transform.position = phaseManager.playerSpawnPoint.position;
        player.transform.rotation = phaseManager.playerSpawnPoint.rotation;
    }
}

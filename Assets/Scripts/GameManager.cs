using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private PhaseManager[] listPhaseManager = new PhaseManager[3];

    [SerializeField]
    private GameObject player = default;

    // ---- INTERN ----
    private Multiplication multiplication;
    private int currentPhaseIndex;


    void Start()
    {
        foreach(PhaseManager pm in listPhaseManager)
        {
            pm.SetGameManager(this);
        }

        InitForLevel(2);    // todo get level in DB

        currentPhaseIndex = 0;
        StartPhase(currentPhaseIndex);
    }

    public void GoToPreviousPhase()
    {
        if (currentPhaseIndex > 0)
        {
            --currentPhaseIndex;
            StartPhase(currentPhaseIndex);
        }
        else
        {
            throw new System.Exception("You try to access a phase that does not exist");
        }
    }

    public void GoToNextPhase()
    {
        if (currentPhaseIndex < listPhaseManager.Length - 1)
        {
            ++currentPhaseIndex;
            StartPhase(currentPhaseIndex);
        }
        else
        {
            throw new System.Exception("You try to access a phase that does not exist");
        }
    }

    private void StartPhase(int phaseIndex)
    {
        PutPlayerInSpawnPoint(listPhaseManager[phaseIndex]);
        listPhaseManager[phaseIndex].StartWithMultiplicationTable(multiplication);
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

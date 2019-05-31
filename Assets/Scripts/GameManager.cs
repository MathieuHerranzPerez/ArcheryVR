using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


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
        // récupere profil + grade + progression
        StartCoroutine(ProfileManager.LoadProfileInformation(1,this));  // todo changer ID to connected player
    }

    public void ContinueStart()
    {
        Debug.Log("profil : " + ProfileManager.PROFIL.nom); // affD
        Debug.Log("progression : " + ProfileManager.PROGRESSION.difficulteMaths + " / " + ProfileManager.PROGRESSION.xpmaths); // affD
        Debug.Log("grade : " + ProfileManager.GRADE.nom); // affD

        foreach (PhaseManager pm in listPhaseManager)
        {
            pm.SetGameManager(this);
        }

        InitForLevel(ProfileManager.PROGRESSION.difficulteMaths);

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

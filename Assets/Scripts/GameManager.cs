using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private PhaseManager[] listPhaseManager = new PhaseManager[3];

    [SerializeField]
    private SceneFader sceneFader = default;

    [SerializeField]
    private GameObject player = default;

    // ---- INTERN ----
    private Multiplication multiplication;
    private int currentPhaseIndex;


    void Start()
    {
        // récupere profil + grade + progression
        // StartCoroutine(ProfileManager.Instance.LoadProfileInformation(1, this));  // todo changer ID to connected player
        if (ProfileManager.Instance.IsInitialized)
            ContinueStart();
        else
            StartCoroutine(ProfileManager.Instance.LoadProfileInformationFromGameManager(1, this));
    }

    public void ContinueStart()
    {
        //Debug.Log("profil : " + ProfileManager.Instance.profil.nom); // affD
        //Debug.Log("progression : " + ProfileManager.Instance.progression.difficulteMaths + " / " + ProfileManager.Instance.progression.xpmaths); // affD
        //Debug.Log("grade : " + ProfileManager.Instance.grade.nom); // affD

        foreach (PhaseManager pm in listPhaseManager)
        {
            pm.SetGameManager(this);
        }

        InitForLevel(ProfileManager.Instance.progression.difficulteMaths);

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

    public void ReturnToMenu()
    {
        sceneFader.FadeTo("MainMenuScene");
    }

    private void StartPhase(int phaseIndex)
    {
        PutPlayerInSpawnPoint(listPhaseManager[phaseIndex]);
        listPhaseManager[phaseIndex].StartWithMultiplicationTable(multiplication);
    }

    private void InitForLevel(int level)
    {
        if(level <= 4)
            multiplication = MultiplicationTable.Instance.GetRandomMultiplicationForLevel(level);
        else    // 5
            multiplication = MultiplicationTable.Instance.GetRandomMultiplication();

        Debug.Log("Multiplication chosen : " + multiplication.num);
    }

    private void PutPlayerInSpawnPoint(PhaseManager phaseManager)
    {
        // move the player to the spawnPoint
        player.transform.position = phaseManager.playerSpawnPoint.position;
        player.transform.rotation = phaseManager.playerSpawnPoint.rotation;
    }
}

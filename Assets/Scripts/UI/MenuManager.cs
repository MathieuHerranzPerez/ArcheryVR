﻿using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [Header("Setup")]
    [SerializeField]
    private GameObject profileSelectionUI = default;
    [SerializeField]
    private ProfileOptionUI profileOptionUI = default;
    [SerializeField]
    private MainMenuUI mainMenuUI = default;

    [SerializeField]
    private SceneFader sceneFader = default;

    void Start()
    {
        Instance = this;

        WeaponManager.Instance.SelectPointer();

        if(ProfileManager.Instance.IsInitialized)
        {
            profileSelectionUI.SetActive(false);
            StartCoroutine(ProfileManager.Instance.LoadProfileInformation(ProfileManager.Instance.profil.id, this));
            mainMenuUI.gameObject.SetActive(true);
        }
        else
        {
            profileSelectionUI.SetActive(true);
        }
    }

    public void DisplayProfileSelection()
    {
        profileOptionUI.Hide();
        mainMenuUI.gameObject.SetActive(false);
        profileSelectionUI.SetActive(true);
    }

    public void ChargeProfile(int idProfile)
    {
        StartCoroutine(ProfileManager.Instance.LoadProfileInformation(idProfile, this));    // need to call NotifyLoaded

        profileSelectionUI.SetActive(false);
    }

    public void NotifyLoaded()
    {
        mainMenuUI.NotifChange();
        mainMenuUI.gameObject.SetActive(true);
    }

    public void DisplayOption()
    {
        profileOptionUI.Display();
    }

    public void Play()
    {
        mainMenuUI.gameObject.SetActive(false);
        sceneFader.FadeTo("SampleScene");
    }

    public void PlayTuto()
    {
        mainMenuUI.gameObject.SetActive(false);
        sceneFader.FadeTo("TutoScene");
    }

    public void LeaveGame()
    {
        Debug.Log("Leaving");
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        Instance = this;
    }

    public void DisplayProfileSelection()
    {
        profileSelectionUI.SetActive(true);
    }

    public void ChargeProfile(string idProfile)
    {
        Profile.Instance.LoadProfile(idProfile);    // need to call NotifyLoaded

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
        // todo
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile : MonoBehaviour
{
    public static Profile Instance { get; private set; }

    public ScholarLevel scholarLevel;
    public Gender gender;

    void Start()
    {
        Instance = this;
    }

    public void LoadProfile(string profileId)
    {
        // todo



        MenuManager.Instance.NotifyLoaded();
    }

    public void SaveProfile()
    {
        // reload questions ?

        // notify to change info ?
    }
}

public enum Gender
{
    GARCON,
    FILLE,
}

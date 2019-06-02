using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileOptionUI : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private GameObject profileOptionCanvasGO = default;

    [SerializeField]
    private Dropdown genderDropdown = default;

    // ---- INTERN ----
    private Gender genderSelected;              // TODO init with BD  

    void Start()
    {
        InitDropdownGender();       // TODO init with BD
    }

    public void Save()
    {
        ProfileManager.Instance.SaveProfile();
    }

    public void Display()
    {
        genderSelected = (Gender)ProfileManager.Instance.profil.genre;
        DropdownGenderChange((int)genderSelected);  // genderDropdown.value = (int) genderSelected;/*(int)ProfileManager.Instance.profil.genre;*/
        profileOptionCanvasGO.SetActive(true);
    }

    public void Hide()
    {
        profileOptionCanvasGO.SetActive(false);
    }

    public void DropdownGenderChange(int index)
    {
        genderSelected = (Gender)index;
    }


    private void InitDropdownGender()
    {
        string[] enumNames = Enum.GetNames(typeof(Gender));
        List<string> listGender = new List<string>(enumNames);

        genderDropdown.ClearOptions();
        genderDropdown.AddOptions(listGender);
    }
}

public enum Gender
{
    Garcon,
    fille,
}

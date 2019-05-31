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
    private Dropdown scholarLevelDropdown = default;
    [SerializeField]
    private Dropdown genderDropdown = default;

    // ---- INTERN ----
    private ScholarLevel scholarLevelSelected;  // TODO init with BD
    private Gender genderSelected;              // TODO init with BD  

    void Start()
    {
        InitDropdownScholarLevel(); // TODO init with BD  
        InitDropdownGender();       // TODO init with BD  
    }

    public void Save()
    {
        Profile.Instance.SaveProfile();
    }

    public void Display()
    {
        scholarLevelDropdown.value = (int)Profile.Instance.scholarLevel;
        genderDropdown.value = (int)Profile.Instance.gender;

        profileOptionCanvasGO.SetActive(true);
    }

    public void Hide()
    {
        profileOptionCanvasGO.SetActive(false);
    }

    public void DropdownScholarLevelChange(int index)
    {
        scholarLevelSelected = (ScholarLevel) index;
    }

    public void DropdownGenderChange(int index)
    {
        genderSelected = (Gender)index;
    }

    private void InitDropdownScholarLevel()
    {
        Debug.Log("Init");
        string[] enumNames = Enum.GetNames(typeof(ScholarLevel));
        List<string> listScholarLevel = new List<string>(enumNames);

        scholarLevelDropdown.ClearOptions();
        scholarLevelDropdown.AddOptions(listScholarLevel);
    }

    private void InitDropdownGender()
    {
        string[] enumNames = Enum.GetNames(typeof(Gender));
        List<string> listGender = new List<string>(enumNames);

        genderDropdown.ClearOptions();
        genderDropdown.AddOptions(listGender);
    }
}

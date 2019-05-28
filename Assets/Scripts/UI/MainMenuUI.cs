using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private Text textScholarLevel = default;
    [SerializeField]
    private Image genderImage = default;

    [Header("Images")]
    [SerializeField]
    private Sprite imageMaleGender = default;
    [SerializeField]
    private Sprite imageFemaleGender = default;


    public void NotifChange()
    {
        textScholarLevel.text = Profile.Instance.scholarLevel.ToString();
        genderImage.sprite = Profile.Instance.gender == Gender.GARCON ? imageMaleGender : imageFemaleGender;
    }

    public void Play()
    {
        MenuManager.Instance.Play();
    }

    public void BackToProfileSelection()
    {
        MenuManager.Instance.DisplayProfileSelection();
    }
}

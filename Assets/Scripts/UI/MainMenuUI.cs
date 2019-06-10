using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private Image genderImage = default;

    [Header("Images")]
    [SerializeField]
    private Sprite imageMaleGender = default;
    [SerializeField]
    private Sprite imageFemaleGender = default;


    public void NotifChange()
    {
        genderImage.sprite = ProfileManager.Instance.profil.genre == 1 ? imageMaleGender : imageFemaleGender;
    }

    public void Play()
    {
        MenuManager.Instance.Play();
    }

    public void BackToProfileSelection()
    {
        MenuManager.Instance.DisplayProfileSelection();
    }

    public void PlayTuto()
    {
        MenuManager.Instance.PlayTuto();
    }
}

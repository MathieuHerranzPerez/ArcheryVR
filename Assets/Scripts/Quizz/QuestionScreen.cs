using UnityEngine;
using UnityEngine.UI;

public class QuestionScreen : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private Text textQuestion = default;

    public void SetQuestion(string question)
    {
        textQuestion.text = question;
    }
}

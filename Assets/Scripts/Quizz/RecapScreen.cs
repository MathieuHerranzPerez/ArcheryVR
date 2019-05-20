using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RecapScreen : MonoBehaviour
{
    [Header("Congratulation sentences")]
    [SerializeField]
    private string allGoodSentence = "Excellent !";
    [SerializeField]
    private Color excellentColor;
    [SerializeField]
    private string almostAllRightSentence = "Presque ! Bien joué !";
    [SerializeField]
    private Color goodColor;
    [SerializeField]
    private string mediumSentence = "Tu peux encore faire mieux !";
    [SerializeField]
    private Color mediumColor;
    [SerializeField]
    private string badSentence = "Il faut encore réviser";
    [SerializeField]
    private Color badColor;

    [Header("Setup")]
    [SerializeField]
    private Text textQuestion = default;
    [SerializeField]
    private Text textNbRightAnswer = default;
    [SerializeField]
    private Text textNbWrongAnswer = default;
    [SerializeField]
    private Text textNbSuccessRate = default;
    [SerializeField]
    private Text textCongratulation = default;
    [SerializeField]
    private Text textTips = default;

    public void SetValues(string question, int nbRightAnswer, int nbWrongAnswer, string tips)
    {
        textQuestion.text = question;
        textNbRightAnswer.text = nbRightAnswer.ToString();
        textNbWrongAnswer.text = nbWrongAnswer.ToString();

        int percentage = nbRightAnswer * 100 / (nbRightAnswer + nbWrongAnswer);
        textNbSuccessRate.text = percentage.ToString();

        textCongratulation.text = GetCongratulationSentence(percentage);

        textTips.text = tips;
    }

    private string GetCongratulationSentence(int percentage)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<color=#");

        if (percentage == 100)
            sb.Append(ColorUtility.ToHtmlStringRGB(excellentColor));
        else if (percentage >= 80)
            sb.Append(ColorUtility.ToHtmlStringRGB(goodColor));
        else if(percentage >= 40)
            sb.Append(ColorUtility.ToHtmlStringRGB(mediumColor));
        else
            sb.Append(ColorUtility.ToHtmlStringRGB(badColor));

        sb.Append(">");

        if (percentage == 100)
            sb.Append(allGoodSentence);
        else if (percentage >= 80)
            sb.Append(almostAllRightSentence);
        else if (percentage >= 40)
            sb.Append(mediumSentence);
        else
            sb.Append(badSentence);

        sb.Append("</color>");

        return sb.ToString();
    }
}

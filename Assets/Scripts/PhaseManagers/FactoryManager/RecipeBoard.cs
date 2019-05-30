using UnityEngine;
using UnityEngine.UI;

public class RecipeBoard : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private Text textNbFeather = default;
    [SerializeField]
    private Text textNbStick = default;
    [SerializeField]
    private Text textNbSpear = default;

    
    public void SetRecipe(int nbFeather, int nbStick, int nbSpear)
    {
        textNbFeather.text = nbFeather.ToString();
        textNbStick.text = nbStick.ToString();
        textNbSpear.text = nbSpear.ToString();
    }
}

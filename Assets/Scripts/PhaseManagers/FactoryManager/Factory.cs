using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    [SerializeField]
    private string tips = "N'oublie pas, multiplier % par 4, c'est comme faire\n% + % + % + %";
    [SerializeField]
    private float timeToReadTips = 10f;

    [Header("Setup")]
    [SerializeField]
    private GameObject[] listIngredientImgGO = new GameObject[3];
    [SerializeField]
    private GameObject canvasTipsGO = default;
    [SerializeField]
    private Text textTips = default;
    [SerializeField]
    private RecipeBoard recipeBoard = default;

    // ---- INTERN ----
    private FactoryPhaseManager factoryPhaseManager;
    private int[] listValues; 
    private int currentIndex = 0;

    public void InitWithValuesNumAndManager(int[] listValues, int num, FactoryPhaseManager factoryPhaseManager)
    {
        currentIndex = 0;
        this.factoryPhaseManager = factoryPhaseManager;

        this.listValues = new int[3];
        // init recipe numbers
        for(int i = 0; i < 3; ++i)
        {
            this.listValues[i] = listValues[i] * num;
        }
        recipeBoard.SetRecipe(listValues[0], listValues[1], listValues[2]);

        tips = tips.Replace("%", num.ToString());
        textTips.text = tips;

        StartGame();
    }


    public void CheckResult(int num)
    {
        if(num == listValues[currentIndex])
        {
            GoToNextStep();
        }
        else
        {
            textTips.text = tips;
            canvasTipsGO.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(HideCanvas());
        }
    }

    private void StartGame()
    {
        foreach(GameObject go in listIngredientImgGO)
        {
            go.SetActive(false);
        }

        listIngredientImgGO[currentIndex].SetActive(true);
    }

    private void GoToNextStep()
    {
        //todo anim
        if (currentIndex == 2)
        {
            factoryPhaseManager.NotifyEnd();
        }
        else
        {
            listIngredientImgGO[currentIndex].SetActive(false);
            ++currentIndex;
            listIngredientImgGO[currentIndex].SetActive(true);
        }
    }

    private IEnumerator HideCanvas()
    {
        float time = 0f;
        while(time < timeToReadTips)
        {
            time += Time.deltaTime;
            yield return null;
        }
        canvasTipsGO.SetActive(false);
    }

    private enum Ingredient
    {
        Plume,
        Baton,
        Pierre,
    }
}

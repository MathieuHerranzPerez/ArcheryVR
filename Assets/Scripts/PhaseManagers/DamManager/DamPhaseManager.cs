using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamPhaseManager : PhaseManager
{
    [Header("Setup")]
    [SerializeField]
    private Dam dam = default;
    [Header("Power text")]
    [SerializeField]
    private GameObject canvasTextGO = default;
    [SerializeField]
    private Text textPowerCounter = default;

    // ---- INTERN ----
    private bool isRunning = false;

    void Update()       // arg
    {
        if (isRunning)
        {
            GameObject arrowGO = ArrowManager.Instance.GetCurrentArrow();
            if (arrowGO != null)
            {
                textPowerCounter.text = arrowGO.GetComponent<Arrow>().power.ToString();
            }
            else
            {
                textPowerCounter.text = "";
            }
        }
    }

    public override void StartWithMultiplicationTable(Multiplication multiplication)
    {
        WeaponManager.Instance.SelectBow();
        lastMultiplication = multiplication;

        isRunning = true;
        canvasTextGO.SetActive(true);

        List<int> listMult = new List<int>();
        for(int i = 0; i < 12; ++i)
        {
            listMult.Add(i+1);
        }

        int[] listUsedValues = new int[dam.GetNbParts()];
        for(int i = 0; i < dam.GetNbParts(); ++i)
        {
            int index = Random.Range(0, listMult.Count);
            listUsedValues[i] = listMult[index] * multiplication.num;

            listMult.RemoveAt(index);
        }

        dam.InitWithValuesAndManager(listUsedValues, this);
    }

    public void NotifyDamDestroyed()
    {
        canvasTextGO.SetActive(false);
        isRunning = false;

        WeaponManager.Instance.SelectPointer();
        endScreenGO.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryPhaseManager : PhaseManager
{
    [Header("Setup")]
    [SerializeField]
    private Factory factory = default;
    [SerializeField]
    private ArrowOrder arrowOrder = default;

    public override void StartWithMultiplicationTable(Multiplication multiplication)
    {
        WeaponManager.Instance.SelectHammer();

        List<int> listMult = new List<int>();
        for (int i = 0; i < 13; ++i)
        {
            listMult.Add(i);
        }

        int[] listUsedValues = new int[3];
        for (int i = 0; i < 3; ++i)
        {
            int index = Random.Range(0, listMult.Count);
            listUsedValues[i] = listMult[index];

            listMult.RemoveAt(index);
        }

        factory.InitWithValuesNumAndManager(listUsedValues, multiplication.num, this);
        arrowOrder.SetOrder(multiplication.num);
    }

    public void NotifyEnd()
    {
        Debug.Log("Phase ended");
        // todo
    }
}

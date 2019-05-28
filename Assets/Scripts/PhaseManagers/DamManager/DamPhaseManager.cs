using System.Collections.Generic;
using UnityEngine;

public class DamPhaseManager : PhaseManager
{
    [Header("Setup")]
    [SerializeField]
    private Dam dam = default;

    public override void StartWithMultiplicationTable(Multiplication multiplication)
    {
        List<int> listMult = new List<int>();
        for(int i = 0; i < 13; ++i)
        {
            listMult.Add(i);
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
        // todo
        Debug.Log("Dam destroyed !");
    }
}

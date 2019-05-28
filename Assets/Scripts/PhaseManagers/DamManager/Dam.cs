using System.Collections.Generic;
using UnityEngine;

public class Dam : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private GameObject damPartContainerGO = default;

    // ---- INTERN ----
    private DamPhaseManager damPhaseManager;
    private List<DamPart> listDamPart = new List<DamPart>();

    void Start()
    {
        Init();
    }

    public void InitWithValuesAndManager(int[] listValues, DamPhaseManager damPhaseManager)
    {
        this.damPhaseManager = damPhaseManager;

        if (listDamPart.Count > listValues.Length)
            throw new System.Exception("Not enought values compared with the number of dam parts");

        int i = 0;
        foreach(DamPart dp in listDamPart)
        {
            dp.InitWithPowerAndDam(listValues[i], this);
            ++i;
        }
    }

    public void NotifyDestruction(DamPart damPart)
    {
        listDamPart.Remove(damPart);
        if(listDamPart.Count == 0)
        {
            damPhaseManager.NotifyDamDestroyed();
        }
    }

    private void Init()
    {
        foreach(Transform t in damPartContainerGO.transform)
        {
            DamPart dp = t.GetComponent<DamPart>();
            listDamPart.Add(dp);
        }
    }

    public int GetNbParts()
    {
        return listDamPart.Count;
    }
}

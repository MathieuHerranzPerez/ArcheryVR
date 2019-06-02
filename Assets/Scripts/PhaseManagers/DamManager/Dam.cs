using System.Collections.Generic;
using UnityEngine;

public class Dam : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private GameObject[] listDamPartPrefab = new GameObject[3];
    [SerializeField]
    private Transform[] listSpawnPointDamPart = new Transform[3];

    // ---- INTERN ----
    private DamPhaseManager damPhaseManager;
    private List<DamPart> listDamPart = new List<DamPart>();

    public void InitWithValuesAndManager(int[] listValues, DamPhaseManager damPhaseManager)
    {
        this.damPhaseManager = damPhaseManager;
        Init();

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
        /*
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }*/

        for (int i = 0; i < listDamPartPrefab.Length; ++i)
        {
            GameObject damPartCloneGO = (GameObject)Instantiate(listDamPartPrefab[i], listSpawnPointDamPart[i].position, listSpawnPointDamPart[i].rotation, transform);

            DamPart dp = damPartCloneGO.GetComponent<DamPart>();
            listDamPart.Add(dp);
        }
    }

    public int GetNbParts()
    {
        return listSpawnPointDamPart.Length;
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerMaster : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private Text textPowerCounter = default;
    [SerializeField]
    private List<TargetNumber> listTargetNumber = new List<TargetNumber>(12);

    void Start()
    {
        Init();
    }

    public void Init()
    {
        foreach(TargetNumber tn in listTargetNumber)
        {
            tn.SetPowerMaster(this);
        }
    }
    /*
    public void NotifyTargetHit(int num, ParticleSystem partcilesPrefab)
    {
        GameObject arrowGO = ArrowManager.Instance.GetCurrentArrow();
        if (arrowGO != null)
        {
            Arrow arrow = arrowGO.GetComponent<Arrow>();
            arrow.MultiplyPower(num, partcilesPrefab);
        }
    }*/
}

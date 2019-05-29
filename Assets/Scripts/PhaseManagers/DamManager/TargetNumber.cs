using UnityEngine;

public class TargetNumber : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private ParticleSystem effectPrefabForArrow = default;
    [SerializeField]
    private int num = default;

    // ---- INTERN ----
    /*private PowerMaster powerMaster;

    public void SetPowerMaster(PowerMaster powerMaster)
    {
        this.powerMaster = powerMaster;
    }
    */

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Arrow")
        {
            Arrow arrow = other.GetComponent<Arrow>();
            arrow.MultiplyPower(num, effectPrefabForArrow);
        }
    }
}

using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    [Header("Setup")]
    [SerializeField]
    private GameObject hammerGO = default;
    [Header("Bow")]
    [SerializeField]
    private GameObject bowGO = default;
    [SerializeField]
    private GameObject quiverGO = default;

    [Header("Pointer")]
    [SerializeField]
    private GameObject pointerGO = default;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectBow()
    {
        ResetWeapon();
        bowGO.SetActive(true);
        quiverGO.SetActive(true);
    }

    public void SelectHammer()
    {
        ResetWeapon();
        hammerGO.SetActive(true);
    }

    public void SelectPointer()
    {
        ResetWeapon();
        pointerGO.SetActive(true);
    }

    private void ResetWeapon()
    {
        bowGO.SetActive(false);
        quiverGO.SetActive(false);

        hammerGO.SetActive(false);

        pointerGO.SetActive(false);
    }
}

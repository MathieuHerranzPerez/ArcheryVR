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

    // todo laser

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

    public void SelectCursor()
    {
        ResetWeapon();
    }

    private void ResetWeapon()
    {
        bowGO.SetActive(false);
        quiverGO.SetActive(false);

        hammerGO.SetActive(false);

        // cursorGO.SetActive(false);
    }
}

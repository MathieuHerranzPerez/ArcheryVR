using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ProfileSelectionBtn : MonoBehaviour
{
    [SerializeField]
    private string userId = "";

    [Header("Setup")]
    [SerializeField]
    private MenuManager menuManager = default;

    // ---- INERN ----
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Select);
    }

    private void Select()
    {
        menuManager.ChargeProfile(userId);
    }
}

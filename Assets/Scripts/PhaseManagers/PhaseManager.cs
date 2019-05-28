using UnityEngine;

public abstract class PhaseManager : MonoBehaviour
{
    public Transform playerSpawnPoint = default;

    // ---- INERN ----
    protected GameManager gameManager;

    public abstract void StartWithMultiplicationTable(Multiplication multiplication);
}

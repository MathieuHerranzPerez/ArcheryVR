using UnityEngine;

public abstract class PhaseManager : MonoBehaviour
{
    public Transform playerSpawnPoint = default;

    [SerializeField]
    protected GameObject endScreenGO = default;

    // ---- INERN ----
    protected GameManager gameManager;
    protected Multiplication lastMultiplication;

    public virtual void StartWithMultiplicationTable(Multiplication multiplication)
    {
        lastMultiplication = multiplication;
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Retry()
    {
        endScreenGO.SetActive(false);
        StartWithMultiplicationTable(lastMultiplication);
    }

    public void GoToNextPhase()
    {
        endScreenGO.SetActive(false);
        gameManager.GoToNextPhase();
    }

    public void GoToPreviousPhase()
    {
        endScreenGO.SetActive(false);
        gameManager.GoToPreviousPhase();
    }
}

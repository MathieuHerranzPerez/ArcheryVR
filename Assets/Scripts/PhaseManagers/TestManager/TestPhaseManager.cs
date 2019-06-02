using UnityEngine;

public class TestPhaseManager : PhaseManager
{
    [Header("Setup")]
    [SerializeField]
    private GameObject wavePrefab = default;

    public override void StartWithMultiplicationTable(Multiplication multiplication)
    {
        WeaponManager.Instance.SelectBow();

        GameObject waveCloneGO = (GameObject)Instantiate(wavePrefab);
        Wave waveClone = waveCloneGO.GetComponent<Wave>();

        waveClone.SetQuizzFromMananger(multiplication, this);
    }

    public void NotifyWaveEnd(int nbGoodAnswer, int nbWrongAnswer)
    {
        // todo save, hide bow and unlock raycast for UI
        Debug.Log("TestPhaseManager : good : " + nbGoodAnswer + ", wrong : " + nbWrongAnswer);
        WeaponManager.Instance.SelectPointer();

        float res = nbGoodAnswer * 100 / (nbGoodAnswer + nbWrongAnswer);

        ProfileManager.Instance.SaveRes(res);
    }

    public void NotifyWaveDestroyed()
    {
        gameManager.ReturnToMenu();
    }
}

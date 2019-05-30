using UnityEngine;
using UnityEngine.UI;

public class BuzzerManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private Factory factory = default;
    [SerializeField]
    private Text textCurrentNumber = default;

    // ---- INTERN ----
    private int currentNumber = 0;
        
    public void NotifyBuzzerHit(int num)
    {
        currentNumber += num;
        textCurrentNumber.text = currentNumber.ToString();
    }

    public void NotifyBuzzerReset()
    {
        currentNumber = 0;
        textCurrentNumber.text = currentNumber.ToString();
    }

    public void NotifyBuzzerTry()
    {
        factory.CheckResult(currentNumber);
        currentNumber = 0;
        textCurrentNumber.text = currentNumber.ToString();
    }
}

using UnityEngine;

public class BuzzerNum : Buzzer
{
    [SerializeField]
    private int num = 1;

    protected override void ActionWhenHit()
    {
        buzzerManager.NotifyBuzzerHit(num);
    }
}

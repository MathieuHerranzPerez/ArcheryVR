using UnityEngine;

public class BuzzerNum : Buzzer
{
    [SerializeField]
    private int num = 1;

    protected override void ActionWhenHit()
    {
        base.ActionWhenHit();
        buzzerManager.NotifyBuzzerHit(num);
    }
}

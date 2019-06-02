
public class BuzzerTry : Buzzer
{
    protected override void ActionWhenHit()
    {
        base.ActionWhenHit();
        buzzerManager.NotifyBuzzerTry();
    }
}

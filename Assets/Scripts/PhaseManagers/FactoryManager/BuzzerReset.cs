
public class BuzzerReset : Buzzer
{
    protected override void ActionWhenHit()
    {
        base.ActionWhenHit();
        buzzerManager.NotifyBuzzerReset();
    }
}

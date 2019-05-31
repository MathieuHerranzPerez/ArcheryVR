
public class BuzzerReset : Buzzer
{
    protected override void ActionWhenHit()
    {
        buzzerManager.NotifyBuzzerReset();
    }
}

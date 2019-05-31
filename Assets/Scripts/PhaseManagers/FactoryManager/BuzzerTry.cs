
public class BuzzerTry : Buzzer
{
    protected override void ActionWhenHit()
    {
        buzzerManager.NotifyBuzzerTry();
    }
}

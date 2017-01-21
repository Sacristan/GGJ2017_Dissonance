
public class MeleeMob : Mob
{
    public override void Awake()
    {
        base.Awake();
        receivedDamageKey = Messages.ReceivedDamageMeleeMob;
        diedKey = Messages.DiedMeleeMob;
    }
}

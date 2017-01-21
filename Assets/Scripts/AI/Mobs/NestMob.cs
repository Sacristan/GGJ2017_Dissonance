public class NestMob : Mob
{
    public override void Awake()
    {
        base.Awake();
        receivedDamageKey = Messages.ReceivedDamageNestMob;
        diedKey = Messages.DiedNestMob;
    }
}

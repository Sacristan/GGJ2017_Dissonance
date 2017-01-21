
public class RangedMob : Mob
{
    public override void Awake()
    {
        base.Awake();
        receivedDamageKey = Messages.ReceivedDamageRangedMob;
        diedKey = Messages.DiedRangedMob;
    }
}

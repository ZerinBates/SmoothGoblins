using UnityEngine;

public class BuffonEncounter : Buff
{
    private int boost;
    private UnitBasic unit;
    private stats stat;
    public BuffonEncounter(stats s=stats.strength,int b=1)
    {
        stat = s;
        trigger = TriggerEvent.Detect;
        SetRounds(2);
        boost = b;
    }

    public override void OnApply(UnitBasic u)
    {
        base.OnApply(u);
        u.GetStats().AddBuff(stat, boost);
        unit = u;
        // Apply the strength boost
        //UnitBasic.GetStats().AddBuff(stats.strength,1);
    }

    public override void OnUpdate(TriggerEvent t, UnitBasic u)
    {
        base.OnUpdate(t, u);

    }

    public override void OnExpire()
    {
        
        unit.GetStats().AddBuff(stat, -1*boost);
        base.OnExpire();
        // Remove the strength boost
        ;
    }
}

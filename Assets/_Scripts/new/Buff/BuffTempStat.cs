using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BuffTempStat:Buff
{
    //Debug.Log("yay");
    // The basic unit to apply the buff to
    //private UnitBasic unit;

    // The amount of bonus durability to apply
    private int bonus;
    private stats stat;

    // Constructor
    public BuffTempStat(UnitBasic daUnit, int daBonus,stats daStat,TriggerEvent daTrigger,int daRounds)
    {
        
        assignedUnit = daUnit;
        SetRounds(daRounds);
        this.bonus = daBonus;
        stat = daStat;
        trigger = daTrigger;
        // OnApply(unit);
    }

    // Called when the buff is applied
    public override void OnApply(UnitBasic u)
    {
        base.OnApply(u);

        // Apply the bonus durability to the unit
        assignedUnit.GetStats().AddBuff(stat, bonus);
    }

    // Called when the buff expires
    public override void OnExpire()
    {
        // Remove the bonus durability from the unit
        assignedUnit.GetStats().AddBuff(stat, -1*bonus);
        base.OnExpire();
    }

    // Called when the buff is updated
    public override void OnUpdate(TriggerEvent t, UnitBasic u)
    {


        base.OnUpdate(t, u);
    }
}

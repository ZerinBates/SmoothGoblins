using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SheildTempBuff : Buff
{
    // The basic unit to apply the buff to
    private UnitBasic unit;

    // The amount of bonus durability to apply
    private int bonusDurability;

    // Constructor
    public SheildTempBuff(UnitBasic unit, int bonusDurability)
    {
        this.unit = unit;
        this.bonusDurability = bonusDurability;
    }

    // Called when the buff is applied
    public override void OnApply(UnitBasic u)
    {
        base.OnApply(u);

        // Apply the bonus durability to the unit
        unit.GetStats().AddBuff(stats.durability, bonusDurability);
    }

    // Called when the buff expires
    public override void OnExpire()
    {
        base.OnExpire();

        // Remove the bonus durability from the unit
        unit.GetStats().AddBuff(stats.durability, -bonusDurability);
    }

    // Called when the buff is updated
    public override void OnUpdate(TriggerEvent t,UnitBasic u)
    {
        base.OnUpdate(t,u);

        // If the trigger is "takedamage", expire the buff
        if (t == TriggerEvent.Damaged)
        {
            Expire();
        }
    }
}

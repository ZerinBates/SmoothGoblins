using System.Collections;
using System.Collections.Generic;


public class BuffDurabilitySpot : BuffTempStat
{


    // Constructor
    public BuffDurabilitySpot(UnitBasic unit, int num) : base(unit, num, stats.durability, TriggerEvent.Move, 1)
    {
        // BuffTempStat
        // OnApply(unit);
    }


}
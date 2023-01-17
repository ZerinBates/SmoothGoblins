using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAreaBuff :Item
{
    // Start is called before the first frame update
    public override void Activate(UnitBasic unit)
    {
        BuffDurabilitySpot b = new BuffDurabilitySpot(unit,1);
        unit.AddBuff(b);
    }


    }

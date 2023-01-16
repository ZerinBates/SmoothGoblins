using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBasic : UnitBasic
{
    private void Start()
    {
        if (!set)
        {
            Stats = new King();
            Stats.faction = Faction.Hero;
            set = true;
            foreach (Trigger t in Stats.genTrigger)
            {
                addTrigger(t);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemiesBasic
{
    private void Start()
    {
        Stats = new Pawn();
        Stats.faction = Faction.Enemy;
        Darken();
        UnitName = "Goblin";
        foreach (Trigger t in Stats.genTrigger)
        {
            addTrigger(t);
        }
        set = true;
    }
}

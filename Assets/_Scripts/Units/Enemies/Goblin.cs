using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemiesBasic
{
    private void Start()
    {
        Stats = new Pawn();
        Darken();
        UnitName = "Goblin";
        foreach (Trigger t in Stats.genTrigger)
        {
            addTrigger(t);
        }
        set = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : HeroBasic
{
    // Start is called before the first frame update
    void Start()
    {
        Stats = new Archer();
       // Darken();
        set = true;
        foreach (Trigger t in Stats.genTrigger)
        {
            addTrigger(t);
        }
        UnitName = "Ranger";
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEncounter :Item
{
   [SerializeField] public Encounter encounter;

    bool end = true;
    public override void Activate(UnitBasic unit)
    {
        if (encounter != null)
        {
            encounter.Activate();
        } 
    }

        // Debug.Log("yay");
    public override void UpdateCheck(){
        //CheckForOccupiedUnits();
       
        if (end)
        {
            if (CheckForOccupiedUnits())
            {
                end = false;
            }
        }
    }
}


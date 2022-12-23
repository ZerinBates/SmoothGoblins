using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shade : EnemiesBasic
{
    private void Start()
    {
        Stats = new Horse();
        Darken();
        set = true;
        UnitName = "Shade";
    }
}

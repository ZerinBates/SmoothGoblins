using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBasic : MonoBehaviour
{
    public string name;
    public Tile OccupiedTile;
    public void setTile(Tile t)
    {
        OccupiedTile = t;
    }
    public virtual void SteppedOn (UnitBasic u)
    {

    }
}


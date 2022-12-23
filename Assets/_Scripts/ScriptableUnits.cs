using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Unit", menuName = "Scriptable Unit")]
public class ScriptableUnits : ScriptableObject
{
    public Faction faction;
    public UnitBasic UnitPrefab;
}
public enum Faction
{
    Hero =0,
    Enemy = 1,
    Neutral =2
}
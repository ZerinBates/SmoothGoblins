using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public List<UnitBasic> all;
    public static TriggerManager Instance;
    private void Awake()
    {
        Instance = this;
        all = new List<UnitBasic>();
    }
    public void UpdateTrigger(TriggerEvent t,UnitBasic unit = null)
    {
        foreach(UnitBasic u in all)
        {
            u.trigger(t, unit);
        }

    }
    public void setAll(List<UnitBasic> u)
    {
        all = u;
    }
}
public enum TriggerEvent
{
    Move,
    Attack,
    Damaged,
    TurnStart,
    TurnEnd,
    Detect

}


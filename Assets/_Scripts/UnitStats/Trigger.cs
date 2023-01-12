using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger 
{
    public string getTriggerName() { return TriggerName; }
    protected string TriggerName;
    TriggerEvent activation;
    protected UnitBasic Triggerer;
    public void setActivation(TriggerEvent active) {activation = active;}
    public UnitBasic GetTriggerer() { return Triggerer; }
    public TriggerEvent getTriggerEvent(){ return activation;}
    public virtual bool activate(UnitBasic triggerUnit)
    {
        Debug.Log(Triggerer);
        return true;
    }
    public void setTriggerer(UnitBasic trigger)
    {
        Triggerer = trigger;
    }
}
/*
public class StrengthenTrigger : Trigger
{
    AttackStyle style;
    int buff = 1;
  //  DefenceBuff buffTrigger;
    public StrengthenTrigger(AttackStyle s, int daBuff = 1)
    {
        setActivation(TriggerEvent.Damaged);
        style = s;
        buff = daBuff;
 //       buffTrigger = new DefenceBuff();
        TriggerName = "DeffenceTrigger";
    }
    public override bool activate(UnitBasic triggerUnit)
    {
        Tile main = GetTriggerer().OccupiedTile;
        Tile triggerTile = triggerUnit.OccupiedTile;
        if (CombatManager.Instance.AttackRange(style, main.x, main.y, triggerTile.x, triggerTile.y))
        {
            if (triggerUnit.OccupiedTile != GetTriggerer().OccupiedTile)
            {
                if (triggerUnit.Faction == GetTriggerer().Faction)
                {
                    triggerUnit.GetStats().DefenceTemp += 1;
                }
            }
        }
        return true;
    }
}*/
public class SwarmTrigger : Trigger
{
    AttackStyle style;
    int buff = 1;
    // DefenceBuff buffTrigger;
    public SwarmTrigger(AttackStyle s, int daBuff = 1)
    {
        setActivation(TriggerEvent.Attack);
        style = s;
        buff = daBuff;
        //  buffTrigger = new DefenceBuff();
        TriggerName = "SwarmTrigger";
    }
    public override bool activate(UnitBasic triggerUnit)
    {
        Tile main = GetTriggerer().OccupiedTile;
        Tile triggerTile = triggerUnit.OccupiedTile;
        if (CombatManager.Instance.AttackRange(style, main.x, main.y, triggerTile.x, triggerTile.y))
        {
            if (triggerUnit.OccupiedTile != GetTriggerer().OccupiedTile)
            {
                if (triggerUnit.Faction == GetTriggerer().Faction)
                {
                    //triggerUnit.GetStats().AttackTemp += 1;
                }
            }
        }
        return true;
    }
}
public class SheilderTrigger : Trigger
{
    AttackStyle style;
    int buff = 1;
   // DefenceBuff buffTrigger;
   public SheilderTrigger (AttackStyle s,int daBuff=1)
    {
        setActivation(TriggerEvent.Damaged);
        style = s;
        buff = daBuff;
      //  buffTrigger = new DefenceBuff();
        TriggerName = "DeffenceTrigger";
    }
    public override bool activate(UnitBasic triggerUnit)
    {
        Tile main = GetTriggerer().OccupiedTile;
        Tile triggerTile=triggerUnit.OccupiedTile;
        if (CombatManager.Instance.AttackRange(style, main.x, main.y, triggerTile.x, triggerTile.y))
        {
            if (triggerUnit.OccupiedTile != GetTriggerer().OccupiedTile)
            {
                if (triggerUnit.Faction == GetTriggerer().Faction)
                {
                  //  triggerUnit.GetStats().DefenceTemp += 1;
                }
            }
        }
        return true;
    }
}
public class SniperTrigger : Trigger
{
    AttackStyle style;
    int buff = 1;
    // DefenceBuff buffTrigger;
    public SniperTrigger(AttackStyle s, int daBuff = 1)
    {
        setActivation(TriggerEvent.Damaged);
        style = s;
        buff = daBuff;
        //  buffTrigger = new DefenceBuff();
        TriggerName = "SniperTrigger";
    }
    public override bool activate(UnitBasic triggerUnit)
    {
        Tile main = GetTriggerer().OccupiedTile;
        Tile triggerTile = triggerUnit.OccupiedTile;
        if (CombatManager.Instance.AttackRange(style, main.x, main.y, triggerTile.x, triggerTile.y,6))
        {
            if (triggerUnit.OccupiedTile != GetTriggerer().OccupiedTile)
            {
                if (triggerUnit.Faction != GetTriggerer().Faction)
                {
                    int x1 = triggerUnit.OccupiedTile.x;
                    int y1 = triggerUnit.OccupiedTile.y;
                    if (UnitManager.Instance.SelectedHero == GetTriggerer())
                    {
                        for (int x = -1; x < 1;x++)
                    {
                        for (int y = -1; y < 1; y++)
                        {
                            if (GridManager.Instance.GetTileAtPosition(x + x1, y + y1) != null && GridManager.Instance.GetTileAtPosition(x + x1, y + y1).OccupiedUnit != null)
                            {
                                if (GridManager.Instance.GetTileAtPosition(x + x1, y + y1).OccupiedUnit.Faction == GetTriggerer().Faction)
                                {

                                 //       triggerUnit.GetStats().DefenceTemp--;
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
        return true;
    }
}
/*
public class DefenceBuff:Trigger
{
    public int count = 1;
    public int amount = 1;
   // public UnitBasic unit;
    public DefenceBuff( int daBuff = 1, int daCount =1)
    {
        setActivation(TriggerEvent.TurnEnd);
        count = daCount;
        amount = daBuff;
        TriggerName = "DefenceBuff";
       // unit = daUnit;
    }
    public override bool activate(UnitBasic triggerUnit)
    {

            count--;
            if (count >= 0)
            {
                Triggerer.GetStats().Defence -= amount;
            }
        
        return true;
    }
}*/
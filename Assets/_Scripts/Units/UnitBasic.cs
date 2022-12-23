using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitBasic : MonoBehaviour
{
    [SerializeField] private GameObject _dark;
    public Tile OccupiedTile;
    public Faction Faction;
    public bool darkened = false;
    protected StatBasic Stats=new StatBasic();
    private List<Trigger> triggers=new List<Trigger>();
    public string UnitName;
    public bool set = false;
    public Trigger getTrigger(string name)
    {
        foreach (Trigger t in triggers)
        {
            if (t.getTriggerName() == name)
            {
                return t;
            }
        }
        return null;
    }
    public bool TriggerContains(Trigger daTrigger)
    {
        string name = daTrigger.getTriggerName();
        foreach (Trigger t in triggers)
        {
            if (t.getTriggerName() == name)
            {
                return true;
            }
        }
        return false;
    }
    public void addTrigger(Trigger daTrigger)
    {
        if (!triggers.Contains(daTrigger))
        {
            daTrigger.setTriggerer(this);
            triggers.Add(daTrigger);

        }
    }
    public void removeTrigger(Trigger daTrigger)
    {
        triggers.Remove(daTrigger);
    }
    public void trigger(TriggerEvent ev, UnitBasic uni)
    {
        foreach (Trigger t in triggers){
            if (triggers.Contains(t))
            {
                if (t.getTriggerEvent() == ev)
                {
                    bool i =t.activate(uni);
                    if (i)
                    {
                        //string j = "yay";
                    }
                }
            }
        }
    }
    public StatBasic GetStats()
    {
        return Stats;
    }
    public double CanHit(Tile spot, Tile enemy)
    {
        int x1 = spot.x;
        int y1 = spot.y;
        int x2 = OccupiedTile.x;
        int y2 = OccupiedTile.y;
        List<Tile> temp = new List<Tile>() { spot };
        double distance = 100000;
        if (CombatManager.Instance.AttackRange(Stats.AttackType, spot.x, spot.y, enemy.x, enemy.y, Stats.Range))
        {
            if (GridManager.Instance.WithinRange(temp, Stats.Range, enemy.x, enemy.y))
            {
                double maxDistance = Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));

                distance = maxDistance;
            }
        }
        return distance;
    }
    public bool Detects(Tile main, Tile enemy)
    {
        if (CombatManager.Instance.AttackRange(Stats.AttackType, main.x, main.y, enemy.x, enemy.y, Stats.vision))
        {
            if (GridManager.Instance.WithinRange(main.x, main.y, Stats.vision, enemy.x, enemy.y))
            {
                return true;
            }
        }
        return false;
    }
    public bool Threatens(Tile main, Tile enemy)
    {
        if (CombatManager.Instance.AttackRange(Stats.AttackType, main.x, main.y, enemy.x, enemy.y, Stats.Range))
        {
            return true;
        }
        return false;
    }
    public bool Attack(UnitBasic enemy)
    {
        bool success = false;
        List<Tile> temp = new List<Tile>() { this.OccupiedTile };
        if (Threatens(OccupiedTile, enemy.OccupiedTile))
        {

            if (GridManager.Instance.WithinRange(temp, Stats.Range, enemy.OccupiedTile.x, enemy.OccupiedTile.y))
            {
                TriggerManager.Instance.UpdateTrigger(TriggerEvent.Attack, this);
                enemy.TakeDamage(Stats.GetAttack());
                success = true;
//                GameManager.Instance.formationmoving = false;
            }
        }
        return success;
    }

    public void TakeDamage(int dmg, string type = "basic")
    {
        TriggerManager.Instance.UpdateTrigger(TriggerEvent.Damaged, this);
        Stats.TakeDamage(dmg, type); 
        if (Stats.Hp <= 0)
        {
            UnitManager.Instance.Death(this);
            gameObject.SetActive(false);
            Destroy(this);
        }
    }
    public void Light()
    {
        if (Stats.Hp > 0)
        {
            gameObject.SetActive(true);
            darkened = false;
        }
        //_dark.SetActive(false);
    }
    public void Darken()
    {
        gameObject.SetActive(false);
        darkened = true;
        //_dark.SetActive(true);
    }

}

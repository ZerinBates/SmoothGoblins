using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBasic 
{
    public List<Trigger> genTrigger { get; set; }=new List<Trigger>();
    public List<AttackStyle> AttackType = new List<AttackStyle>() { AttackStyle.Bishop };
    public int Hp{get; set;}=8;
    public int  Range { get; set; } = 1;
    public int  Speed { get; set; } = 1;
    public int  Attack { get; set; } = 3;
    public int  vision { get; set; } = 20;
    public int  Defence { get; set; } = 1;
    public int HpTemp { get; set; } = 0;
    public int RangeTemp { get; set; } = 0;
    public int SpeedTemp { get; set; } = 0;
    public int AttackTemp { get; set; } = 0;
    public int visionTemp { get; set; } = 0;
    public int DefenceTemp { get; set; } = 0;
    public void setDefenceTemp(int num)
    {
        DefenceTemp = num;
    }
    public int TakeDamage(int dmg , string type)
    {
        if (DefenceTemp < 0)
        {
            Debug.Log(DefenceTemp);
        }
        int total = dmg;
        total = total - Defence;
        if (total <= 0)
        {
            total = 1;
        }
        total = total - DefenceTemp;
        if (total < 0)
        {
            total = 0;
        }
        Hp -= total;
        DefenceTemp = 0;
        return total;
    }
    public int GetAttack()
    {
        int total = Attack + AttackTemp;
        AttackTemp = 0;
        return total;
    }
}
public class Pawn : StatBasic
{
    
     public Pawn()
    {
        SwarmTrigger trigger=new SwarmTrigger(AttackStyle.all);
        genTrigger.Add(trigger);
        AttackType = new List<AttackStyle>() { AttackStyle.all };
        Hp = 2;
        Range = 1;
        Speed = 1;
        Attack = 2;
        vision = 5;
        Defence = 1;
    }
}
public class Horse : StatBasic
{

    public Horse()
    {
       // SheilderTrigger trigger = new SheilderTrigger(AttackStyle.all);
        //genTrigger.Add(trigger);
        AttackType = new List<AttackStyle>() { AttackStyle.all };
        Hp = 3;
        Range = 3;
        Speed = 3;
        Attack = 1;
        vision = 6;
        Defence = 0;
    }
}
public class King : StatBasic
{

    public King()
    {
        SheilderTrigger trigger = new SheilderTrigger(AttackStyle.Castle);
        genTrigger.Add(trigger);
        AttackType = new List<AttackStyle>() { AttackStyle.all };
        Hp = 20;
        Range = 1;
        Speed = 3;
        Attack = 2;
        vision = 6;
        Defence = 1;
    }
}
public class Archer : StatBasic
{

    public Archer()
    {
        SniperTrigger trigger = new SniperTrigger(AttackStyle.all);
        genTrigger.Add(trigger);
        AttackType = new List<AttackStyle>() { AttackStyle.all };
        Hp = 3;
        Range = 3;
        Speed = 1;
        Attack = 1;
        vision = 6;
        Defence = 0;
    }
}
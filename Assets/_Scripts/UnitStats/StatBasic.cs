using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatBasic
{
    public int Strength { get; set; } = 1;
    public int Speed { get; set; } = 1;
    public int Durability { get; set; } = 1;
    public int Knowledge { get; set; } = 1;
    public int Heart { get; set; } = 1;
    public int Keen { get; set; } = 1;

    // Dictionary to store temporary buff values for each stat
    private Dictionary<stats, int> Buffs = new Dictionary<stats, int>();
    //add buff function

    public void AddBuff(stats stat, int value)
    {
        if (Buffs.ContainsKey(stat))
        {
            Buffs[stat] += value;
        }
        else
        {
            Buffs.Add(stat, value);
        }
    }
    //clear buff function
    public void ClearBuff(stats stat)
    {
        Buffs[stat] = 0;
    }
    // skill check
    public int SkillCheck(stats stat)
    {
        int total = 0;
        int statValue = 0;
        int buffValue = 0;
        switch (stat)
        {
            case stats.strength:
                statValue = Strength;
                buffValue = Buffs.ContainsKey(stat) ? Buffs[stat] : 0;
                break;
            case stats.speed:
                statValue = Speed;
                buffValue = Buffs.ContainsKey(stat) ? Buffs[stat] : 0;
                break;
            case stats.durability:
                statValue = Durability;
                buffValue = Buffs.ContainsKey(stat) ? Buffs[stat] : 0;
                break;
            case stats.knowledge:
                statValue = Knowledge;
                buffValue = Buffs.ContainsKey(stat) ? Buffs[stat] : 0;
                break;
            case stats.heart:
                statValue = Heart;
                buffValue = Buffs.ContainsKey(stat) ? Buffs[stat] : 0;
                break;
            case stats.keen:
                statValue = Keen;
                buffValue = Buffs.ContainsKey(stat) ? Buffs[stat] : 0;
                break;
        }
        for (int i = 0; i < statValue + buffValue; i++)
        {
            total += Random.Range(1, 6);
        }
        return total;
    }

    //OLD !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public List<Trigger> genTrigger { get; set; } = new List<Trigger>();
    public List<AttackStyle> AttackType = new List<AttackStyle>() { AttackStyle.Bishop };
    public int Hp { get; set; } = 8;
    public int Range { get; set; } = 1;
    //    public int  Speed { get; set; } = 1;
    public stats Attack { get; set; } = stats.strength;
    public int vision { get; set; } = 20;
    // public int  Defence { get; set; } = 1;
    public int HpTemp { get; set; } = 0;
    public int RangeTemp { get; set; } = 0;
    public int SpeedTemp { get; set; } = 0;
    //    public int AttackTemp { get; set; } = 0;
    public int visionTemp { get; set; } = 0;
    //public int DefenceTemp { get; set; } = 0;
    public int BasicAttack()
    {
        return SkillCheck(Attack);
        // return 1;
    }
    public int TakeDamage(int dmg, string type, bool peircing = false)
    {
        int total = dmg;
        if (!peircing)
        {
            total -= SkillCheck(stats.durability);
        }
        if (total < 0)
        {
            total = 0;
        }
        Hp -= total;
        return total;
    }
    public int GetAttack(stats stat = stats.strength)
    {
        int total = SkillCheck(stat);
        ClearBuff(Attack);
        return total;
    }
}
public enum stats
{
    strength,
    durability,
    speed,
    knowledge,
    heart,
    keen,

}
public class Pawn : StatBasic
{

    public Pawn()
    {
        SwarmTrigger trigger = new SwarmTrigger(AttackStyle.all);
        genTrigger.Add(trigger);
        AttackType = new List<AttackStyle>() { AttackStyle.all };
        Hp = 2;
        Range = 1;
        Speed = 1;
        //Attack = 2;
        vision = 5;
        //  Defence = 1;
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
        //Attack = 1;
        vision = 6;
        // Defence = 0;
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
        //Attack = 2;
        vision = 6;
        //    Defence = 1;
    }
}
public class Archer : StatBasic
{

    public Archer()
    {
        //   SniperTrigger trigger = new SniperTrigger(AttackStyle.all);
        //   genTrigger.Add(trigger);
        AttackType = new List<AttackStyle>() { AttackStyle.all };
        Hp = 3;
        Range = 3;
        Speed = 1;
        //Attack = 1;
        vision = 6;
        //   Defence = 0;
    }
}
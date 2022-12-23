using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class CombatManager : MonoBehaviour
    {
        public static CombatManager Instance;
        private void Awake()
        {
            Instance = this;
        }
    public bool AttackRange(List<AttackStyle> attack, int x1, int y1, int x2, int y2, int range = 1)
    {
        bool success = false;
        foreach(AttackStyle a in attack)
        {
            if (AttackRange(a, x1, y1, x2, y2, range)){

                    success = true;

            }
        }
        return success;
    }
        public bool AttackRange(AttackStyle attack, int x1, int y1, int x2, int y2,int range = 1)
    {
        bool success = false;

        switch (attack)
        {
            case AttackStyle.Queen:
                success =queen(x1, y1, x2, y2,range);
                break;
            case AttackStyle.Bishop:
                success = bishop(x1, y1, x2, y2, range);
                break;
            case AttackStyle.Castle:
                success = castle(x1, y1, x2, y2, range);
                break;
            case AttackStyle.Knight:
               success = knight(x1, y1, x2, y2, range);
                break;
            case AttackStyle.all:
                success = all(x1, y1, x2, y2, range);
                break;
        }
        return success;
    }
    public bool adjacent(int x1, int y1, int x2, int y2)
    {
        return queen(x1, y1, x2, y2);
    }
        public bool queen(int x1, int y1, int x2, int y2,int range=1)
    {
            bool hit = false;
            if(castle (x1,y1,x2,y2,range)||bishop(x1, y1, x2, y2,range))
        {
            hit = true;
        }
            return hit;
        }
    public bool castle(int x1, int y1, int x2, int y2,int range=1)
    {
        bool hit = false;
        if (Mathf.Abs(x1 - x2) <= range && y1 - y2 == 0)
        {
            hit = true;
        }
        if (Mathf.Abs(y1 - y2) <= range && x1 - x2 == 0)
        {
            hit = true;
        }
        return hit;
    }
    public bool bishop(int x1, int y1, int x2, int y2,int range = 1)
    {
        bool hit = false;
        if (x1-y1 == x2-y2|| x1 + y1 == x2 + y2)
        {
            hit = true;
        }
        return hit;
    }
    public bool knight(int x1, int y1, int x2, int y2, int range = 1)
    {
        range = 1;
        bool hit = false;
        if (Mathf.Abs(x1 - x2) == 1&& Mathf.Abs(y1 - y2) == 2)
        {
            hit = true;
        }
        if (Mathf.Abs(x2 - x1) == 2 && Mathf.Abs(y2 - y1) ==1)
        {
            hit = true;
        }

        return hit;
    }
    public bool all (int x1, int y1, int x2, int y2, int range = 2)
    {
        double distance = System.Math.Sqrt(System.Math.Pow(x1 - x2, 2) + System.Math.Pow(y1 - y2, 2));
        if (distance < range+1)
        {
            return true;
        }
        return false;
    }
}
public enum AttackStyle
{
    Queen = 0,
    Bishop = 1,
    Castle = 2,
    Knight =3,
    all=4
}

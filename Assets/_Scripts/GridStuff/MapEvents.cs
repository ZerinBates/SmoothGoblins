using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEvents
{
    public Dictionary<Vector2, Tile> eventMap { get; set; }
    public Dictionary<Vector2, string> eventUnit { get; set; }
    //x,y are the bottom right points
    public bool requirements(int x, int y)
    {
        if (getBounds(x, y))
        {
            
            return true;
        }
        return false;
    }
    bool getBounds(int x,int y)
    {
        if (GridManager.Instance.GetWidth() > eventSize + x && GridManager.Instance.GetHeight() > eventSize + y && 0 <= x && 0 <= y)
        {
            return true;
        }
        return false;
    }
    int eventSize = 0;
    public void setEventSize(int num)
    { eventSize = num;}
    public void startSeed(int x, int y)
    {
        for (int i=x;i<x+eventSize;i++)
        {
            for(int j = y; j < y+eventSize; j++)
            {
                Tile t = eventMap[new Vector2(i-x, j -y)];

                GridManager.Instance.SetTile(i, j,t);
            }
        }
        foreach(KeyValuePair<Vector2, string> u in eventUnit)
        {
            UnitManager.Instance.SpawnUnit(u.Value,(int) u.Key.x+x,(int) u.Key.y+y);
        }
    } 

//requirements- is the entrance to close to exit is this tile overwritable etc return true or false on true get tile formatation
//reseed the grid formation with new tiles 
//add other objects 

}
public class GoblinNook : MapEvents
{
   public GoblinNook()
    {
        setEventSize(3);
        eventMap = new Dictionary<Vector2, Tile>();
        eventUnit = new Dictionary<Vector2, string>();
        int angle = Random.Range(1, 4);
        for (int x = 0;x < 3;x++)
        {
            for (int y = 0; y < 3; y++)
            {

                if (angle == 3)
                {
                    if (x == 0 || x == 2 || y == 2)
                    {
                        eventMap.Add(new Vector2(x, y), GridManager.Instance._mountainTile);
                    }
                    else
                    {
                        eventMap.Add(new Vector2(x, y), GridManager.Instance._grassTile);
                        // UnitBasic u = new Goblin();
                        eventUnit.Add(new Vector2(x, y), "goblin");
                    }
                }else if (angle == 1)
                {
                    if (x == 0 || x == 2 || y == 0)
                    {
                        eventMap.Add(new Vector2(x, y), GridManager.Instance._mountainTile);
                    }
                    else
                    {
                        eventMap.Add(new Vector2(x, y), GridManager.Instance._grassTile);
                        // UnitBasic u = new Goblin();
                        eventUnit.Add(new Vector2(x, y), "goblin");
                    }
                }
                else if (angle == 2)
                {
                    if (y == 0 || y == 2 || x == 0)
                    {
                        eventMap.Add(new Vector2(x, y), GridManager.Instance._mountainTile);
                    }
                    else
                    {
                        eventMap.Add(new Vector2(x, y), GridManager.Instance._grassTile);
                        // UnitBasic u = new Goblin();
                        eventUnit.Add(new Vector2(x, y), "goblin");
                    }
                }
                else
                {
                    if (y == 0 || y == 2 || x == 2)
                    {
                        eventMap.Add(new Vector2(x, y), GridManager.Instance._mountainTile);
                    }
                    else
                    {
                        eventMap.Add(new Vector2(x, y), GridManager.Instance._grassTile);
                        // UnitBasic u = new Goblin();
                        eventUnit.Add(new Vector2(x, y), "goblin");
                    }
                }

            }
        }
    }
}

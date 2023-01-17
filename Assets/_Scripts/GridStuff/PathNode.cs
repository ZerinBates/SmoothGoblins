using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathNode
{
    public float G{get;private set;}
    public float weight = 0;
    public float H { get; private set;}
    public Vector2 Pos;
    public float F => G + H+weight;
    public int x;
    public int y;
    public bool isWalkable;
    public PathNode cameFromNode;
    public List<PathNode> Neighbors;
    public PathNode Connection { get; private set; }
    public void SetConnection(PathNode nodebase)=>Connection=nodebase;
    public void SetG(float g)=>G=g+weight;
    public void setH(float h)=>H=h;

    private static readonly List<Vector2> Dirs = new List<Vector2>() {
            new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1), new Vector2(1, 0),
            new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 1)
        };
    public void CacheNeighbors()
    {
        Neighbors = new List<PathNode>();
        //fix this later
        foreach (var tile in Dirs.Select(dir => GridManager.Instance.GetTileAtPosition(Pos + dir)).Where(tile => tile != null))
        {
            if (tile != null)
            {
             //   PathNode temp = new PathNode(tile);
                Neighbors.Add(tile.Pnode);
            }
        }
    }
    public float GetDistance(PathNode other)
    {
        if (other != null)
        {
            var dist = new Vector2Int(Mathf.Abs((int)x - (int)other.x), Mathf.Abs((int)y - (int)other.y));

            var lowest = Mathf.Min(dist.x, dist.y);
            var highest = Mathf.Max(dist.x, dist.y);

            var horizontalMovesRequired = highest - lowest;

            return lowest * 14 + horizontalMovesRequired * 10;
        }
        return 100000;
    }
    public PathNode(int X,int Y,bool walkable=true)
    {
        x = X;
        y = Y;
        Pos = new Vector2(X, Y);
        isWalkable =walkable;
    }
    public PathNode(Tile t)
    {

        x = t.x;
        y = t.y;
        isWalkable = t.walkable;
        Pos = new Vector2(x, y);
        //CacheNeighbors();
    }
}

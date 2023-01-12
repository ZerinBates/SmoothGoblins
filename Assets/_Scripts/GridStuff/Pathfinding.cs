using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathPropper
{
    public  List<PathNode> FindPath(PathNode startNode, PathNode targetNode, bool tunnel=false)
    {
        int count = 0;
        int tunnelcount = 0;
        var toSearch = new List<PathNode>() { startNode };
        var processed = new List<PathNode>();
        while (toSearch.Any())
        {
            bool onPath = true;
            count++;
            tunnelcount++;
            var current = toSearch[0];
            //replace with heap or binary tree
            foreach (var t in toSearch)
            {
                if (t.F < current.F || t.F == current.F && t.H < current.H)
                {
                    current = t;
                }
            }
            processed.Add(current);
            toSearch.Remove(current);
            //check if path 
            if (current == targetNode)
            {
                var currentPathTile = targetNode;
                var path = new List<PathNode>();
                while (currentPathTile != startNode)
                {
                    path.Add(currentPathTile);
                    currentPathTile = currentPathTile.Connection;
                }
                 path.Reverse();
                return path ;
            }else if (tunnel)
            {
                if (tunnelcount >  GridManager.Instance.GetWidth()* GridManager.Instance.GetHeight()/2 || tunnelcount ==1)
                {
                    // p = currentPathTile;
                    onPath = false;
                    tunnelcount = 0;
                }
            }

            foreach (var neighbor in current.Neighbors.Where(t => t.isWalkable && !processed.Contains(t)))
            {
                var inSearch = toSearch.Contains(neighbor);
                var costToNeighbor = current.G + current.GetDistance(neighbor);
                if (!inSearch || costToNeighbor < neighbor.G)
                {
                    neighbor.SetG(costToNeighbor);
                    neighbor.SetConnection(current);
                    if (!inSearch)
                    {
                        neighbor.setH(neighbor.GetDistance(targetNode));
                        toSearch.Add(neighbor);
                    }
                }
            }
            if (tunnel&&!onPath)
            {
                float cost = current.GetDistance(targetNode);
                PathNode p = null;
                foreach (var neighbor in current.Neighbors.Where(t =>  !processed.Contains(t)))
                {
                    var inSearch = toSearch.Contains(neighbor);
                    var costToNeighbor = current.G + current.GetDistance(neighbor);

                    //if (!inSearch || costToNeighbor < neighbor.G)
                    //{

                        neighbor.isWalkable = true;
                        neighbor.SetG(costToNeighbor);
                      //  neighbor.SetConnection(current);
                        float distance =neighbor.GetDistance(targetNode);
                        if (distance<cost)
                        {
                    //    p.isWalkable = false;
                        onPath = true;
                        cost = distance;
                            p=neighbor;
                        }

                   //}
                }
                if (p != null)
                {
                    onPath = true;
                    p.isWalkable = true;
                    p.SetConnection(current);
                    GridManager.Instance.SetTile(p.x, p.y, GridManager.Instance._grassTile, true);
                    p.setH(p.GetDistance(targetNode));
                    toSearch.Add(p);
                }
                
            }
        }
        return null;
    }
}
public class Pathfinding 
{




    public void setNode(int x, int y, string name, bool walkable = false, bool visable = false, int weight = 0)
    {
        PathNode node = new PathNode(x, y,walkable);

    }

    /// <summary>
    /// NON EXPERIMANTLE
    /// </summary>
    public double[][] neighborGraph;
    int _height,_width;
    public Pathfinding(double[][] graph,int width,int height)
    {
        neighborGraph = graph;
        _width = width;
        _height = height;

    }

    public void GetAttackingNeighbourgraph(int x, int y, double distance)
    {
        int step = 1;
        if (GridManager.Instance.GetTileAtPosition(x, y)._weight > 0)
        {
            step = 0;
        }
        if (distance <= 0)
        {
            return;
        }
        if (x - 1 >= 0)
        {
            // Left
            if (neighborGraph[x - 1][y] < distance)
            {
                neighborGraph[x - 1][y] = distance;
                if (!GridManager.Instance.GetTileAtPosition(x - 1, y)._isConcealing)
                {
                    GetAttackingNeighbourgraph(x - 1, y, distance - step);
                }
            }

            // Left Down
            if (y - 1 >= 0)
            {
                if (neighborGraph[x - 1][y - 1] < distance)
                {
                    neighborGraph[x - 1][y - 1] = distance;
                    if (!GridManager.Instance.GetTileAtPosition(x - 1, y - 1)._isConcealing)
                    {
                        GetAttackingNeighbourgraph(x - 1, y - 1, distance - step * 1.5);
                    }
                }
            }

            // Left Up
            if (y + 1 < _height)
            {
                if (neighborGraph[x - 1][y + 1] < distance)
                {
                    neighborGraph[x - 1][1 + y] = distance;
                    if (!GridManager.Instance.GetTileAtPosition(x - 1, y + 1)._isConcealing)
                    {
                        GetAttackingNeighbourgraph(x - 1, y + 1, distance - step * 1.5);
                    }
                }
            }
        }
        if (x + 1 < _width)
        {
            // Right
            // Left
            if (neighborGraph[x + 1][y] < distance)
            {
                neighborGraph[x + 1][y] = distance;
                if (!GridManager.Instance.GetTileAtPosition(x + 1, y)._isConcealing)
                {
                    GetAttackingNeighbourgraph(x + 1, y, distance - step);
                }
            }

            // Right Down
            if (y - 1 >= 0)
            {
                if (neighborGraph[x + 1][y - 1] < distance)
                {
                    neighborGraph[x + 1][y - 1] = distance;
                    if (!GridManager.Instance.GetTileAtPosition(x + 1, y - 1)._isConcealing)
                    {
                        GetAttackingNeighbourgraph(x + 1, y - 1, distance - step * 1.5);
                    }
                }
            }

            // Right Up
            if (y + 1 < _height)
            {
                if (neighborGraph[x + 1][y + 1] < distance)
                {
                    neighborGraph[x + 1][y + 1] = distance;
                    if (!GridManager.Instance.GetTileAtPosition(x + 1, y + 1)._isConcealing)
                    {
                        GetAttackingNeighbourgraph(x + 1, y + 1, distance - step * 1.5);
                    }
                }
            }

        }
        // Down
        if (y - 1 >= 0)
            if (neighborGraph[x][y - 1] < distance)
            {
                neighborGraph[x][y - 1] = distance;
                if (!GridManager.Instance.GetTileAtPosition(x, y - 1)._isConcealing)
                {
                    GetAttackingNeighbourgraph(x, y - 1, distance - step);
                }
            }
        // Up
        if (y + 1 < _height)
            if (neighborGraph[x][y + 1] < distance)
            {
                neighborGraph[x][y + 1] = distance;
                if (!GridManager.Instance.GetTileAtPosition(x, y + 1)._isConcealing)
                {
                    GetAttackingNeighbourgraph(x, y + 1, distance - step);
                }
            }

    }
    public void GetWalkingNeighbourgraph(int x, int y, double distance)
    {
        int step = 1;
        if (GridManager.Instance.GetTileAtPosition(x, y)._weight > 0)
        {
            step = 2;
        }
        if (distance <= 0)
        {
            return;
        }
        if (x - 1 >= 0)
        {
            // Left
            if (neighborGraph[x - 1][y] < distance)
            {
                neighborGraph[x - 1][y] = distance;
                if (GridManager.Instance.GetTileAtPosition(x - 1, y).walkable)
                {
                    if (distance - step > 0)
                    {
                        GetWalkingNeighbourgraph(x - 1, y, distance - step);
                    }
                }
            }

            // Left Down
            if (y - 1 >= 0)
            {
                if (neighborGraph[x - 1][y - 1] < distance)
                {
                    neighborGraph[x - 1][y - 1] = distance;
                    if (GridManager.Instance.GetTileAtPosition(x - 1, y - 1).walkable)
                    {
                        if (distance - step > 0)
                            GetWalkingNeighbourgraph(x - 1, y - 1, distance - step * 1.5);
                    }
                }
            }

            // Left Up
            if (y + 1 < _height)
            {
                if (neighborGraph[x - 1][y + 1] < distance)
                {
                    neighborGraph[x - 1][1 + y] = distance;
                    if (GridManager.Instance.GetTileAtPosition(x - 1, y + 1).walkable)
                    {
                        if (distance - step > 0)
                            GetWalkingNeighbourgraph(x - 1, y + 1, distance - step * 1.5);
                    }
                }
            }
        }
        if (x + 1 < _width)
        {
            // Right
            // Left
            if (neighborGraph[x + 1][y] < distance)
            {
                neighborGraph[x + 1][y] = distance;
                if (GridManager.Instance.GetTileAtPosition(x + 1, y).walkable)
                {
                    if (distance - step > 0)
                        GetWalkingNeighbourgraph(x + 1, y, distance - step);
                }
            }

            // Right Down
            if (y - 1 >= 0)
            {
                if (neighborGraph[x + 1][y - 1] < distance)
                {
                    neighborGraph[x + 1][y - 1] = distance;
                    if (GridManager.Instance.GetTileAtPosition(x + 1, y - 1).walkable)
                    {
                        if (distance - step > 0)
                            GetWalkingNeighbourgraph(x + 1, y - 1, distance - step * 1.5);
                    }
                }
            }

            // Right Up
            if (y + 1 < _height)
            {
                if (neighborGraph[x + 1][y + 1] < distance)
                {
                    neighborGraph[x + 1][y + 1] = distance;
                    if (GridManager.Instance.GetTileAtPosition(x + 1, y + 1).walkable)
                    {
                        if (distance - step > 0)
                            GetWalkingNeighbourgraph(x + 1, y + 1, distance - step * 1.5);
                    }
                }
            }

        }
        // Down
        if (y - 1 >= 0)
            if (neighborGraph[x][y - 1] < distance)
            {
                neighborGraph[x][y - 1] = distance;
                if (GridManager.Instance.GetTileAtPosition(x, y - 1).walkable)
                {
                    if (distance - step > 0)
                        GetWalkingNeighbourgraph(x, y - 1, distance - step);
                }
            }
        // Up
        if (y + 1 < _height)
            if (neighborGraph[x][y + 1] < distance)
            {
                neighborGraph[x][y + 1] = distance;
                if (GridManager.Instance.GetTileAtPosition(x, y + 1).walkable)
                {
                    if (distance - step > 0)
                        GetWalkingNeighbourgraph(x, y + 1, distance - step);
                }
            }

    }

    public double[][] getGraph()
    {
        return neighborGraph;
    }
}
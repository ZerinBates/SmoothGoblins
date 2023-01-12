using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GridManager : MonoBehaviour
{
    public List<MapEvents> events = new List<MapEvents>();
    public static GridManager Instance;
    public bool start = true;
    [SerializeField] private ItemBasic test;
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] public Tile _grassTile, _mountainTile,_hinderingTile;
    [SerializeField] private Transform _cam;
    [SerializeField] private GameObject generate;
    public Dictionary<Vector2, Tile> _tiles;
    public int GetWidth() { return _width;}
    public int GetHeight() { return _height; }
    private void Awake()
    {
        Instance = this;
    }
    public void RemoveTile(int x,int y)
    {
        Tile t = GetTileAtPosition(x, y);
        Destroy(t);
        _tiles.Remove(new Vector2(x,y));
        t.gameObject.SetActive(false);
        //t.GetComponent<GameObject>().SetActive(false);
        // _tiles.Clear(new Vector2(x, y),GetTileAtPosition(x,y));
        // GetTileAtPosition(x, y).delete();
    }
    public void SetTile(int x, int y,Tile t, bool pregame = true)
    {
        Tile t1 = GetTileAtPosition(x, y);
        Destroy(t1);
        _tiles.Remove(new Vector2(x, y));
        t1.gameObject.SetActive(false);
   //     RemoveTile(x, y);
        var spawnedTile = Instantiate(t, new Vector3(x, y), Quaternion.identity); ;
        spawnedTile.name = $"Tile {x},{y}";

        spawnedTile.Init(x, y);

        _tiles[new Vector2(x, y)] = spawnedTile;
        _tiles[new Vector2(x, y)].Darken();
     //   _tiles[new Vector2(x, y)].gameObject.SetActive(true);
        if (pregame)
        {
            return;
        }
    }
    public void GenerateGrid(bool WaveFunction = false)
    {
        _tiles = new Dictionary<Vector2, Tile>();
        if (WaveFunction)
        {
            OverlapMap gen = generate.GetComponent<OverlapMap>();
            gen.width = _width + 1;
            gen.depth = _height + 1;
            gen.Generate();
            gen.Run();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                   // RemoveTile(x, y);
                    var spawnedTile = Instantiate(GridManager.Instance._grassTile, new Vector3(x, y), Quaternion.identity);
                    SetTile(x, y, spawnedTile);
                   // spawnedTile.name = $"Tile {x},{y}";

                    //spawnedTile.Init(x, y);

                   // GridManager.Instance._tiles[new Vector2(x, y)] = spawnedTile;
                }
            }
            
        }
        else
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Tile _chosenTile;
                    if (x != 50 && y != 50)
//if (true)
                    {
                        _chosenTile = UnityEngine.Random.Range(0, 6) == 3 ? _grassTile : _grassTile;
                    }
                    else
                    {
                        _chosenTile = _mountainTile;
                    }

                    var spawnedTile = Instantiate(_chosenTile, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Tile {x},{y}";

                    spawnedTile.Init(x, y);

                    _tiles[new Vector2(x, y)] = spawnedTile;

                }

            }

            var spawnedTile1 = Instantiate(GridManager.Instance._grassTile, new Vector3(0, 0), Quaternion.identity);
            SetTile(0, 0, spawnedTile1);
        }
        //set events
       MapEvents m = new GoblinNook();
       events.Add(m);
       for (int k = 0; k < 0; k++)
     {
        MapEvents chosen =  events.OrderBy(o => UnityEngine.Random.value).First();
         int x = UnityEngine.Random.Range(5, _width);
         int y = UnityEngine.Random.Range(5, _height);
         while (!chosen.requirements(x, y))
         {
              x = UnityEngine.Random.Range(5, _width);
              y = UnityEngine.Random.Range(5, _height);
         }

         chosen.startSeed(x, y);
     }
        //get cached neighbors

        updatePaths();
        PathPropper p = new PathPropper();
        //tunnel
        p.FindPath(GetTileAtPosition(0, 0).Pnode, GetTileAtPosition(_width - 1, _height - 1).Pnode,true);
        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
        //stairsDown s = new stairsDown();
        GetTileAtPosition(_width - 1, _height - 1).setItem(test);
        updatePaths();
        GameManager.Instance.UpdateGameState(GameState.SpawnHeroes);
    }
    public void updatePaths()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                GetTileAtPosition(x, y).Pnode.CacheNeighbors();
                _tiles[new Vector2(x, y)].Darken();
            }
        }
    }
    public void MoveCam(int x, int y)
    {
        if (!GameManager.Instance.formationmoving|| start)
        {
            _cam.transform.position = new Vector3((float)GetTileAtPosition(x, y).transform.position.x, (float)GetTileAtPosition(x, y).transform.position.y, -10);
            start = false;
        }
    }
    //spawning tiles
    public Tile GetHeroSpawnTile()
    {
      //  return GetTileAtPosition(50, 50);
        return _tiles.Where(t => t.Key.x <5&&t.Key.y<5 && t.Value.walkable).OrderBy(t => UnityEngine.Random.value).First().Value;
    }
    public Tile GetEnemySpawnTile()
    {

        return _tiles.Where(t => t.Key.x > _width / 3 && t.Value.walkable).OrderBy(t => UnityEngine.Random.value).First().Value;
    }
    //getTile positions
    public Tile GetTileAtPosition(int x, int y)
    {
        Vector2 pos = new Vector2(x, y);
        if (pos.x < _width && pos.y < _height && pos.x >= 0 && pos.y >= 0)
        {
            return GetTileAtPosition(pos);
        }
        else
        {
            return null;
        }
    }
    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }
    //pathfinding stuff
    //tile creation
    private List <Tile> walkRange= new List<Tile>();
    private List<Tile> AttackRange = new List<Tile>();
    private List<Tile> lightRange = new List<Tile>();
    public void ClearLightTiles()
    {
        if (lightRange.Count > 0)
        {
            foreach (Tile t in lightRange)
            {
                t.Darken();
            }
        }
        lightRange.Clear();
    }
    public void ClearWalkTiles()
    {
        if (walkRange.Count>0)
        {
            foreach (Tile t in walkRange)
            {
                t.UnMark();
            }
        }
        walkRange.Clear();
    }
    public void ClearAttackTiles()
    {
        if (AttackRange.Count > 0)
        {
            foreach (Tile t in AttackRange)
            {
                t.UnMark();
            }
        }
        AttackRange.Clear();
    }
    public void PrintLight(Tile characterTile, int distance = 8)
    {
        List<Tile> t = new List<Tile>() { characterTile };
        PrintLight(t);
    }

    public void PrintLight(List<Tile> t, int distance = 8)
    {

        ClearLightTiles();
        HeatMap();
        foreach (Tile p in t)
        {
            Tile characterTile = p;
        //foreach(Basic  character in characters)
        //int distance = 4;//character.stats.getSpeed();
        int x = characterTile.x;
        int y = characterTile.y;
        int nx = (characterTile.x - distance);
        int ny = (characterTile.y - distance);
        int py = (characterTile.y + distance);
        int px = (characterTile.x + distance);
        if (nx < 0) { nx = 0; }
        if (ny < 0) { ny = 0; }
        if (px > _width) { px = _width; }
        if (py > _height) { py = _height; }


        double[][] tilemap = GridManager.Instance.FindWalkOptions(characterTile, distance);
        // var realGrid = grid.GetGrid();

        //bool range = false;

            HM.setValue(p.x, p.y, 60);

            for (int i = ny; i < py; i++)
            {
                for (int j = nx; j < px; j++)
                {
                    if (tilemap[j][i] > 0)
                    {
                        if (WithinRange(t, distance, j, i))
                        {
                            GridManager.Instance.GetTileAtPosition(j, i).Light();
                            lightRange.Add(GridManager.Instance.GetTileAtPosition(j, i));
                        }
                    }
                }
            }
        }
    }
    public void PrintWalkTile(Tile characterTile)
    {

        int distance = characterTile.OccupiedUnit.GetStats().Speed;
        int x = characterTile.x;
        int y = characterTile.y;
        int nx = (characterTile.x - distance);
        int ny = (characterTile.y - distance);
        int py = (characterTile.y + distance);
        int px = (characterTile.x + distance);
        if (nx < 0) { nx = 0; }
        if (ny < 0) { ny = 0; }
        if (px >= _width) { px = _width-1; }
        if (py >= _height) { py = _height-1; }

        ClearWalkTiles();
        double[][] tilemap = GridManager.Instance.FindWalkOptions(characterTile, distance);
        // var realGrid = grid.GetGrid();
        for (int i = ny; i <= py; i++)
        {
            for (int j = nx; j <= px; j++)
            {
                if (tilemap[j][i] > 0)
                {
                    GridManager.Instance.GetTileAtPosition(j, i).MarkBlue();
                    walkRange.Add(GridManager.Instance.GetTileAtPosition(j, i));
                }
            }
        }
    }
    public void getWalkRange(Tile characterTile)
    {

        int distance = characterTile.OccupiedUnit.GetStats().Speed;
        int x = characterTile.x;
        int y = characterTile.y;
        int nx = (characterTile.x - distance);
        int ny = (characterTile.y - distance);
        int py = (characterTile.y + distance);
        int px = (characterTile.x + distance);
        if (nx < 0) { nx = 0; }
        if (ny < 0) { ny = 0; }
        if (px >= _width) { px = _width - 1; }
        if (py >= _height) { py = _height - 1; }

        ClearWalkTiles();
        double[][] tilemap = GridManager.Instance.FindWalkOptions(characterTile, distance);
        // var realGrid = grid.GetGrid();
        for (int i = ny; i <= py; i++)
        {
            for (int j = nx; j <= px; j++)
            {
                if (tilemap[j][i] > 0)
                {
                  //  GridManager.Instance.GetTileAtPosition(j, i).MarkBlue();
                    walkRange.Add(GridManager.Instance.GetTileAtPosition(j, i));
                }
            }
        }
    }
    public Tile getClosestTile(Tile characterTile, Tile target,bool dontAttack=false)
    {
        int distance = characterTile.OccupiedUnit.GetStats().Speed;
        int x = characterTile.x;
        int y = characterTile.y;
        int nx = (characterTile.x - distance);
        int ny = (characterTile.y - distance);
        int py = (characterTile.y + distance);
        int px = (characterTile.x + distance);
        if (nx < 0) { nx = 0; }
        if (ny < 0) { ny = 0; }
        if (px >= _width) { px = _width - 1; }
        if (py >= _height) { py = _height - 1; }

        ClearWalkTiles();
        double[][] tilemap = GridManager.Instance.FindWalkOptions(characterTile, distance);
        double maxDistance = 1000;
        Tile daTile = characterTile;
        // var realGrid = grid.GetGrid();
        for (int i = ny; i <= py; i++)
        {
            for (int j = nx; j <= px; j++)
            {
                if (tilemap[j][i] > 0)
                {
                  if (maxDistance >=  Math.Sqrt((Math.Pow(j - target.x, 2) + Math.Pow(i - target.y, 2))))
                    {

                        daTile = GridManager.Instance.GetTileAtPosition(j, i);
                        if (daTile.walkable)
                        {
                            maxDistance = Math.Sqrt((Math.Pow(j - target.x, 2) + Math.Pow(i - target.y, 2)));
                        }
                    }
                }
            }
        }
        return daTile;
    }
    public void PrintAttackTile(Tile characterTile)
    {
        int range = characterTile.OccupiedUnit.GetStats().Range;
        //int distance = 4;//character.stats.getSpeed();
        int x = characterTile.x;
        int y = characterTile.y;
        int nx = (characterTile.x - range);
        int ny = (characterTile.y - range);
        int py = (characterTile.y + range);
        int px = (characterTile.x + range);
        if (nx < 0) { nx = 0; }
        if (ny < 0) { ny = 0; }
        if (px >= _width) { px = _width-1; }
        if (py >= _height) { py = _height-1; }
        List<AttackStyle> attack = characterTile.OccupiedUnit.GetStats().AttackType;
       // int range = characterTile.OccupiedUnit.getStats().Range;
        ClearAttackTiles();
        double[][] tilemap = GridManager.Instance.FindAttackOptions(characterTile, range);
        // var realGrid = grid.GetGrid();
        for (int i = ny; i <= py; i++)
        {
            for (int j = nx; j <=px; j++)
            {
                if (tilemap[j][i] > 0)
                {

                    if (CombatManager.Instance.AttackRange(attack, x, y, j, i, 10))
                    {
                        List<Tile> temp = new List<Tile>();
                        temp.Add(characterTile);
                        if (WithinRange( temp,10,j,i))
                        {
                            if (characterTile.OccupiedUnit.darkened == false)
                            {
                                GridManager.Instance.GetTileAtPosition(j, i).MarkRed();
                                AttackRange.Add(GridManager.Instance.GetTileAtPosition(j, i));
                            }
                        }
                    }
                }
            }
        }

    }
    //graphs
    private double[][] neighborGraph;
    public double[][] FindAttackOptions(Tile tile, int distance)
    {
        neighborGraph = new double[_width][];
        for (int i = 0; i < _width; i++)
        {
            neighborGraph[i] = new double[_height];
            for (int j = 0; j < _height; j++)
            {
                neighborGraph[i][j] = 0;
            }
        }
        neighborGraph[tile.x][tile.y] = distance;
        Pathfinding p = new Pathfinding(neighborGraph, _width, _height);
        p.GetAttackingNeighbourgraph(tile.x, tile.y, 20);
        neighborGraph = p.getGraph();
        return neighborGraph;
    }
    public List<PathNode>getPath (Tile first,Tile last)
    {
        PathPropper p = new PathPropper();
       
       // var t= new List<PathNode>();
        return p.FindPath(first.Pnode, last.Pnode); ;
    }
    public double[][] FindWalkOptions(Tile tile, int distance)
    {
        neighborGraph = new double[_width][];
        for (int i = 0; i < _width; i++)
        {
            neighborGraph[i] = new double[_height];
            for (int j = 0; j < _height; j++)
            {
                neighborGraph[i][j] = 0;
            }
        }
        neighborGraph[tile.x][tile.y] = distance;
        Pathfinding p = new Pathfinding(neighborGraph, _width, _height);
        p.GetWalkingNeighbourgraph(tile.x, tile.y, distance);
        neighborGraph=p.getGraph();
        return neighborGraph;
    }
    public void VeiwDark(Tile characterTile, int distance = 4)
    {

    }

    
    //Heatmap
    public HeatMap HM;
    public int[][] HeatMap(int vis = 6)
    {
        int obst = -50;
        //Console.WriteLine("wtf");
        int[][] heatMap = new int[_width][];
        for (int i = 0; i < _width; i++)
        {
            heatMap[i] = new int[_height];
            for (int j = 0; j < _height; j++)
            {
                if (GetTileAtPosition(i, j)._isConcealing == true)
                {

                    heatMap[i][j] = obst;
                }
                else
                {
                    heatMap[i][j] = 0;
                }
            }




        }

        HM = new HeatMap(heatMap, _width, _height);
        return heatMap;
    }
    public bool MoveUp(Tile main, Tile target)
    {
        bool solution = false;
        Tile basic = WithinThreat(main, target);
        if (basic != null)
        {
            basic.SetUnit(main.OccupiedUnit);
            solution = true;
        }
        return solution;

    }
    public Tile WithinThreat(Tile main,Tile target)
    {
        Tile success = null;
        UnitBasic mainC = main.OccupiedUnit;
        int range = mainC.GetStats().Range;
        int speed = mainC.GetStats().Speed;
        int x1 = main.x;
        int y1 = main.y;
        int x2 = target.x;
        int y2 = target.y;
        double maxDistance = Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
        if (range+speed > maxDistance)
        {
            double max = 100;
            if (walkRange.Count == 0)
            {
                getWalkRange(main);
            }
            foreach (Tile tile in walkRange)
            {
                double temp = mainC.CanHit(tile, target);
                if (temp< max )
                {
                    if (tile.walkable)
                    {
                        if (main.OccupiedUnit.Threatens(tile,target)){
                            max = temp;
                            success = tile;
                        }
                    }
                }
                
                
            }
        }
        return success;
    }
    public bool WithinRange(int startx, int starty, int distance, int endx, int endy)
    {
        Tile t = GetTileAtPosition(startx, starty);
        return WithinRange(new List<Tile>{t},distance,endx,endy);
    }
        public bool WithinRange(List<Tile> nodes, int distance, int endx, int endy)
    {
        bool range = false;
        //distance=10;
            foreach (Tile node in nodes)
        {
            int x = node.x;
            int y = node.y;
            int nx = (node.x - distance);
            int ny = (node.y - distance);
            int py = (node.y + distance);
            int px = (node.x + distance);
            if (nx < 0) { nx = 0; }
            if (ny < 0) { ny = 0; }
            if (px > _width) { px = _width; }
            if (py > _height) { py = _height; }
            if (endx <= px && endx >= nx && endy <= py && endy >= ny)
            {
                int[,] xy = new int[,] { { x, y } };

                if (HM.drawLine(x, y, endx, endy))
                {

                    range = true;
                    return range;
                }
            }
        }

        return range;
    }
    //A* pathfinding
    /*
    public static List<Tile> FindPath(Tile startNode, Tile targetNode)
    {
        var toSearch = new List<Tile>() { startNode };
        var processed = new List<Tile>();
        
        while (toSearch.Any())
        {
            var current = toSearch[0];
            foreach(var t in toSearch)
            {
                if (t.FCost < current.FCost || t.FCost == current.FCost && t.hCost < current.hCost)
                    current = t;
                processed.Add(current);
                toSearch.Remove(current);
                foreach(var neighbor in current.Neighbors.Where(t => t.Walkable && !processed.Contains(t)))
                {

                }
            }
        }
    }*/
}

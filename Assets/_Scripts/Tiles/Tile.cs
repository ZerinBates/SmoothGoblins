using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] protected Color _baseColor;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _blue;
    [SerializeField] private GameObject _red;
    [SerializeField] private GameObject _dark;
    public bool lit = false;
    [SerializeField] private bool _iswalkable;
   // public ICoords Coords;
    [SerializeField] public bool _isConcealing;
    [SerializeField] public int _weight=0;
    public int x;
    public int y;
   // public GameObject gameObject { get; }
    // public int weight=1;
    public Tile Connection { get; private set; }
    public float gCost;
    public float hCost;
    public float FCost=>gCost+hCost;
    private bool walkMarked=false;
    //private bool attackMarked = false;
    public string TileName;
    public UnitBasic OccupiedUnit;
    public List<Tile> Neighbors { get; protected set; }
    public bool walkable => _iswalkable && OccupiedUnit == null;
    public void SetConnection(Tile tile) => Connection = tile;
    public void SetG(float g) => gCost = g;
    public void SetH(float h) => hCost = h;
    public PathNode Pnode;
    public void MarkRed()
    {
        //attackMarked = true;
        // _blue.SetActive(false);

            _red.SetActive(true);
        
    }
    public void MarkBlue()
    {
        walkMarked = true;
        _blue.SetActive(true);
    }
    public void UnMark()
    {
        walkMarked = false;
        _blue.SetActive(false);
        _red.SetActive(false);
    }
    public void Light()
    {
//        GameManager.Instance.count++;
 //       Debug.Log(GameManager.Instance.count);
        lit = true;
        _dark.SetActive(false);
        if (OccupiedUnit)
        {
            OccupiedUnit.Light();
        }
    }
    public void Darken()
    {
     //   GameManager.Instance.count++;
     //   Debug.Log(GameManager.Instance.count);
        lit = false;
        _dark.SetActive(true);
        if (OccupiedUnit)
        {
            OccupiedUnit.Darken();
        }
    }
    public virtual void Init(int x, int y)
    {
   //     Darken();
        this.x = x;
        this.y = y;
        Pnode = new PathNode(x,y,walkable);
        // _renderer.color = isOffset ? _offsetColor : _baseColor;
        _renderer.color = _baseColor;
    }
    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
        if (OccupiedUnit)
        {
            MenuManager.Instance.ShowSelectedHero(OccupiedUnit);
            GridManager.Instance.PrintAttackTile(this);
        }
    }
    private void OnMouseExit()
    {
        _highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
        if (OccupiedUnit)
        {

                GridManager.Instance.ClearAttackTiles();
            
           if( UnitManager.Instance.SelectedHero!=null)
            {
                GridManager.Instance.PrintWalkTile(UnitManager.Instance.SelectedHero.OccupiedTile);
            }
        }
    }
    private void OnMouseDown()
    {
        //select hero
        if (GameManager.Instance.State != GameState.HeroesTurn) return;
        if (OccupiedUnit != null)
        {
            //after selecting hero 
            if (OccupiedUnit.Faction == Faction.Hero)
            {
                //   UnitManager.Instance.SetSelectedHero((HeroBasic)OccupiedUnit);
               // GridManager.Instance.PrintAttackTile(this);

            }
            else
            {
                if (UnitManager.Instance.SelectedHero != null)
                {
                    var enemy = (UnitBasic)OccupiedUnit;
                    if(UnitManager.Instance.SelectedHero.CanHit(UnitManager.Instance.SelectedHero.OccupiedTile, this)== 100000)
                    {

                        //if (GridManager.Instance.MoveUp(UnitManager.Instance.SelectedHero.OccupiedTile, this)&& UnitManager.Instance.MovePoint > 0)
                        //{
                        //    UnitManager.Instance.MovePoint--;

                        //}

                    }

                    UnitManager.Instance.AttackSelect(enemy);
                    //  Destroy(enemy.gameObject);
                    if (UnitManager.Instance.MovePoint <= 0&& UnitManager.Instance.ActivePoint <= 0){
                        UnitManager.Instance.SetSelectedHero(null);
                    }
                    //UnitManager.Instance.NextInitiative();
                }
            }
        }else
        //blank tile
        {
            //move
            if (UnitManager.Instance.SelectedHero != null)
            {
                if (walkMarked && UnitManager.Instance.MovePoint > 0)
                {
                    if (SetUnit(UnitManager.Instance.SelectedHero))
                    {
                        if (UnitManager.Instance.ActivePoint <= 0)
                        {
                            UnitManager.Instance.SetSelectedHero(null);
                        }
                        UnitManager.Instance.MovePoint--;
                        UnitManager.Instance.NextInitiative();
                    }
                }
                else
                {
                    if (GameManager.Instance.formationmoving)
                    {
                        if (this.walkable)
                        {
                            UnitManager.Instance.selectedTile = this;
                            UnitManager.Instance.NextInitiative();
                        }
                    }
                    GridManager.Instance.MoveCam(x, y);
                }
            }
            else
            {
                GridManager.Instance.MoveCam(x, y);
            }

        }

    }
    public bool SetUnit(UnitBasic u)
    {
        if (walkable)
        {

            if (u.OccupiedTile != null) u.OccupiedTile.OccupiedUnit = null;
            u.transform.position = this.transform.position;
            this.OccupiedUnit = u;
            u.OccupiedTile = this;
    //        UnitManager.Instance.PartyLight();
            return true;
            //GridManager.Instance.PrintLight(this);
        }
        return false;
    }
    public double DistanceToTile(Tile t)
    {
        double distance = System.Math.Sqrt(System.Math.Pow(t.x - this.x, 2) + System.Math.Pow(t.y - this.y, 2));

        return distance;
    }
}
/*
public interface ICoords
{
    public float GetDistance(ICoords other);
    public Vector2 Pos { get; set; }
}*/
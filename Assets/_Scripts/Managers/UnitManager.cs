using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
//using System;

public class UnitManager : MonoBehaviour
{
    public int maxDistance = 5;
    public List<ScriptableUnits> _units;
    public static UnitManager Instance;
    private int trackerInitiative;
    public HeroBasic SelectedHero;
    public int ActivePoint=0;
    public int MovePoint=0;
    public EnemiesBasic selectedEnemy;
    public Tile selectedTile=null;
    
    public List<HeroBasic> Party = new List<HeroBasic>();
    public List<UnitBasic> initiative;
    public List<EnemiesBasic> Foes = new List<EnemiesBasic>();
    // turn orders
    private void Awake()
    {
        Instance = this;
        _units = Resources.LoadAll<ScriptableUnits>("Units").ToList();
    }
    public void SetInitiative(List<HeroBasic> hero, List<EnemiesBasic> enemies)
    {
        initiative = new List<UnitBasic>();
        int good = hero.Count - 1;
        int bad = enemies.Count - 1;
        while (good >= 0 || bad >= 0)
        {
            int num = Random.Range(0, 10);
            if (num < 5)
            {
                if (good >= 0)
                {
                    initiative.Add(hero[good]);
                    good--;
                }
                else if (bad >= 0)
                {
                    initiative.Add(enemies[bad]);
                    bad--;
                }
            } else if (bad >= 0)
            {
                initiative.Add(enemies[bad]);
                bad--;
            }
            else if (good >= 0)
            {
                initiative.Add(hero[good]);
                good--;
            }
        }

    }
    public void StartCombat(bool surprised=false,List<EnemiesBasic> enemies = null)
    {
        if (initiative != null)
        {
            TriggerManager.Instance.setAll(initiative);
        }
        if (enemies==null)
        {
            enemies = Foes;
        }
        SetInitiative(Party, enemies);
        trackerInitiative = 0;
        NextInitiative();
    }
    public void NextInitiative()
    {
        if (initiative != null)
        {
            TriggerManager.Instance.setAll(initiative);
        }
        if(Party.Count <= 0)
        {
            GameManager.Instance.UpdateGameState(GameState.EndGame);
            return;
        }
        if (trackerInitiative >= initiative.Count)
        {
            trackerInitiative = 0;
        }
        if (initiative[trackerInitiative].Faction == Faction.Enemy)
        {
            selectedEnemy = (EnemiesBasic)initiative[trackerInitiative];
            TriggerManager.Instance.UpdateTrigger(TriggerEvent.TurnStart, selectedEnemy);
            trackerInitiative++;
            GameManager.Instance.UpdateGameState(GameState.EnemiesTurn);
            TriggerManager.Instance.UpdateTrigger(TriggerEvent.TurnEnd, selectedEnemy);
        }
        else
        {

                // ActivePoint = 1;
                MovePoint = 1;
                SetSelectedHero((HeroBasic)initiative[trackerInitiative]);
                TriggerManager.Instance.UpdateTrigger(TriggerEvent.TurnStart, SelectedHero);
            int x = SelectedHero.OccupiedTile.x;
                int y = SelectedHero.OccupiedTile.y;
                GridManager.Instance.MoveCam(x, y);
                trackerInitiative++;
                GameManager.Instance.UpdateGameState(GameState.HeroesTurn);
            if (!GameManager.Instance.formationmoving || selectedTile == null)
            {

            }
            else
            {
                // pathfinding initiative !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!------------------------
               List<PathNode> temp =  GridManager.Instance.getPath(SelectedHero.OccupiedTile, selectedTile);
                //               GridManager.Instance.MoveUp(SelectedHero.OccupiedTile, selectedTile);
                Tile basic = GridManager.Instance.GetTileAtPosition(temp[0].x, temp[0].y);
                if (CheckPartyFormation(basic))
                {
                    basic.SetUnit(SelectedHero);
                }
                if (SelectedHero.OccupiedTile == selectedTile) {
                    selectedTile = null;
                    //GameManager.Instance.formationmoving = false;
                    TriggerManager.Instance.UpdateTrigger(TriggerEvent.TurnEnd, SelectedHero);
                    NextInitiative();
                }
                // GameManager.Instance.wait();
                TriggerManager.Instance.UpdateTrigger(TriggerEvent.TurnEnd, SelectedHero);
                StartCoroutine(SmoothMovement());
               
            }
          //  checkTriggers((UnitBasic)SelectedHero, TriggerEvent.TurnEnd);
        }
    }
    private IEnumerator SmoothMovement()
    {
       // Debug.Log("begin");
        yield return new WaitForSecondsRealtime(.05f);
        NextInitiative();

    }
    //
    public bool CheckPartyFormation(Tile t)
    {
        bool temp = false;
        foreach (UnitBasic u in Party)
        {
            int j = u.OccupiedTile.x;
            int i = u.OccupiedTile.y;
            if (System.Math.Sqrt(System.Math.Pow(j - t.x, 2) + System.Math.Pow(i - t.y, 2)) <= maxDistance)
            {
                temp = true;
            }
        }
        return temp;
    }
    //Spawners 
    public void SpawnHeroes()
    {
        int HeroCount = 4;
        for(int i = 0; i < HeroCount; i++)
        {
            var randomPrefab = GetRandomeUnit<UnitBasic>(Faction.Hero);
            var spawnedHero = Instantiate(randomPrefab);
            var randomeSpawnTile = GridManager.Instance.GetHeroSpawnTile();
            Party.Add((HeroBasic)spawnedHero);
            randomeSpawnTile.SetUnit(spawnedHero);

        }
        GridManager.Instance.MoveCam(Party[0].OccupiedTile.x, Party[0].OccupiedTile.y);
        GameManager.Instance.UpdateGameState(GameState.SpawnEnemies);
    }
    public void SpawnEnemy()
    {
        int enemyCount = 5;
        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = GetRandomeUnit<UnitBasic>(Faction.Enemy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomeSpawnTile = GridManager.Instance.GetEnemySpawnTile();
            randomeSpawnTile.SetUnit(spawnedEnemy);
            Foes.Add((EnemiesBasic)spawnedEnemy);
        }
        GameManager.Instance.UpdateGameState(GameState.CombatStart);
    }
    public void SpawnUnit(string u,int x, int y)
    {
        var prefab = GetUnit<UnitBasic>(u);
        var spawnedUnit = Instantiate(prefab);
        Tile t = GridManager.Instance.GetTileAtPosition(x, y);
        t.SetUnit(spawnedUnit);

        if (spawnedUnit.Faction == Faction.Hero)
        {
            Party.Add((HeroBasic)spawnedUnit);
        }else if (spawnedUnit.Faction == Faction.Enemy)
        {
            Foes.Add((EnemiesBasic)spawnedUnit);
        }
    }
    //actions
/*    public bool checkTriggers(UnitBasic react, TriggerEvent trigger)
    {
        foreach(var unit in Party)
        {
            unit.trigger(trigger,react);
        }
        foreach(var unit in Foes)
        {
            unit.trigger(trigger, react);
        }
        return true;
    }*/
    private T GetRandomeUnit<T>(Faction faction)where T : UnitBasic
    {
        return (T)_units.Where(u =>u.faction == faction).OrderBy(o=>Random.value).First().UnitPrefab;
    }
    private T GetUnit<T>(string unit)where T : UnitBasic
    {
        return (T)_units.Where(u=>u.name == unit).First().UnitPrefab;
    }

    public void PartyLight()
    {
        List<Tile> t = new List<Tile>();
        foreach(HeroBasic c in Party)
        {
            if (c.OccupiedTile)
            {
                t.Add(c.OccupiedTile);
            }
        }
        if (t.Count > 0)
        {
            GridManager.Instance.PrintLight(t);
        }
    }

    public void AttackSelect(UnitBasic enemy)
    {
        if (MovePoint > 0)
        {
            if (SelectedHero.Attack(enemy))
            {
                MovePoint--;
                UnitManager.Instance.NextInitiative();
            }
            
        }
    }
    public void SelectEnemyTurn()
    {
        selectedEnemy.SelectTarget(Party);
        selectedEnemy.AttackTarget();
        NextInitiative();
    }
    public void SetSelectedHero(HeroBasic h)
    {
        SelectedHero = h;
        MenuManager.Instance.ShowSelectedHero(h);
        if (h != null)
        {
//            GridManager.Instance.PrintAttackTile(h.OccupiedTile);
            GridManager.Instance.PrintWalkTile(h.OccupiedTile);
            
        }
        else
        {
         //   GridManager.Instance.ClearAttackTiles();
            GridManager.Instance.ClearWalkTiles();
           // NextInitiative();
        }
        PartyLight();
    }
    public void Death(UnitBasic unit)
    {
        if (Party.Contains(unit))
        {
            Party.Remove((HeroBasic)unit);
        }
        if (Foes.Contains(unit))
        {
            Foes.Remove((EnemiesBasic)unit);
        }
        if (initiative.Contains(unit)){
            initiative.Remove(unit);
        }

    }
}

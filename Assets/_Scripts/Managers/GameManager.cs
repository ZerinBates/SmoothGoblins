using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int count = 0;
    public static GameManager Instance;
    public GameState State;
    public bool formationmoving = true;
    public bool menuDisplay = true;
    private void Start()
    {
        UpdateGameState(GameState.GenerateGrid);
    }
    private void Awake()
    {
        Instance = this;
    }
    public static event Action<GameState> onGameStateChanged;
    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid(true);
                break;
            case GameState.SpawnHeroes:
                UnitManager.Instance.SpawnHeroes();
                break;
            case GameState.SpawnEnemies:
                UnitManager.Instance.SpawnEnemy();
                break;
            case GameState.CombatStart:
                UnitManager.Instance.PartyLight();
                UnitManager.Instance.StartCombat();
                break;
            case GameState.HeroesTurn:
                UnitManager.Instance.PartyLight();
                break;
            case GameState.EnemiesTurn:
                UnitManager.Instance.SelectEnemyTurn();
                break;
            case GameState.EndLevel:
                break;
            case GameState.EndGame:

                SceneManager.LoadScene(sceneName: "DeathScene");
                break;
        }
        onGameStateChanged?.Invoke(newState);
    }
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            print("f key was pressed");
            formationmoving = !formationmoving;
        }
        if (Input.GetKeyDown("m"))
        {
            print("m key was pressed");
            menuDisplay = !menuDisplay;
            MenuManager.Instance.ShowSelectedHero(UnitManager.Instance.SelectedHero);
        }
    }
    public void wait()
    {
        StartCoroutine(SmoothMovement());
        Debug.Log("end");
    }
    private IEnumerator SmoothMovement()
    {
        Debug.Log("begin");
        yield return new WaitForSecondsRealtime(10);

    }

}

public enum GameState
{
    GenerateGrid = 0,
    SpawnHeroes = 1,
    SpawnEnemies = 2,
    CombatStart = 3,
    HeroesTurn = 4,
    EnemiesTurn = 5,
    EndLevel = 6,
    EndGame = 7

}

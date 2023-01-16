using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsDown : Encounter
{
    
    public override void Activate()
    {
        Debug.Log("woot woot");
        GameManager.Instance.UpdateGameState(GameState.EndGame);
    }
}

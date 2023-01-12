using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsDown : ItemBasic
{
    public override void SteppedOn(UnitBasic u)
    {
        Debug.Log("woot woot");
        GameManager.Instance.UpdateGameState(GameState.EndGame);
    }
}

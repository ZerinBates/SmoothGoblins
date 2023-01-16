using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FightDisplay : MonoBehaviour
{
    public Text allyNameLabel;
    public Text enemyNameLabel;
    public Text total;
    // public GameObject diceRollPrefab;
    //public Transform diceRollParent;
    public DiceRoller allyDice;
    public DiceRoller enemyDice;
    public List<int> diceRolls;
    
    public Image allySprite;
    public Image enemySprite;
    //public UnitBasic daAlly;
    //public UnitBasic daEnemy;

    void Start()
    {


       // List<int> l = new List<int>() { 1, 2, 3, 4, 5,6 };
       // List<int> l2 = new List<int>() { 1,6,6,6};
       // display(daAlly, l, daEnemy, l2,"3 damage","blue");
    }
    public void display(UnitBasic ally,List<int>allyRolls,UnitBasic enemy,List<int>enemyRolls, string sum,string firstDice = "red")
    {

        SetSprites(ally.gameObject.GetComponent<SpriteRenderer>().sprite, enemy.gameObject.GetComponent<SpriteRenderer>().sprite);
        SetNames(ally.name, enemy.name,sum.ToString());
        
        if (firstDice == "red")
        {
            DisplayDiceRolls(allyRolls, allyDice,"red");
            DisplayDiceRolls(enemyRolls, enemyDice,"blue");
        }
        else
        {
            DisplayDiceRolls(allyRolls, allyDice, "blue");
            DisplayDiceRolls(enemyRolls, enemyDice, "red");
        }
    }
    public void SetSprites(Sprite ally, Sprite enemy)
    {
        Debug.Log(ally);
        allySprite.sprite = ally;
        enemySprite.sprite = enemy;
    }

    public void SetNames(string ally, string enemy,string toats)
    {
        allyNameLabel.text = ally;
        enemyNameLabel.text = enemy;
        total.text = toats;
    }


    // Displays a list of dice rolls
    void DisplayDiceRolls(List<int> rolls,DiceRoller dice,string color)
    {
        dice.CreateDice(rolls,color);
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public List<Sprite> redDice;
    public List<Sprite> blueDice;
    public int numberToCreate;
    //public float distanceBetweenSprites = .75f;
    public GameObject container;
    private float scaleWhite = .3f;
    private List<GameObject> Sprites = new List<GameObject>();
    public void CreateDice(List<int>rolls,string color = "red")
    {
        Clear();
        List<Sprite> dice;
        if(color == "red")
        {
            dice = redDice;
        }
        else
        {
            dice = blueDice;
        }
        int i = 0;
        foreach (int roll in rolls)
        {
            i++;
            GameObject newSprite = new GameObject("Sprite " + i);
            Image image = newSprite.AddComponent<Image>();
            image.sprite = dice[roll-1];
            newSprite.transform.SetParent(container.transform);
            newSprite.transform.localScale = new Vector3(scaleWhite, scaleWhite, 1);
            newSprite.transform.position = container.transform.position + new Vector3((i * .75f), 0, 0);
            Sprites.Add(newSprite);
            //newSprite.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        }
       
    }
    public void Clear()
    {
        foreach (GameObject sprite in Sprites)
        {
            Destroy(sprite);
        }
    }

}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceCreator : MonoBehaviour
{
    public Sprite basicDice;
    public Sprite tempDice;
    public int numberToCreate;
    public float distanceBetweenSprites = .1f;
    public GameObject container;
    private float scaleWhite = 0.005f;
    private List<GameObject> Sprites =new List<GameObject>();
    public void CreateDice(int basicNumber,int tempNumber=0)
    {
        Clear();
        if (tempNumber < 0)
        {
            basicNumber += tempNumber;
            if (basicNumber < 1)
            {
                basicNumber = 0;
            }
        }
        for (int i = 0; i < basicNumber; i++)
        {
            GameObject newSprite = new GameObject("Sprite " + i);
            Image image = newSprite.AddComponent<Image>();
            image.sprite = basicDice;
            newSprite.transform.SetParent(container.transform);
            newSprite.transform.localScale = new Vector3(scaleWhite, scaleWhite, 1);
            newSprite.transform.position = container.transform.position + new Vector3((i*.6f), 0, 0);
            Sprites.Add(newSprite);
            //newSprite.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        }
        if (tempNumber > 0)
        {
            for (int j = basicNumber; j < tempNumber + basicNumber; j++)
            {
                GameObject newSprite = new GameObject("Sprite " + j);
                Image image = newSprite.AddComponent<Image>();
                image.sprite = tempDice;
                newSprite.transform.SetParent(container.transform);
                newSprite.transform.localScale = new Vector3(scaleWhite, scaleWhite, 1);
                newSprite.transform.position = container.transform.position + new Vector3((j * .6f), 0, 0);
                Sprites.Add(newSprite);
                //newSprite.transform.localScale = new Vector3(0.1f, 0.1f, 1);
            }
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

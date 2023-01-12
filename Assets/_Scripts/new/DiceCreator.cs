using UnityEngine;
using UnityEngine.UI;

public class DiceCreator : MonoBehaviour
{
    public Sprite spriteToCreate;
    public int numberToCreate;
    public float distanceBetweenSprites = .1f;
    public GameObject container;
    private float scaleWhite = 0.005f;
    private GameObject oldSprite;
    public void CreateDice(int number)
    {
        
        for (int i = 0; i < number; i++)
        {
            GameObject newSprite = new GameObject("Sprite " + i);
            Image image = newSprite.AddComponent<Image>();
            image.sprite = spriteToCreate;
            newSprite.transform.SetParent(container.transform);
            newSprite.transform.localScale = new Vector3(scaleWhite, scaleWhite, 1);
            newSprite.transform.position = container.transform.position + new Vector3((i*.6f), 0, 0);
            oldSprite = newSprite;
            //newSprite.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        }
    }
    void Start()
    {
      //  Debug.Log("what");
        CreateDice(2);
    }
    }

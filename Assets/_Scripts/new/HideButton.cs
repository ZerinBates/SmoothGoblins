using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButton : MonoBehaviour
{
    public GameObject thing;
    public void onClick()
    {
        thing.SetActive(false);
        GameManager.Instance.ResumeGame();
}
}

using System.Collections.Generic;
using UnityEngine;

public abstract class Encounter : MonoBehaviour
{
    protected int difficulty = 4;
    // The popup window to use
    protected PopupWindow Popup;

    // The list of game objects to send to the popup
    protected List<HeroBasic> Party;

    // The message to display in the popup
    protected string Message;

    // The labels for the buttons in the popup
    protected string[] ButtonLabels;

    public virtual void Activate()
    {
        MenuManager.Instance.ShowPopup(true);
        Party = UnitManager.Instance.Party;
        GameObject PopupObject = GameObject.Find("Popup");
        Popup = PopupObject.GetComponent<PopupWindow>();
        Popup.encounter = this;
        GameManager.Instance.PauseGame();
    }

    // Called when a button is clicked in the popup
    public virtual void OnButtonClicked(string label)
    {
        GameManager.Instance.ResumeGame();
    }

    // The Check function
    public virtual void Check(stats s)
    {

    }
}

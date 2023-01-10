using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    int difficulty = 4;
    // The popup window to use
    public PopupWindow Popup;

    // The list of game objects to send to the popup
    public List<HeroBasic> Party;

    // The message to display in the popup
    public string Message;

    // The labels for the buttons in the popup
    public string[] ButtonLabels;

    public void Activate ()
    {
        MenuManager.Instance.ShowPopup(true);
        Party = UnitManager.Instance.Party;
        //Debug.Log("yay");
        GameObject PopupObject = GameObject.Find("Popup");
        //PopupObject.SetActive(true); 
            
            Popup = PopupObject.GetComponent<PopupWindow>();
            Popup.encounter = this;

            // Show the popup window with the message and buttons
            Popup.Show(Message, ButtonLabels);

            // Send the party list to the popup
            Popup.Party = Party;
        
    }

    // Called when a button is clicked in the popup
    public void OnButtonClicked(string label)
    {
       // Debug.Log("yay");
        stats s = stats.strength;
        // Do something based on the button label
        switch (label)
        {
            case "Fight":
                Check(stats.strength);
                break;
            case "Run":
                Check(stats.speed);
                break;
                // Add additional cases for more buttons
        }
        
    }

    // The Check function
    public void Check(stats s)
    {
        List<UnitBasic> failures = new List<UnitBasic>();
            foreach (UnitBasic unit in Party)
            {

                int roll = unit.SkillCheck(s);
                if (roll < difficulty)
                {
                failures.Add(unit);
                }
            }
        UnitManager.Instance.GroupDamage(failures, 4);
            MenuManager.Instance.ShowPopup(false);
    }
}

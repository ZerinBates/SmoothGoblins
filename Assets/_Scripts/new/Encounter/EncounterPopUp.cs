using System.Collections.Generic;
using UnityEngine;
public class EncounterPopUp : Encounter
{
    // The message to display in the popup
    public string _Message;

    // The labels for the buttons in the popup
    public string[] _ButtonLabels;
    public override void Activate()
    {
        base.Activate();
        // Show the popup window with the message and buttons
        Popup.Show(_Message, _ButtonLabels);
        // Send the party list to the popup
        Popup.Party = Party;
    }

    public override void OnButtonClicked(string label)
    {
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
        base.OnButtonClicked(label);
        // Add additional code for OnButtonClicked
    }

    public override void Check(stats s)
    {
        List<UnitBasic> failures = new List<UnitBasic>();
        foreach (UnitBasic unit in Party)
        {
            int roll = unit.SkillCheck(s);
            if (roll < difficulty)
            {
                failures.Add(unit);
            }
            else
            {
                BuffonEncounter b = new BuffonEncounter();
                unit.AddBuff(b);
                Debug.Log("yay");
            }
        }
        UnitManager.Instance.GroupDamage(failures, 4);
        MenuManager.Instance.ShowPopup(false);
        base.Check(s);
        // Add additional code for Check
    }
}
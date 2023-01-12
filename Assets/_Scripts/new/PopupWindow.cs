using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopupWindow : MonoBehaviour
{
    public Encounter encounter;
    public List<HeroBasic> Party;
    // The text component for displaying the message
    public Text MessageText;

    // The button prefab to use for creating buttons
    public Button ButtonPrefab;

    // The transform where the buttons will be parented
    public Transform ButtonParent;

    // The button click event that will be triggered when a button is clicked
    public Button.ButtonClickedEvent OnButtonClicked;

    // A list of buttons that were created
    private List<Button> buttons = new List<Button>();
    //
    public void getAllButtons()
    {
      Party=UnitManager.Instance.Party;
    }
    void Start()
    {
        // Set the game object to inactive
       // gameObject.SetActive(false);
    }

    // Displays the popup window with the specified message and buttons
    public void Show(string message, params string[] buttonLabels)
    {
       // gameObject.SetActive(true);
        // Set the message text
        MessageText.text = message;

        // Destroy any existing buttons
        foreach (Button button in buttons)
        {
            Destroy(button.gameObject);
        }
        buttons.Clear();

        // Create a button for each button label
        foreach (string label in buttonLabels)
        {
            // Create a new button
            Button button = Instantiate(ButtonPrefab, ButtonParent);

            // Set the button's label
            button.GetComponentInChildren<Text>().text = label;

            // Add the button to the list
            buttons.Add(button);

            // Subscribe to the button's click event
            button.onClick.AddListener(() => encounter.OnButtonClicked(label));
        }
    }
}

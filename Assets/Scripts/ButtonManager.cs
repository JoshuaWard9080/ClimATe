using UnityEngine;
using UnityEngine.UI;

// This script is for buttons that use scripts from the LevelManager. 
// To use this script, ensure that the script the button uses is in LevelManager. Then, attach this to the button, and add add
// another else/if below in the OnClickEvent() function that references the button's name to the expected button name for the script. Then
// just call "script.[your script here]();"
// 
// this script ensures that the buttons are always referencing the LevelManager.
// without this script, the buttons will reference nothing, since they reference a Destroyed Singleton when loading a new level.
public class ButtonManager : MonoBehaviour
{
    private Button button;
    private LevelManager script;

    private void Awake()
    {
        button = GetComponent<Button>();
        script = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        button.onClick.AddListener(OnClickEvent);
    }

    // this currently hard-codes the types of buttons the menu expects and attaches the expected script to it. This could probably be handled differently
    // to enhance modularity, but I can't really think of a way to do it right now lol.
    private void OnClickEvent()
    {
        if (this.name == "ResumeButton")
            script.ResumeGame();
        else if (this.name == "RestartButton")
            script.RetryLevel();
        else if (this.name == "MainMenuButton")
            script.LoadMainMenu();
        else if (this.name == "YesQuitButton")
            script.ConfirmQuitToMainMenu();
        else if (this.name == "NoQuitButton")
            script.CancelQuitToMainMenu();
    }
}

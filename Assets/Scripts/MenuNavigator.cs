using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigator : MonoBehaviour
{
    [SerializeField] private List<Button> buttons = new List<Button>();
    [SerializeField] private AudioSource switchButtonAudio;
    [SerializeField] AudioSource clickButtonAudio;

    private int selectedIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetSelection();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeInHierarchy || buttons.Count == 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            selectedIndex = (selectedIndex - 1 + buttons.Count) % buttons.Count;
            SelectButton(selectedIndex);

            if (switchButtonAudio != null)
            {
                switchButtonAudio.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            selectedIndex = (selectedIndex + 1 + buttons.Count) % buttons.Count;
            SelectButton(selectedIndex);

            if (switchButtonAudio != null)
            {
                switchButtonAudio.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            buttons[selectedIndex].onClick.Invoke();

            if (clickButtonAudio != null)
            {
                clickButtonAudio.Play();
            }
        }
    }

    private void SelectButton(int index)
    {
        EventSystem.current.SetSelectedGameObject(null); // clear the old EventSystem stuff
        EventSystem.current.SetSelectedGameObject(buttons[index].gameObject);
    }

    public void ResetSelection()
    {
        selectedIndex = 0;
        //EventSystem.current.SetSelectedGameObject(null); // clear old EventSystem stuff 
        SelectButton(selectedIndex);
    }

    void OnEnable()
    {
        ResetSelection();
    }
}

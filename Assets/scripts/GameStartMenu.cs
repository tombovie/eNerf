using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject options;
    public GameObject startOptions;

    [Header("Main Menu Buttons")]
    public Button startButton;
    public Button optionButton;
    public Button quitButton;

    public List<Button> returnButtons;

    // Start is called before the first frame update
    void Start()
    {
        EnableMainMenu();

        //Hook events
        startButton.onClick.AddListener(EnableStartGame);
        optionButton.onClick.AddListener(EnableOption);
        quitButton.onClick.AddListener(QuitGame);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(EnableMainMenu);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void HideAll()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
    }

    public void EnableStartGame()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        startOptions.SetActive(true);

    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
    }
    public void EnableOption()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
    }
    public void EnableAbout()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
    }

    //for debugging purposes (delete later)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            EnableStartGame();
        }
    }
}

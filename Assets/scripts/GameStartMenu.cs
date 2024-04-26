using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject startOptions;

    [Header("Main Menu Buttons")]
    public Button startButton;
    public Button quitButton;

    public List<Button> returnButtons;

    // Start is called before the first frame update
    void Start()
    {
        EnableMainMenu();

        //Hook events
        startButton.onClick.AddListener(EnableStartGame);
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
    }

    public void EnableStartGame()
    {
        mainMenu.SetActive(false);
        startOptions.SetActive(true);

    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
    }
    public void EnableOption()
    {
        mainMenu.SetActive(false);
    }
    public void EnableAbout()
    {
        mainMenu.SetActive(false);
    }

    //for debugging purposes (delete later)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            EnableStartGame();
        }
    }
}

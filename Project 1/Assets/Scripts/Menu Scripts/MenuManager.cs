using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] List<Menu> menus = new List<Menu>();
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject specialAbilityMenu;
    public GameObject scoreMenu;
    //public Menu scoreMenu;
    private bool isInMainMenu;
    //private int buildIndex;

    //private BudgetManager bugMan;

    // Start is called before the first frame update
    void Start()
    {
        // Makes sure the ability menu is off at start
        specialAbilityMenu.SetActive(false);

       // bugMan = GameObject.Find("Budget Manager").GetComponent<BudgetManager>();

        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            isInMainMenu = true;
            mainMenu.SetActive(true);
        }
        else
        {
            isInMainMenu = false;
            mainMenu.SetActive(false);
        }
        ShowMenu(menus[0]);
        pauseMenu.SetActive(false);

        // buildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        Debug.Log("isInMainMenu is currently set to: " + isInMainMenu);

        if (Input.GetKeyDown(KeyCode.Escape) && !isInMainMenu)
        {
            PauseGame();
        }

        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            isInMainMenu = false;
            mainMenu.SetActive(false);
        }

    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        isInMainMenu = true;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }


    public void PlayGame()
    {
        isInMainMenu = false;
        SceneManager.LoadScene(1);
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You have quit the game.");
    }

    // This is the No buttons functionality. This could probably be more effective elsewhere
    public void GoToScore()
    {
        specialAbilityMenu.SetActive(false);
        scoreMenu.SetActive(true);
    }

    public void ShowMenu(Menu menuToShow)
    {
        if (menus.Contains(menuToShow) == false)
        {
            Debug.LogErrorFormat("{ 0} is not in the list of menus", menuToShow.name);
            return;
        }

        foreach (Menu otherMenu in menus)
        {

            // Is this the menu we want to display?
            if (otherMenu == menuToShow)
            {
                // Mark it as active.
                otherMenu.gameObject.SetActive(true);

                // Tell the Menu object to invoke its "did appear" action
                otherMenu.menuDidAppear.Invoke();
            }
            else
            {
                // Is the menu currently active?
                if (otherMenu.gameObject.activeInHierarchy)
                {
                    // If so, tell the Menu object to invoke its "will disappear" action
                    otherMenu.menuWillDisappear.Invoke();
                }

                // And mark it as inactive
                otherMenu.gameObject.SetActive(false);
            }
        }
    }
}

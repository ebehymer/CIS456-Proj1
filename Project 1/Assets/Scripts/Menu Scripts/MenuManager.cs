using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] List<Menu> menus = new List<Menu>();
    public GameObject pauseMenu;
    private bool isInMainMenu;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            isInMainMenu = true;
        }
        else
        {
            isInMainMenu = false;
        }
        ShowMenu(menus[0]);
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        Debug.Log("isInMainMenu is currently set to: " + isInMainMenu);

        if(Input.GetKeyDown(KeyCode.Escape) && !isInMainMenu)
        {
            PauseGame();
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

    public void ShowMenu(Menu menuToShow)
    {
        if(menus.Contains(menuToShow) == false)
        {
            Debug.LogErrorFormat("{ 0} is not in the list of menus", menuToShow.name);
            return;
        }

        foreach(Menu otherMenu in menus)
        {

            // Is this the menu we want to display?
            if(otherMenu == menuToShow)
            {
                // Mark it as active.
                otherMenu.gameObject.SetActive(true);

                // Tell the Menu object to invoke its "did appear" action
                otherMenu.menuDidAppear.Invoke();
            }
            else
            {
                // Is the menu currently active?
                if(otherMenu.gameObject.activeInHierarchy)
                {
                    // If so, tell the Menu object to invoke its "will disappear" action
                    otherMenu.menuWillDisappear.Invoke();
                }

                // And mark it as inactive
                otherMenu.gameObject.SetActive(false);
            }
        }
    }

    public void PlayGame()
    {
        isInMainMenu = false;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You have quit the game.");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    private int currentScene;
    private GameObject pauseMenu;
    private bool isInMainMenu = false;

    private void Awake()
    {
        pauseMenu = GameObject.Find("PauseMenu");
    }

    // Start is called before the first frame update
    void Start()
    {
        isInMainMenu = true;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    /// <summary>
    /// Main Menu
    /// </summary>

    public void StartGame()
    {
        SceneManager.LoadScene(currentScene + 1);
        isInMainMenu = false;
    }

    /// <summary>
    /// General Application Buttons
    /// </summary>

    public void QuitButton()
    {
        Application.Quit();
    }

    /// <summary>
    /// Pause Menu Section
    /// </summary>

    public void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isInMainMenu)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        isInMainMenu = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        isInMainMenu = true;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene - 1);
    }
}

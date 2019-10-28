using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    private int currentScene;
    private GameObject pauseMenu;

    private void Awake()
    {
        pauseMenu = GameObject.Find("PauseMenu");
    }

    // Start is called before the first frame update
    void Start()
    {
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
    }

    /// <summary>
    /// General Application Buttons
    /// </summary>

    public void QuitButton()
    {
        Application.Quit();
    }
}

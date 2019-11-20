using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    //public GameObject welcomePanel;
    //public GameObject tilePlacementPanel;
    //public GameObject tileDetailsPanel;
    //public GameObject budgetPanel;
    //public GameObject partyPanel;
    //public GameObject finishedPanel;
    //public GameObject goodLuckaPanel;

    public List<GameObject> tutorialPanels = new List<GameObject>();
    private int index;
    private bool canIncrease;
    private bool canDecrease;
    // Start is called before the first frame update
    void Start()
    {
        //welcomePanel.SetActive(true); 
        tutorialPanels[0].SetActive(true);
        index = 0;
       // Debug.Log("index at start is: " + index);
        canIncrease = true;
        canDecrease = false;
    }
    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && canIncrease)
        {
            index++;
            tutorialPanels[index].SetActive(true);
            tutorialPanels[index - 1].SetActive(false);
            Debug.Log("index is: " + index);
            canDecrease = true;

            if (index == 6)
            {
                canIncrease = false;
            }
        }

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && canDecrease)
        {
            index--;
            tutorialPanels[index].SetActive(true);
            tutorialPanels[index + 1].SetActive(false);
            Debug.Log("index is: " + index);
            canIncrease = true;

            if (index == 0)
            {
                canDecrease = false;
            }
        }
    }

    public void EndTutorial()
    {
        SceneManager.LoadScene("Level Select");
    }


    //IEnumerator BeginTutorial()
    //{ 


    //    //yield return null;
    //}

}

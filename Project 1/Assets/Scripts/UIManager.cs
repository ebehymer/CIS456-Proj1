//Script: UIManager
//Assignment: Project
//Description: Manages the UI (Not In Use)
//Edits made by: CJ
//Last edited by and date: CJ 10/2/2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public Text budgetText;
    public Text spentText;
    public Text partyContainsText;
    public Text tileInfo;

    public int budget;
    public int spent;

    /*
    public PlayerScore playerScore;
    public TileBase tileBase;
    public TileSelect tileSelect;
     */


    // Start is called before the first frame update
    void Start()
    {
        budget = 1000;
        spent = 0;
        budgetText.text = "Budget: \n$" + budget;
        spentText.text = "Spent: \n" + spent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Undo()
    {
        // The player will be able to undo an tile placement
    }

    public void Redo()
    {
        // The player will be able to redo an undo action
    }

    public void Delete()
    {
        // The player will be able to delete a selected tile.
    }

    public void Finished()
    {
        // The player will be able to confirm their actions and be able
    }


}

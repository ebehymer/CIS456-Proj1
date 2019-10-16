//Script: BudgetManager
//Assignment: Project
//Description: Tracks the player's spending
//Edits made by: Robyn
//Last edited by and date: Robyn 10/2

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BudgetManager : MonoBehaviour
{
    public Text total;
    public Text used;

    public int maxMoney;
    public int usedMoney;

    // Start is called before the first frame update
    void Start()
    {
        total.text = "Budget\n" + maxMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        used.text = "Spent\n" + usedMoney.ToString();

        if (usedMoney > maxMoney)
        {
            used.color = Color.red;
        }
        else used.color = Color.green;
    }
}

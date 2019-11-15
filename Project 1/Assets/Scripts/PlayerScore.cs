//Script: PlayerScore
//Assignment: Project
//Description: Calculates the score at the end of the level using the budget/spent and party members
//Edits made by: Nicole
//Last edited by and date: Nicole 11/14

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private float score;
    [SerializeField] private int budget; //get reference for the budget
    [SerializeField] private int spent; //get reference for spent
    private CharacterBase[] party;
    [SerializeField] int membersAlive = 0;
    Text membersKilled, budgetScore, totalScore;

    // Start is called before the first frame update
    void Start()
    {
        budget = GameObject.Find("Budget Manager").GetComponent<BudgetManager>().maxMoney;
        spent = GameObject.Find("Budget Manager").GetComponent<BudgetManager>().usedMoney;

        membersKilled = GameObject.Find("PM Amount").GetComponent<Text>();
        budgetScore = GameObject.Find("B Amount").GetComponent<Text>();
        totalScore = GameObject.Find("Scored Amount").GetComponent<Text>();
        CalculateMembersAlive();
        CalculateScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CalculateMembersAlive()
    {
        //foreach member in partymember check their health
        //if !isAlive then add to score, otherwise subtract
        party = GameObject.Find("Party").GetComponent<PartyBase>().GetPartyMembers();
        //party = GameObject.GetComponent<PartyBase>().GetPartyMembers();
        foreach (CharacterBase member in party)
        {
            if (member.isAlive == false) //if memeber is dead
            {
                //Add to members
                membersAlive++;
            }
        }
        Debug.Log("Members Alive: " + membersAlive);
    }

    IEnumerator DisplayScore()
    {
        //Display members killed first
        score = 50 * membersAlive;
        Debug.Log("Score after members: " + score);
        membersKilled.text = "(50 x " + membersAlive + " )";
        yield return new WaitForSeconds(2);

        Debug.Log("HERE IT IS---------Budget: " + budget + "--Spent: "+ spent);

        //Display the budget percentage second
        score += ((spent/budget)*100);
        Debug.Log("Spent / budget: " + ((spent / budget)*100));
        budgetScore.text = ((spent / budget)*100) + "%";
        yield return new WaitForSeconds(2);

        //Display the total score
        totalScore.text = score.ToString();
    }

    public void CalculateScore()
    {
        StartCoroutine(DisplayScore());
    }
}

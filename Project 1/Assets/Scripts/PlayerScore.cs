//Script: PlayerScore
//Assignment: Project
//Description: Calculates the score at the end of the level using the budget/spent and party members
//Edits made by: Nicole
//Last edited by and date: Nicole 9/23

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int budget; //get reference for the budget
    [SerializeField] private int spent; //get reference for spent
    private CharacterBase[] party;
    [SerializeField] int membersAlive = 0;
    Text membersKilled, budgetScore, totalScore;

    // Start is called before the first frame update
    void Start()
    {
        membersKilled = GameObject.Find("PM Amount").GetComponent<Text>();
        budgetScore = GameObject.Find("B Amount").GetComponent<Text>();
        totalScore = GameObject.Find("Scored Amount").GetComponent<Text>();
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
        party = gameObject.GetComponent<PartyBase>().GetPartyMembers();
        foreach (CharacterBase member in party)
        {
            if (member.isAlive == false) //if memeber is dead
            {
                //Add to members
                membersAlive++;
            }
        }
    }

    IEnumerator DisplayScore()
    {
        //Display members killed first
        score = 50 * membersAlive;
        membersKilled.text = "(50 x " + membersAlive.ToString() + " )";
        yield return new WaitForSeconds(2);

        //Display the budget percentage second
        score += Mathf.FloorToInt(spent/budget);
        budgetScore.text = Mathf.FloorToInt(spent / budget).ToString() + "%";
        yield return new WaitForSeconds(2);

        //Display the total score
        totalScore.text = score.ToString();
    }

    public void CalculateScore()
    {
        CalculateMembersAlive();
        StartCoroutine(DisplayScore());
    }
}

//Script: PlayerScore
//Assignment: Project
//Description: Calculates the score at the end of the level using the budget/spent and party members
//Edits made by: Nicole
//Last edited by and date: Nicole 9/23

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int budget; //get reference for the budget
    [SerializeField] private int spent; //get reference for spent
    private CharacterBase[] party;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculateScore()
    {
        if(spent < budget) //Good score
        {
            //Add to score
        }
        else if(spent > budget) //Bad score
        {
            //Subtract from score

        }
        //If spent == budget it stays the same

        //foreach member in partymember check their health
        //if !isAlive then add to score, otherwise subtract
        party = gameObject.GetComponent<PartyBase>().GetPartyMembers();
        foreach (CharacterBase member in party)
        {
            if(member.isAlive == false) //if memeber is dead
            {
                //Add to score

            }
            else
            {
                //Subtract from score

            }
        }

    }
}

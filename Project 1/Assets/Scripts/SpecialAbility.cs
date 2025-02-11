﻿//Script: PlayerScore
//Assignment: Project
//Description: Allows the player to try and kill the rest of the party members.
//Edits made by: Nicole
//Last edited by and date: Nicole 11/14

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbility : MonoBehaviour
{
    private PartyBase partyScript;
    [SerializeField] private int abilityDamage = 20;

    private MenuManager menMan;
    private BudgetManager bugMan;

    // Start is called before the first frame update
    void Start()
    {
        bugMan = GameObject.Find("Budget Manager").GetComponent<BudgetManager>();
        menMan = GetComponent<MenuManager>();
        partyScript = GameObject.Find("Party").GetComponent<PartyBase>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitToChange()
    {
        yield return new WaitForSeconds(1f);
        menMan.GoToScore();
    }

    public void ActivateAbility()
    {
        partyScript.DealDamage(abilityDamage);
        StartCoroutine(WaitToChange());
        //menMan.GoToScore();
    }
}

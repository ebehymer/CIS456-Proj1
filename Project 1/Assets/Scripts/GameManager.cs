using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script: GameManager
//Assignment: Project
//Description: Handles available actions based on the game State
//Edits made by: Robyn
//Last edited by and date: Robyn 11/6


public class GameManager : MonoBehaviour
{
    public enum GameState { placing, running, menu };

    public static GameState current;

    // Start is called before the first frame update
    void Start()
    {
        current = GameState.placing;
    }
    
}

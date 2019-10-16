using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { placing, running };

    public static GameState current;

    // Start is called before the first frame update
    void Start()
    {
        current = GameState.placing;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelect : MonoBehaviour
{
    public enum Tile { Enemy, Magic, Trap };

    public Tile type;

    TilePlacementTest man;

    private void Start()
    {
        man = GameObject.Find("Tile Manager").GetComponent<TilePlacementTest>(); 
    }

    private void OnMouseDown()
    {
        switch (type)
        {
            case Tile.Enemy:

                man.placing = man.enemy;
                break;
            case Tile.Magic:
                man.placing = man.magic;
                break;
            case Tile.Trap:
                man.placing = man.traps;
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelect : MonoBehaviour
{
    public TileBase.tileType type;

    TilePlacementTest man;

    private void Start()
    {
        man = GameObject.Find("Tile Manager").GetComponent<TilePlacementTest>(); 
    }

    private void OnMouseDown()
    {
        switch (type)
        {
            case TileBase.tileType.enemy:

                man.placing = man.enemy;
                break;
            case TileBase.tileType.magic:
                man.placing = man.magic;
                break;
            case TileBase.tileType.trap:
                man.placing = man.trap;
                break;
        }
    }
}

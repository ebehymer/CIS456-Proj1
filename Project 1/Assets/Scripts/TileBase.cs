//Script: TileBase
//Assignment: Project
//Description: Handles the creation of tiles
//Edits made by: Nicole
//Last edited by and date: Nicole 9/23

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBase : MonoBehaviour
{
    public enum tileType { trap, magic, enemy };

    [SerializeField] protected tileType type;
    [SerializeField] protected int tileCost;
    [SerializeField] protected int tileDamage;
    [SerializeField] protected bool steppedOn;

    protected string tileName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public tileType GetTileType()
    {
        return type;
    }

    public void SetTileType(tileType tt)
    {
        type = tt;
    }

    public int GetTileCost()
    {
        return tileCost;
    }

    public void SetTileCost(int amount)
    {
        tileCost = amount;
    }

    public string GetTileName()
    {
        return tileName;
    }

    public void SetTileName()
    {
        tileName = GetTileType() + " Tile";
    }

    public int GetTileDamage()
    {
        return tileDamage;
    }

    public void SetTileDamage(int damage)
    {
        tileDamage = damage;
    }

}

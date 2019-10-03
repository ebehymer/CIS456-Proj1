//Script: TilePlacementTest
//Assignment: Project
//Description: Handles the Placement of obstacles on the path
//Edits made by: Robyn
//Last edited by and date: Robyn 10/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Action
{
   public Vector3Int position;
    public TileBase tile;

    public Action(Vector3Int pos, TileBase t)
    {
        position = pos;
        tile = t;
        
    }
    public Action(Vector3Int pos)
    {
        position = pos;
    }
}

public class TrapTile : TileBase
{
    public TrapTile()
    {
        SetTileCost(50);
        SetTileType(tileType.trap);
        SetTileDamage(5);
    }
}

public class MagicTile : TileBase
{
    public MagicTile()
    {
        SetTileCost(150);
        SetTileType(tileType.magic);
        SetTileDamage(15);
    }
}

public class EnemyTile : TileBase
{
    public EnemyTile()
    {
        SetTileCost(100);
        SetTileType(tileType.enemy);
        SetTileDamage(10);
    }
}

public class TilePlacementTest : MonoBehaviour
{

    public Tilemap Wall, Path, obstacle, Misc;
    public Tile placing, trap, magic, enemy, placeGlow, deleteGlow;
    public Grid grid;

    public bool deleting;
    public bool lastDeleted;

    public Text info;

    BudgetManager man;

    TrapTile traptile = new TrapTile();
    MagicTile magictile = new MagicTile();
    EnemyTile enemytile = new EnemyTile();

    public List<Action> list = new List<Action>();
    public int numActions;
    Vector3Int oldCoord;

    private void Start()
    {
        man = GameObject.Find("Budget Manager").GetComponent<BudgetManager>();
    }

    private void Update()
    {
        if(placing == trap)
        {
            info.text = "Tile Info:\n" + "Trap\nCost: " + traptile.GetTileCost() + "\nDamage: " + traptile.GetTileDamage();
        }
        else if (placing == magic)
        {
            info.text = "Tile Info:\n" + "Magic\nCost: " + magictile.GetTileCost() + "\nDamage: " + magictile.GetTileDamage();
        }
        else if (placing == enemy)
        {
            info.text = "Tile Info:\n" + "Enemy\nCost: " + enemytile.GetTileCost() + "\nDamage: " + enemytile.GetTileDamage();
        }


        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int coordinate = grid.WorldToCell(pos);

        if (Input.GetMouseButton(0))
        {
            if (!deleting)
            {
                if (Path.GetTile(coordinate) && !obstacle.GetTile(coordinate))
                {

                    obstacle.SetTile(coordinate, placing);

                    numActions++;

                    if (lastDeleted) lastDeleted = false;

                    if (placing == trap)
                    {
                        list.Add(new Action(coordinate, new TrapTile()));
                        man.usedMoney += list[numActions-1].tile.GetTileCost();
                    }
                    else if (placing == magic)
                    {

                        list.Add(new Action(coordinate, new MagicTile()));
                        man.usedMoney += list[numActions-1].tile.GetTileCost();
                    }
                    else
                    {
                        list.Add(new Action(coordinate, new EnemyTile()));
                        man.usedMoney += list[numActions-1].tile.GetTileCost();
                    }
                    undone.Clear();
                }
            }
            else
            {
                if (obstacle.GetTile(coordinate))
                {
                    obstacle.SetTile(coordinate, null);
                    numActions--;
                    lastDeleted = true;


                    for (int i = 0; i < list.Count; i++)
                    {
                        if (!obstacle.GetTile(list[i].position))
                        {


                            if (list[i].tile.GetTileType() == TileBase.tileType.trap)
                            {
                                man.usedMoney -= list[i].tile.GetTileCost();
                            }
                            else if (list[i].tile.GetTileType() == TileBase.tileType.magic)
                            {
                                man.usedMoney -= list[i].tile.GetTileCost();
                            }
                            else man.usedMoney -= list[i].tile.GetTileCost();

                            undone.Push(list[i]);
                            list.RemoveAt(i);
                        }
                    }
                }
            }
        }

        if (Wall.GetTile(coordinate))
        {
            if (deleting)
            {
                Misc.SetTile(coordinate, deleteGlow);
            }
            else Misc.SetTile(coordinate, placeGlow);
        }
        if(oldCoord != coordinate)
        {
            Misc.SetTile(oldCoord, null);
            oldCoord = coordinate;
        }

        Debug.Log(list.Count);
    }



    Stack<Action> undone = new Stack<Action>();
    int numDeleted;
    
    public void Undo()
    {
        if (lastDeleted)
        {
            Action redone = undone.Pop();
            list.Add(redone);


            numDeleted++;
            numActions++;

            if (redone.tile.GetTileType() == TileBase.tileType.trap)
            {
                man.usedMoney += redone.tile.GetTileCost();

                obstacle.SetTile(redone.position, trap);
            }
            else if (redone.tile.GetTileType() == TileBase.tileType.magic)
            {
                man.usedMoney += redone.tile.GetTileCost();

                obstacle.SetTile(redone.position, magic);
            }
            else
            {
                man.usedMoney += redone.tile.GetTileCost();

                obstacle.SetTile(redone.position, enemy);
            }
        }
        else if (numActions > 0)
        {
            undone.Push(list[numActions - 1]);
            obstacle.SetTile(list[numActions - 1].position, null);

            if (list[numActions - 1].tile.GetTileType() == TileBase.tileType.trap)
            {
                man.usedMoney -= list[numActions-1].tile.GetTileCost();
            }
            else if (list[numActions - 1].tile.GetTileType() == TileBase.tileType.magic)
            {
                man.usedMoney -= list[numActions - 1].tile.GetTileCost();
            }
            else
            {
                man.usedMoney -= list[numActions - 1].tile.GetTileCost();
            }

            list.RemoveAt(numActions - 1);
            numActions--;
        }
    }

    public void Redo()
    {
        if (lastDeleted)
        {
            if (numDeleted > 0)
            {
                undone.Push(list[numActions - 1]);
                obstacle.SetTile(list[numActions - 1].position, null);


                if (list[numActions - 1].tile.GetTileType() == TileBase.tileType.trap)
                {
                    man.usedMoney -= list[numActions - 1].tile.GetTileCost();
                }
                else if (list[numActions - 1].tile.GetTileType() == TileBase.tileType.magic)
                {
                    man.usedMoney -= list[numActions - 1].tile.GetTileCost();
                }
                else man.usedMoney -= list[numActions - 1].tile.GetTileCost();

                list.RemoveAt(numActions - 1);
                numActions--;
                numDeleted--;
            }
        }
        else if (undone.Count > 0)
        {
            Action redone = undone.Pop();
            list.Add(redone);

            if (redone.tile.GetTileType() == TileBase.tileType.trap)
            {
                man.usedMoney += redone.tile.GetTileCost();

                obstacle.SetTile(redone.position, trap);
            }
            else if (redone.tile.GetTileType() == TileBase.tileType.magic)
            {
                man.usedMoney += redone.tile.GetTileCost();

                obstacle.SetTile(redone.position, magic);
            }
            else
            {
                man.usedMoney += redone.tile.GetTileCost();

                obstacle.SetTile(redone.position, enemy);
            }

            numActions++;
        }
    }


    public void Delete()
    {
        deleting = !deleting;
    }
}

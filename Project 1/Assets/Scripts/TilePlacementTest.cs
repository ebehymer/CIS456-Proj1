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
    public Tile type;

    public Action(Vector3Int pos, Tile tile)
    {
        position = pos;
        type = tile;
        
    }
    public Action(Vector3Int pos)
    {
        position = pos;
    }
}


public class TilePlacementTest : MonoBehaviour
{

    public Tilemap Wall, Path, Trap, Misc;
    public Tile placing, traps, magic, enemy, glow;
    public Grid grid;

    public bool deleting;
    public bool lastDeleted;

    public Text info;

    BudgetManager man;

    public List<Action> list = new List<Action>();
    public int numActions;
    Vector3Int oldCoord;

    private void Start()
    {
        man = GameObject.Find("Budget Manager").GetComponent<BudgetManager>();
    }

    private void Update()
    {
        info.text = "Tile Info:\n" + placing.name;
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int coordinate = grid.WorldToCell(pos);

        if (Input.GetMouseButton(0))
        {
            if (!deleting)
            {
                if (Path.GetTile(coordinate) && !Trap.GetTile(coordinate))
                {

                    Trap.SetTile(coordinate, placing);
                    list.Add(new Action(coordinate, placing));

                    numActions++;

                    if (lastDeleted) lastDeleted = false;

                    if (placing == traps)
                    {
                        man.usedMoney += 50;
                    }
                    else if (placing == magic)
                    {
                        man.usedMoney += 150;
                    }
                    else man.usedMoney += 100;

                    undone.Clear();
                }
            }
            else
            {
                if (Trap.GetTile(coordinate))
                {
                    Trap.SetTile(coordinate, null);
                    numActions--;
                    lastDeleted = true;


                    for (int i = 0; i < list.Count; i++)
                    {
                        if (!Trap.GetTile(list[i].position))
                        {


                            if (list[i].type == traps)
                            {
                                man.usedMoney -= 50;
                            }
                            else if (list[i].type == magic)
                            {
                                man.usedMoney -= 150;
                            }
                            else man.usedMoney -= 100;

                            undone.Push(list[i]);
                            list.RemoveAt(i);
                        }
                    }
                }
            }
        }

        if (Wall.GetTile(coordinate))
        {
            Misc.SetTile(coordinate, glow);
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

            Trap.SetTile(redone.position, redone.type);

            numDeleted++;
            numActions++;

            if (redone.type == traps)
            {
                man.usedMoney += 50;
            }
            else if (redone.type == magic)
            {
                man.usedMoney += 150;
            }
            else man.usedMoney += 100;
        }
        else if (numActions > 0)
        {
            undone.Push(list[numActions-1]);
            Trap.SetTile(list[numActions-1].position, null);

            if (list[numActions-1].type == traps)
            {
                man.usedMoney -= 50;
            }
            else if (list[numActions - 1].type == magic)
            {
                man.usedMoney -= 150;
            }
            else man.usedMoney -= 100;


            list.RemoveAt(numActions-1);
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
                Trap.SetTile(list[numActions - 1].position, null);


                if (list[numActions - 1].type == traps)
                {
                    man.usedMoney -= 50;
                }
                else if (list[numActions - 1].type == magic)
                {
                    man.usedMoney -= 150;
                }
                else man.usedMoney -= 100;

                list.RemoveAt(numActions - 1);
                numActions--;
                numDeleted--;
            }
        }
        else if (undone.Count > 0)
        {
            Action redone = undone.Pop();
            list.Add(redone);

            Trap.SetTile(redone.position, redone.type);


            if (redone.type == traps)
            {
                man.usedMoney += 50;
            }
            else if (redone.type == magic)
            {
                man.usedMoney += 150;
            }
            else man.usedMoney += 100;

            numActions++;
        }
    }


    public void Delete()
    {
        deleting = !deleting;
    }
}

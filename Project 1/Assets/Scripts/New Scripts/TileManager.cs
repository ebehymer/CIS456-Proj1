using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script: TileManager
//Assignment: Project
//Description: Handles the buying and selling of tiles
//Edits made by: Robyn
//Last edited by and date: Robyn 11/6

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

public class TileManager : MonoBehaviour
{

    public GameObject placing, trap, magic, enemy, placeGlow, deleteGlow;

    GameObject glow;

    public bool deleting;
    public bool lastDeleted;

    public Text info;

    BudgetManager man;

    public static TrapTile traptile = new TrapTile();
    public static MagicTile magictile = new MagicTile();
    public static EnemyTile enemytile = new EnemyTile();

    Vector2 mousePos;
    RaycastHit2D hit, tile;

    public LayerMask paths, placable;

    public int numActions = 0;

    List<Vector2> placed = new List<Vector2>();

    Stack<GameObject> actions = new Stack<GameObject>();
    Stack<GameObject> undone = new Stack<GameObject>();

    Stack<GameObject> deleted = new Stack<GameObject>();
    public AudioSource PlacingSound, SellingSound;

    // Start is called before the first frame update
    void Start()
    {
        man = GameObject.Find("Budget Manager").GetComponent<BudgetManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (placing == trap)
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

        if (GameManager.current == GameManager.GameState.placing)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, paths);
            tile = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, placable);
        }
        if (hit)
        {
            if(glow == null)
            {
                if (deleting)
                {
                    glow = Instantiate(deleteGlow, hit.collider.transform.position, Quaternion.identity);
                }
                else glow = Instantiate(placeGlow, hit.collider.transform.position, Quaternion.identity);
            }
            else if(glow.transform.position != hit.collider.gameObject.transform.position)
            {
                Destroy(glow);
                if (deleting)
                {
                    glow = Instantiate(deleteGlow, hit.collider.transform.position, Quaternion.identity);
                }
                else glow = Instantiate(placeGlow, hit.collider.transform.position, Quaternion.identity);
            }

            if (!deleting)
            {
                if (Input.GetMouseButton(0) && !placed.Contains(hit.collider.transform.position))
                {

                    numActions++;

                    if (lastDeleted) lastDeleted = false;

                    if(placing == trap)
                    {
                        man.usedMoney += traptile.GetTileCost();
                    }
                    else if (placing == magic)
                    {
                        man.usedMoney += magictile.GetTileCost();
                    }
                    else if (placing == enemy)
                    {
                        man.usedMoney += enemytile.GetTileCost();
                    }

                    actions.Push(Instantiate(placing, hit.collider.transform.position, Quaternion.identity));
                    placed.Add(hit.collider.transform.position);

                    for(int i = 0; i < undoneCount; i++)
                    {
                        Destroy(undone.Pop());
                    }
                    undoneCount = 0;
                    actions.TrimExcess();
                }

                
            }
            else
            {
                if (Input.GetMouseButton(0) && tile)
                {
                    numActions--;
                    undoneCount++;
                    GameObject holder = tile.collider.gameObject;
                    placed.Remove(hit.collider.transform.position);
                    undone.Push(holder);
                    holder.GetComponent<SpriteRenderer>().enabled = false;
                    holder.GetComponent<BoxCollider2D>().enabled = false;
                    lastDeleted = true;
                    actions.TrimExcess();
                    if(holder.name == "Trap(Clone)")
                    {
                        man.usedMoney -= traptile.GetTileCost();
                    }
                    else if (holder.name == "Magic(Clone)")
                    {
                        man.usedMoney -= magictile.GetTileCost();
                    }
                    else if(holder.name == "Enemy(Clone)")
                    {
                        man.usedMoney -= enemytile.GetTileCost();
                    }
                }
            }
        } else
        {
            if(glow != null)
            {
                Destroy(glow);
            }
        }
    }


    int undoneCount = 0;
    int numDeleted = 0;
    public void Undo()
    {
        if (lastDeleted)
        {
            GameObject redone = undone.Pop();
            actions.Push(redone);
            redone.GetComponent<SpriteRenderer>().enabled = true;
            redone.GetComponent<BoxCollider2D>().enabled = true;
            numActions++;
            undoneCount--;
            numDeleted++;

            if(redone.name == "Trap(Clone)")
            {
                man.usedMoney += traptile.GetTileCost();
            }
            else if (redone.name == "Magic(Clone)")
            {
                man.usedMoney += magictile.GetTileCost();
            }
            else if (redone.name == "Enemy(Clone)")
            {
                man.usedMoney += enemytile.GetTileCost();
            }
        }
        else if (numActions > 0)
        {
            GameObject holder = actions.Pop();
            undone.Push(holder);
            holder.GetComponent<SpriteRenderer>().enabled = false;
            undoneCount++;
            numActions--;
            if(holder.name == "Trap(Clone)")
            {
                man.usedMoney -= traptile.GetTileCost();
            }
            else if (holder.name == "Magic(Clone)")
            {
                man.usedMoney -= magictile.GetTileCost();
            }
            else if (holder.name == "Enemy(Clone)")
            {
                man.usedMoney -= enemytile.GetTileCost();
            }
        }
    }

    public void Redo()
    {
        if (lastDeleted)
        {
            if (numDeleted > 0)
            {
                GameObject holder = actions.Pop();
                undone.Push(holder);
                holder.GetComponent<SpriteRenderer>().enabled = false;
                holder.GetComponent<BoxCollider2D>().enabled = false;
                undoneCount++;
                numActions--;
                numDeleted--;
                if (holder.name == "Trap(Clone)")
                {
                    man.usedMoney -= traptile.GetTileCost();
                }
                else if (holder.name == "Magic(Clone)")
                {
                    man.usedMoney -= magictile.GetTileCost();
                }
                else if (holder.name == "Enemy(Clone)")
                {
                    man.usedMoney -= enemytile.GetTileCost();
                }
            }
        }
        else if(undoneCount > 0)
        {
            GameObject holder = undone.Pop();
            actions.Push(holder);
            holder.GetComponent<SpriteRenderer>().enabled = true;
            undoneCount--;
            numActions++;
            if(holder.name == "Trap(Clone)")
            {
                man.usedMoney += traptile.GetTileCost();
            } 
            else if (holder.name == "Magic(Clone)")
            {
                man.usedMoney += magictile.GetTileCost();
            }
            else if (holder.name == "Enemy(Clone)")
            {
                man.usedMoney += enemytile.GetTileCost();
            }
        }
    }

    public void Selling()
    {
        deleting = !deleting;
    }
}

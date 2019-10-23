//Script: FollowPath
//Assignment: Project
//Description: Guides the Party along the path of the dungeon
//Edits made by: Robyn
//Last edited by and date: Robyn 10/9

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
//Emma has worked on this
public class FollowPath : MonoBehaviour
{
    public Tilemap path, walked, danger;
    Vector3Int pos;
    public Tile floor;
    public Grid grid;

    public Text failure;

    bool stepped = false;

    List<Vector3Int> posList = new List<Vector3Int>();

    public Vector2 startPos;

    List<Action> temp;
    TilePlacementTest man;

    Vector3Int end;

    float stepTime = .1f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        grid = FindObjectOfType<Grid>();

        end = grid.WorldToCell(GameObject.Find("End").transform.position);
        man = GameObject.Find("Tile Manager").GetComponent<TilePlacementTest>();
        failure.text = "";
    }

    private void FixedUpdate()
    {
        if (path.GetTile(pos))
        {
            path.SetTile(pos, null);
            walked.SetTile(pos, floor);
        }

        if (danger.GetTile(pos) && !stepped)
        {

            danger.SetTile(pos, null);

            for (int i = 0; i < TilePlacementTest.list.Count; i++)
            {
                
                if (!danger.GetTile(TilePlacementTest.list[i].position) && !stepped)
                {


                    if (TilePlacementTest.list[i].tile.GetTileType() == TileBase.tileType.trap)
                    {
                        GetComponent<PartyBase>().DealDamage(TilePlacementTest.traptile.GetTileDamage());
                    }
                    else if (TilePlacementTest.list[i].tile.GetTileType() == TileBase.tileType.magic)
                    {
                        GetComponent<PartyBase>().DealDamage(TilePlacementTest.magictile.GetTileDamage());
                    }
                    else GetComponent<PartyBase>().DealDamage(TilePlacementTest.enemytile.GetTileDamage());

                    stepped = true;
                }
            }
        }

        if (GetComponent<PartyBase>().allDead)
        {
            StopCoroutine(Walk());

        }
        
    }


    public void Begin()
    {
        if(GameManager.current == GameManager.GameState.placing)
        StartCoroutine(Walk());
    }

    bool walking;

    private IEnumerator Walk()
    {
        pos = grid.WorldToCell(GameObject.Find("Start").transform.position);
        path.SetTile(end, floor);

        stepTime = .5f;

        temp = TilePlacementTest.list;

        GameManager.current = GameManager.GameState.running;

        transform.position = grid.GetCellCenterLocal(pos);


        yield return new WaitForSeconds(.1f);

        posList.Add(pos);
        posList.Add(end);

        path.SetTile(pos, null);
        walked.SetTile(pos, floor);

        Vector3Int xPos, xNeg, yPos, yNeg;
        while(pos != end && !GetComponent<PartyBase>().allDead)
        {
            xNeg = new Vector3Int(pos.x - 1, pos.y, pos.z);
            xPos = new Vector3Int(pos.x + 1, pos.y, pos.z);
            yPos = new Vector3Int(pos.x, pos.y + 1, pos.z);
            yNeg = new Vector3Int(pos.x, pos.y - 1, pos.z);
            if (path.HasTile(xNeg))
            {
                pos = xNeg;
                //transform.position = grid.GetCellCenterLocal(pos);
                transform.position = Vector3.Lerp(transform.position, grid.GetCellCenterLocal(pos), 1);
            }
            else if (path.HasTile(xPos))
            {
                pos = xPos;
                //transform.position = grid.GetCellCenterLocal(pos);
                transform.position = Vector3.Lerp(transform.position, grid.GetCellCenterLocal(pos), 1);
            }
            else if (path.HasTile(yPos))
            {
                pos = yPos;
                //transform.position = grid.GetCellCenterLocal(pos);
                transform.position = Vector3.Lerp(transform.position, grid.GetCellCenterLocal(pos), 1);
            }
            else if (path.HasTile(yNeg))
            {
                pos = yNeg;
                //transform.position = grid.GetCellCenterLocal(pos);
                transform.position = Vector3.Lerp(transform.position, grid.GetCellCenterLocal(pos), 1);
            }
            else
            {

            }
            stepped = false;

            posList.Add(pos);
            yield return new WaitForSeconds(stepTime);

        }
        Debug.Log("Reached End");
        
        if(!GetComponent<PartyBase>().allDead)
        {
            failure.text = "Quest Failed";
            yield return new WaitForSeconds(2.0f);
            failure.text = "";
            Restart();
        }
        
        yield return null;
    }

    public void Restart()
    {
        GameManager.current = GameManager.GameState.placing;
        transform.position = startPos;


        for (int i = 0; i < posList.Count; i++)
        {
            walked.SetTile(posList[i], null);
            path.SetTile(posList[i], floor);
        }
        walked.SetTile(end, null);
        path.SetTile(end, floor);

        posList.Clear();

        for (int i = 0; i < GetComponent<PartyBase>().GetPartyMembers().Length; i++)
        {
            GetComponent<PartyBase>().GetPartyMembers()[i].SetCharacterHealth(GetComponent<PartyBase>().GetPartyMembers()[i].maxHealth);
        }

        GetComponent<PartyBase>().updateText();

        for (int i = 0; i < TilePlacementTest.list.Count; i++)
        {
            Debug.Log(i);
            if (TilePlacementTest.list[i].tile.GetTileType() == TileBase.tileType.trap)
            {
                danger.SetTile(TilePlacementTest.list[i].position, man.trap);
            }
            else if (TilePlacementTest.list[i].tile.GetTileType() == TileBase.tileType.magic)
            {
                danger.SetTile(TilePlacementTest.list[i].position, man.magic);
            }
            else danger.SetTile(TilePlacementTest.list[i].position, man.enemy);

        }
    }

    public void timeChange(Text t)
    {
        if(t.text == "Slow")
        {
            stepTime = .5f;
            t.text = "Medium";
        } else if (t.text == "Medium")
        {
            stepTime = .1f;
            t.text = "Fast";
        } else if (t.text == "Fast")
        {
            stepTime = .9f;
            t.text = "Slow";
        }
    }
}

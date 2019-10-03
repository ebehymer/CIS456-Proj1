//Script: FollowPath
//Assignment: Project
//Description: Guides the Party along the path of the dungeon
//Edits made by: Robyn
//Last edited by and date: Robyn 10/1

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Emma has worked on this
public class FollowPath : MonoBehaviour
{
    public Tilemap path, walked, danger;
    Vector3Int pos;
    public Tile floor;
    public Grid grid;

    bool stepped = false;

    Vector3Int end;
    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<Grid>();
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

            stepped = true;
            for (int i = 0; i < TilePlacementTest.list.Count; i++)
            {
                
                if (!danger.GetTile(TilePlacementTest.list[i].position))
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



                    TilePlacementTest.list.RemoveAt(i);
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
        if(!walking)
        StartCoroutine(Walk());
    }

    bool walking;

    private IEnumerator Walk()
    {
        walking = true;
        pos = grid.WorldToCell(GameObject.Find("Start").transform.position);
        end = grid.WorldToCell(GameObject.Find("End").transform.position);

        transform.position = grid.GetCellCenterLocal(pos);

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
            yield return new WaitForSeconds(.5f);

        }


        yield return null;
    }
}

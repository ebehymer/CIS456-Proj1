using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FollowPath : MonoBehaviour
{
    public Tilemap path, walked;
    Vector3Int pos;
    public Tile floor;
    public Grid grid;

    Vector3Int end;
    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<Grid>();
    }

    private void FixedUpdate()
    {
        Debug.Log(pos);
        if (path.GetTile(pos))
        {
            path.SetTile(pos, null);
            walked.SetTile(pos, floor);
        }
        
    }

    public void Begin()
    {
        StartCoroutine(Walk());
    }



    private IEnumerator Walk()
    {

        pos = grid.WorldToCell(GameObject.Find("Start").transform.position);
        end = grid.WorldToCell(GameObject.Find("End").transform.position);

        transform.position = grid.GetCellCenterLocal(pos);

        path.SetTile(pos, null);
        walked.SetTile(pos, floor);

        Vector3Int xPos, xNeg, yPos, yNeg;
        while(pos != end)
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

            yield return new WaitForSeconds(.5f);
        }


        yield return null;
    }
}

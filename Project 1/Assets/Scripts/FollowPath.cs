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
        pos = grid.WorldToCell(GameObject.Find("Start").transform.position);
        end = grid.WorldToCell(GameObject.Find("End").transform.position);
        transform.position = grid.GetCellCenterLocal(pos);
        StartCoroutine(Walk());
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



    private IEnumerator Walk()
    {
        Vector3Int nextPos;
        while(pos != end)
        {
            nextPos = new Vector3Int(pos.x - 1, pos.y, pos.z);
            if (path.HasTile(nextPos))
            {
                pos = nextPos;
                //transform.position = grid.GetCellCenterLocal(pos);
                transform.position = Vector3.Lerp(transform.position, grid.GetCellCenterLocal(pos), 1);
            }

            yield return new WaitForSeconds(.5f);
        }


        yield return null;
    }
}

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

    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<Grid>();
        pos = grid.WorldToCell(GameObject.Find("Start").transform.position);
        transform.position = grid.GetCellCenterLocal(pos);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacementTest : MonoBehaviour
{

    public Tilemap Wall, Path, Trap, Misc;
    public Tile placing, traps, magic, enemy, glow;
    public Grid grid;

    Vector3Int oldCoord;

    private void Update()
    {

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int coordinate = grid.WorldToCell(pos);

        if (Input.GetMouseButton(0))
        {
            if (Path.GetTile(coordinate))
            {
                Trap.SetTile(coordinate, placing);
            }
        }
        if (Input.GetMouseButton(1))
        {
            if (Trap.GetTile(coordinate))
            {
                Trap.SetTile(coordinate, null);
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
    }
}

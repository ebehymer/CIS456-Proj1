using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public int[,] data
    {
        get; private set;
    }

    public GameObject tileRoad;
    public GameObject wallTile;
    public GameObject endTile;

    private MazeGeneratorHelper dataGenerator;

    private void Awake()
    {
        dataGenerator = new MazeGeneratorHelper();
        data = new int[,]
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
        };
    }

    private void Start()
    {
        Generate(12, 20);
    }

    private void Generate(int numRows, int numColumns)
    {
        if (numRows % 2 == 0 && numColumns % 2 == 0)
        {
            Debug.LogError("Odd numbers work better for dungeon size.");
        }

        data = dataGenerator.FromDimensions(numRows, numColumns);

        for(int i = 0; i < numRows; i++)
        {
            for(int j = 0; j < numColumns; j++)
            {
                if(data[i, j] == 0)
                {
                    GameObject ins = Instantiate(tileRoad);
                    ins.transform.position = new Vector2(j, i);
                } else
                {
                    GameObject ins = Instantiate(wallTile);
                    ins.transform.position = new Vector2(j, i);
                }
            }
        }
    }




    public bool showDebug;
    void OnGUI()
    {
        //1
        if (!showDebug)
        {
            return;
        }

        //2
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        string msg = "";

        //3
        for (int i = rMax; i >= 0; i--)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    msg += "....";
                }
                else
                {
                    msg += "==";
                }
            }
            msg += "\n";
        }

        //4
        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }
}



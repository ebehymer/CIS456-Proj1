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
    public GameObject endTile, startTile;

    public Vector2 startPos, endPos;

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

                    floors.Add(new Vector2(j, i));
                } else
                {
                    GameObject ins = Instantiate(wallTile);
                    ins.transform.position = new Vector2(j, i);
                }
            }
        }

        for(int i = 3; i < 8; i++)
        {
            if(data[i, 1] == 0)
            {
                GameObject ins = Instantiate(startTile);
                ins.transform.position = startPos = new Vector2(0, i);
                break;
            }
        }
        for(int i = 9; i > 3; i--)
        {
            if(data[i, 18] == 0)
            {
                GameObject ins = Instantiate(endTile);
                ins.transform.position = endPos = new Vector2(19, i);
                break;
            }
        }

        StartCoroutine(FinalizePath(numRows, numColumns));
    }

    public int nextX;
    public int nextY;
    public Vector2 currentPos;

    List<Vector2> floors = new List<Vector2>();

    List<Vector2> finalPath = new List<Vector2>();
    IEnumerator FinalizePath(int numRows, int numColumns)
    {
        finalPath.Add(startPos);
        currentPos = new Vector2(startPos.x -1, startPos.y);
        Vector2 nextPos = currentPos;

        do
        {
            do
            {

                
                int direction = Random.Range(0, 4);

                switch (direction)
                {

                    case 0:
                        nextPos.x += 1;
                        break;
                    case 1:
                        nextPos.x -= 1;
                        break;
                    case 2:
                        nextPos.y += 1;
                        break;
                    case 3:
                        nextPos.y -= 1;
                        break;

                }
                yield return null;


            } while (!floors.Contains(nextPos) && !finalPath.Contains(nextPos));

            if (!floors.Contains(new Vector2(nextPos.x + 1, nextPos.y)) &&
                !floors.Contains(new Vector2(nextPos.x - 1, nextPos.y)) &&
                !floors.Contains(new Vector2(nextPos.x, nextPos.y+1)) &&
                !floors.Contains(new Vector2(nextPos.x, nextPos.y-1)))
            {
               RaycastHit2D hit = Physics2D.BoxCast(nextPos, Vector2.one, 0, Vector2.zero);
                Destroy(hit.collider.gameObject);
                GameObject ins = Instantiate(wallTile);
                ins.transform.position = nextPos;
            }
            else { 
            currentPos = nextPos;
            finalPath.Add(currentPos);
            }
            yield return null;


            if (Vector2.Distance(endPos, currentPos) <= 1) currentPos = endPos;
        } while (currentPos != endPos);

        finalPath.Add(endPos);

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numColumns; j++)
            {
               if(!finalPath.Contains(new Vector2(j, i)))
                {
                    RaycastHit2D hit = Physics2D.BoxCast(new Vector2(j, i), Vector2.one, 0, Vector2.zero);
                    Destroy(hit.collider.gameObject);
                    GameObject ins = Instantiate(wallTile);
                    ins.transform.position = nextPos;
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



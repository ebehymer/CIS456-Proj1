using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public int numWayPoints = 4;
    int numRows=12, numColumns=20;

    Vector2[] wayPoints;

    bool horizontal = false;

    public GameObject start, floor, wall, end, holder;

    List<Vector2> path = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        if (numWayPoints < 4) numWayPoints = 4;
        if (numWayPoints % 2 != 0) numWayPoints -= 1;
        wayPoints = new Vector2[numWayPoints];

        wayPoints[0] = new Vector2(0, Random.Range(1, numRows-1));
        Instantiate(start, wayPoints[0], Quaternion.identity, holder.transform);
        wayPoints[numWayPoints - 1] = new Vector2(numColumns-1, Random.Range(1, numRows-1));
        Instantiate(end, wayPoints[numWayPoints-1], Quaternion.identity, holder.transform);

        GenerateDungeon();
    }
    
    private void GenerateDungeon()
    {
        for(int i = 0; i < numColumns; i++)
        {
            for(int k = 0; k < numRows; k++)
            {
                Instantiate(wall, new Vector2(i, k), Quaternion.identity, holder.transform);
            }
        }



        int X = (int)wayPoints[0].x, Y = (int)wayPoints[0].y;
        for(int i = 1; i < wayPoints.Length-1; i++)
        {
            int sign = Random.Range(0, 2);
            if (i == wayPoints.Length - 2)
            {
                X = (int)wayPoints[i - 1].x;
                Y = (int)wayPoints[i + 1].y;
            }
            else if (horizontal)
            {
                horizontal = false;
                if (Y < (numRows / 2))
                {
                    Y = Y + Random.Range(2, 6);
                }
                else Y -= Random.Range(2, 6);
            }
            else
            {
                horizontal = true;
                if (X < (numColumns * .75))
                {
                    X = X + Random.Range(1, 6);
                }
                else X -= Random.Range(1, 6);
            }
            wayPoints[i] = new Vector2(X, Y);

            Instantiate(floor, wayPoints[i], Quaternion.identity, holder.transform);
            path.Add(wayPoints[i]);

        }


        horizontal = false;
        for(int i = 0; i < wayPoints.Length; i++)
        {
            if (horizontal)
            {
                if(wayPoints[i].y > wayPoints[i + 1].y)
                {

                    for (int k = (int)wayPoints[i].y - 1; k > wayPoints[i + 1].y; k--)
                    {
                        Instantiate(floor, new Vector2(wayPoints[i].x, k), Quaternion.identity, holder.transform);
                        path.Add(new Vector2(wayPoints[i].x, k));
                    }
                } else
                {

                    for (int k = (int)wayPoints[i].y + 1; k < wayPoints[i + 1].y; k++)
                    {
                        Instantiate(floor, new Vector2(wayPoints[i].x, k), Quaternion.identity, holder.transform);
                        path.Add(new Vector2(wayPoints[i].x, k));
                    }
                }

                horizontal = false;
            }
            else
            {
                if (wayPoints[i].x > wayPoints[i + 1].x)
                {

                    for (int k = (int)wayPoints[i].x - 1; k > wayPoints[i + 1].x; k--)
                    {
                        Instantiate(floor, new Vector2(k, wayPoints[i].y), Quaternion.identity, holder.transform);
                        path.Add(new Vector2(k, wayPoints[i].y));
                    }
                }
                else
                {

                    for (int k = (int)wayPoints[i].x + 1; k < wayPoints[i + 1].x; k++)
                    {
                        Instantiate(floor, new Vector2(k, wayPoints[i].y), Quaternion.identity, holder.transform);
                        path.Add(new Vector2(k, wayPoints[i].y));
                    }
                }
                horizontal = true;
            }
        }
    }

}

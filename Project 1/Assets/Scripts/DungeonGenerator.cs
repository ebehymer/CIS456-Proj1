using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public int numWayPoints = 4;
    int numRows=12, numColumns=20;

    Vector2[] wayPoints;
    public GameObject[] wayPointObj;

    bool horizontal = false;

    public GameObject start, floor, wall, end, holder;

    List<Vector2> path = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        if (numWayPoints < 4) numWayPoints = 4;
        if (numWayPoints % 2 != 0) numWayPoints -= 1;
        wayPoints = new Vector2[numWayPoints];
        wayPointObj = new GameObject[numWayPoints];

        wayPoints[0] = new Vector2(0, Random.Range(1, numRows-1));

        wayPointObj[0] = Instantiate(start, wayPoints[0], Quaternion.identity, holder.transform);
        wayPoints[numWayPoints - 1] = new Vector2(numColumns-1, Random.Range(1, numRows-1));
        wayPointObj[numWayPoints-1] = Instantiate(end, wayPoints[numWayPoints-1], Quaternion.identity, holder.transform);

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

            wayPointObj[i] = Instantiate(floor, wayPoints[i], Quaternion.identity, holder.transform);
            

            path.Add(wayPoints[i]);

        }


        horizontal = false;
        for(int i = 0; i < wayPoints.Length; i++)
        {

            /*if (wayPointObj[i].GetComponent<Borders>() != null)
            {
                if (wayPointObj[i].transform.position.y <= (numRows / 2))
                {
                    wayPointObj[i].GetComponent<Borders>().top.SetActive(false);
                }
                else wayPointObj[i].GetComponent<Borders>().bottom.SetActive(false);

                if (wayPointObj[i].transform.position.x <= (numColumns * .75))
                {
                    wayPointObj[i].GetComponent<Borders>().right.SetActive(false);
                }
                else wayPointObj[i].GetComponent<Borders>().left.SetActive(false);
            }*/

            if (horizontal)
            {

                

                if (wayPoints[i].y > wayPoints[i + 1].y)
                {

                    if (wayPointObj[i].GetComponent<Borders>() != null)
                    {
                        wayPointObj[i].GetComponent<Borders>().bottom.SetActive(false);
                       
                    }
                    if (wayPointObj[i + 1].GetComponent<Borders>() != null)
                    {
                        wayPointObj[i + 1].GetComponent<Borders>().top.SetActive(false);
                    }

                    for (int k = (int)wayPoints[i].y - 1; k > wayPoints[i + 1].y; k--)
                    {
                        GameObject ins = Instantiate(floor, new Vector2(wayPoints[i].x, k), Quaternion.identity, holder.transform);
                        ins.GetComponent<Borders>().top.SetActive(false);
                        ins.GetComponent<Borders>().bottom.SetActive(false);

                        ins.transform.localScale = new Vector3(.95f, 1, .95f);
                        path.Add(new Vector2(wayPoints[i].x, k));
                    }
                }
                else
                {
                    if (wayPoints[i].y != wayPoints[i + 1].y)
                    {
                        if (wayPointObj[i].GetComponent<Borders>() != null)
                        {
                            wayPointObj[i].GetComponent<Borders>().top.SetActive(false);

                        }
                        if (wayPointObj[i + 1].GetComponent<Borders>() != null)
                        {
                            wayPointObj[i + 1].GetComponent<Borders>().bottom.SetActive(false);
                        }
                    }
                    for (int k = (int)wayPoints[i].y + 1; k < wayPoints[i + 1].y; k++)
                    {
                        GameObject ins = Instantiate(floor, new Vector2(wayPoints[i].x, k), Quaternion.identity, holder.transform);
                        ins.GetComponent<Borders>().top.SetActive(false);
                        ins.GetComponent<Borders>().bottom.SetActive(false);

                        ins.transform.localScale = new Vector3(.95f, 1, .95f);
                        path.Add(new Vector2(wayPoints[i].x, k));
                    }
                }


                    horizontal = false;
            }
            else
            {
                if (wayPoints[i].x > wayPoints[i + 1].x)
                {

                    if (wayPointObj[i].GetComponent<Borders>() != null)
                    {
                        wayPointObj[i].GetComponent<Borders>().left.SetActive(false);
                        
                    }
                    if (wayPointObj[i + 1].GetComponent<Borders>() != null)
                    {
                        wayPointObj[i + 1].GetComponent<Borders>().right.SetActive(false);
                    }

                    for (int k = (int)wayPoints[i].x - 1; k > wayPoints[i + 1].x; k--)
                    {
                        GameObject ins = Instantiate(floor, new Vector2(k, wayPoints[i].y), Quaternion.identity, holder.transform);
                        ins.GetComponent<Borders>().left.SetActive(false);
                        ins.GetComponent<Borders>().right.SetActive(false);

                        ins.transform.localScale = new Vector3(1, .95f, .95f);
                        path.Add(new Vector2(k, wayPoints[i].y));
                    }
                }
                else
                {
                    if (wayPoints[i].x != wayPoints[i + 1].x)
                    {
                        if (wayPointObj[i].GetComponent<Borders>() != null)
                        {
                            wayPointObj[i].GetComponent<Borders>().right.SetActive(false);

                        }
                        if (wayPointObj[i + 1].GetComponent<Borders>() != null)
                        {
                            wayPointObj[i + 1].GetComponent<Borders>().left.SetActive(false);
                        }
                    }
                    for (int k = (int)wayPoints[i].x + 1; k < wayPoints[i + 1].x; k++)
                    {
                        GameObject ins = Instantiate(floor, new Vector2(k, wayPoints[i].y), Quaternion.identity, holder.transform);
                        
                        ins.transform.localScale = new Vector3(1, .95f, .95f);
                        ins.GetComponent<Borders>().left.SetActive(false);
                        ins.GetComponent<Borders>().right.SetActive(false);


                        path.Add(new Vector2(k, wayPoints[i].y));
                    }
                }
                horizontal = true;
            }
        }
    }
}


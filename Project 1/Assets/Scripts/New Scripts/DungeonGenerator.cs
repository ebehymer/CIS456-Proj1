using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script: DungeonGenerator
//Assignment: Project
//Description: Creates a dungeon based on passed in parameters
//Edits made by: Robyn
//Last edited by and date: Robyn 11/6

public class DungeonGenerator : MonoBehaviour
{
    //The number of turns the dungeon will have
    public int numWayPoints = 4;

    //The number of rows and columns the dungeon can be built in.
    int numRows=12, numColumns=20;

    //Arrays holding the waypoints and objects placed on them
    public Vector2[] wayPoints;
    public GameObject[] wayPointObj;

    //A boolean value that allows us to build the dungeons ourselves
    public bool makeRandom;

    bool horizontal = false;

    //Prefabs to be created for the dungeon
    public GameObject start, floor, wall, end, holder;

    //The overall path that is built
    List<Vector2> path = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        //If the number of waypoints is too low, sets it higher and generates dungeon randomly
        if (numWayPoints < 4)
        {
            numWayPoints = 4;
            makeRandom = true;
        }
        //If an unusable number is passed, sets a usable one and generates dungeon randomly
        if (numWayPoints % 2 != 0)
        {
             numWayPoints -= 1;
             makeRandom = true;
        }

        //If a random dungeon is desired, creates the arrays and sets the start and end points
        if (makeRandom)
        {

            wayPoints = new Vector2[numWayPoints];
            wayPointObj = new GameObject[numWayPoints];

            wayPoints[0] = new Vector2(0, Random.Range(1, numRows - 1));

            wayPointObj[0] = Instantiate(start, wayPoints[0], Quaternion.identity, holder.transform);
            wayPoints[numWayPoints - 1] = new Vector2(numColumns - 1, Random.Range(1, numRows - 1));
            wayPointObj[numWayPoints - 1] = Instantiate(end, wayPoints[numWayPoints - 1], Quaternion.identity, holder.transform);
        } else
        {
            //If user input is used for the array, sets the start and end points as given
            wayPointObj = new GameObject[wayPoints.Length];
            wayPointObj[0] = Instantiate(start, wayPoints[0], Quaternion.identity, holder.transform);
            wayPointObj[numWayPoints - 1] = Instantiate(end, wayPoints[numWayPoints - 1], Quaternion.identity, holder.transform);
        }
        GenerateDungeon();
    }
    
    //A method that generates walls and paths of a dungeon layout
    private void GenerateDungeon()
    {
        //Fills the background with wall objects
        for(int i = 0; i < numColumns; i++)
        {
            for(int k = 0; k < numRows; k++)
            {
                Instantiate(wall, new Vector2(i, k), Quaternion.identity, holder.transform);
            }
        }


        //int holders are used to consistency when laying out points
        int X = (int)wayPoints[0].x, Y = (int)wayPoints[0].y;


        //If a random dungeon is being made, loops through and generates coordinates
        //Based on the number of waypoints desired.
        if (makeRandom)
        {
            for (int i = 1; i < wayPoints.Length - 1; i++)
            {
                int sign = Random.Range(0, 2);

                //if on the last iteration of the loop, set the values to align with the previous and ending waypoints
                if (i == wayPoints.Length - 2)
                {
                    X = (int)wayPoints[i - 1].x;
                    Y = (int)wayPoints[i + 1].y;
                }
                else if (horizontal)
                {
                    //Based on where the previous Y coordinate was, either increases or decreases the position
                    horizontal = false;
                    if (Y < (numRows / 2))
                    {
                        Y = Y + Random.Range(2, 6);
                    }
                    else Y -= Random.Range(2, 6);
                }
                else
                {
                    //If the X position is too close to the right edge of the screen
                    //Sends the X coordinate in a negative direction
                    horizontal = true;
                    if (X < (numColumns * .75))
                    {
                        X = X + Random.Range(1, 6);
                    }
                    else X -= Random.Range(1, 6);
                }
                //Adds the generated points to the Waypoints array, and instantiates the new path piece on the spot.
                wayPoints[i] = new Vector2(X, Y);

                wayPointObj[i] = Instantiate(floor, wayPoints[i], Quaternion.identity, holder.transform);


                path.Add(wayPoints[i]);

            }
        }
        else
        {
            //If a random dungeon is not wanted, just creates path objects at the given points
            for(int i = 1; i < wayPoints.Length-1; i++)
            {
                wayPointObj[i] = Instantiate(floor, wayPoints[i], Quaternion.identity, holder.transform);
            }
        }


        horizontal = false;

        float zPos = 0;

        //This loop goes through the waypoint objects that were created, and sets
        //their borders either on or off depending on which direction the path
        //flows from their position. 
        //This loop also fills in the positions between each waypoint to create
        //The connected path of the dungeon
        for(int i = 0; i < wayPoints.Length-1; i++)
        {

            if (horizontal)
            {
                if (wayPoints[i].y > wayPoints[i + 1].y)
                {

                    //if (wayPointObj[i].GetComponent<Borders>() != null)
                    //{
                    //    wayPointObj[i].GetComponent<Borders>().bottom.SetActive(false);
                       
                    //}
                    //if (wayPointObj[i + 1].GetComponent<Borders>() != null)
                    //{
                    //    wayPointObj[i + 1].GetComponent<Borders>().top.SetActive(false);
                    //}

                    for (int k = (int)wayPoints[i].y - 1; k > wayPoints[i + 1].y; k--)
                    {
                        GameObject ins = Instantiate(floor, new Vector3(wayPoints[i].x, k), Quaternion.identity, holder.transform);
                        zPos -= .1f;
                        //ins.GetComponent<Borders>().top.SetActive(false);
                        //ins.GetComponent<Borders>().bottom.SetActive(false);
                        
                        path.Add(new Vector2(wayPoints[i].x, k));
                    }
                }
                else
                {
                    //if (wayPoints[i].y != wayPoints[i + 1].y)
                    //{
                    //    if (wayPointObj[i].GetComponent<Borders>() != null)
                    //    {
                    //        wayPointObj[i].GetComponent<Borders>().top.SetActive(false);

                    //    }
                    //    if (wayPointObj[i + 1].GetComponent<Borders>() != null)
                    //    {
                    //        wayPointObj[i + 1].GetComponent<Borders>().bottom.SetActive(false);
                    //    }
                    //}
                    for (int k = (int)wayPoints[i].y + 1; k < wayPoints[i + 1].y; k++)
                    {
                        GameObject ins = Instantiate(floor, new Vector3(wayPoints[i].x, k), Quaternion.identity, holder.transform);
                        zPos -= .1f;
                        //ins.GetComponent<Borders>().top.SetActive(false);
                        //ins.GetComponent<Borders>().bottom.SetActive(false);
                        
                        path.Add(new Vector2(wayPoints[i].x, k));
                    }
                }


                    horizontal = false;
            }
            else
            {
                if (wayPoints[i].x > wayPoints[i + 1].x)
                {

                    //if (wayPointObj[i].GetComponent<Borders>() != null)
                    //{
                    //    wayPointObj[i].GetComponent<Borders>().left.SetActive(false);
                        
                    //}
                    //if (wayPointObj[i + 1].GetComponent<Borders>() != null)
                    //{
                    //    wayPointObj[i + 1].GetComponent<Borders>().right.SetActive(false);
                    //}

                    for (int k = (int)wayPoints[i].x - 1; k > wayPoints[i + 1].x; k--)
                    {
                        GameObject ins = Instantiate(floor, new Vector3(k, wayPoints[i].y), Quaternion.identity, holder.transform);
                        zPos -= .1f;
                        //ins.GetComponent<Borders>().left.SetActive(false);
                        //ins.GetComponent<Borders>().right.SetActive(false);
                        
                        path.Add(new Vector2(k, wayPoints[i].y));
                    }
                }
                else
                {
                    //if (wayPoints[i].x != wayPoints[i + 1].x)
                    //{
                    //    if (wayPointObj[i].GetComponent<Borders>() != null)
                    //    {
                    //        wayPointObj[i].GetComponent<Borders>().right.SetActive(false);

                    //    }
                    //    if (wayPointObj[i + 1].GetComponent<Borders>() != null)
                    //    {
                    //        wayPointObj[i + 1].GetComponent<Borders>().left.SetActive(false);
                    //    }
                    //}
                    for (int k = (int)wayPoints[i].x + 1; k < wayPoints[i + 1].x; k++)
                    {
                        GameObject ins = Instantiate(floor, new Vector3(k, wayPoints[i].y), Quaternion.identity, holder.transform);
                        zPos -= .1f;
                        //ins.GetComponent<Borders>().left.SetActive(false);
                        //ins.GetComponent<Borders>().right.SetActive(false);


                        path.Add(new Vector2(k, wayPoints[i].y));
                    }
                }
                horizontal = true;
            }
        }

        GameObject[] floors = GameObject.FindGameObjectsWithTag("Path");

        Debug.Log(floors.Length);

        foreach(GameObject i in floors)
        {
            if(i.GetComponent<Borders>() != null)
            {
                i.GetComponent<Borders>().SetBorders();
            }
        }
    }
}


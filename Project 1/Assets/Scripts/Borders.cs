using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    public GameObject top, bottom, left, right, bottomLeft, bottomRight, topLeft, topRight, bottomLeft2, bottomRight2;

    public Vector3 above, below, forward, back, belowBack, belowForward, aboveForward, aboveBack;

    public bool aboveHit, belowHit, forwardHit, backHit;

    public LayerMask path;

    
    
    public void SetBorders()
    {

        above = new Vector3(this.gameObject.transform.position.x , this.gameObject.transform.position.y+1, gameObject.transform.position.z);
        below = new Vector3(this.gameObject.transform.position.x , this.gameObject.transform.position.y-1, gameObject.transform.position.z);
        forward = new Vector3(this.gameObject.transform.position.x+1 , this.gameObject.transform.position.y, gameObject.transform.position.z);
        back = new Vector3(this.gameObject.transform.position.x-1, this.gameObject.transform.position.y, gameObject.transform.position.z);

        belowForward = new Vector3(forward.x, below.y, gameObject.transform.position.z);
        belowBack = new Vector3(back.x, below.y, gameObject.transform.position.z);
        aboveForward = new Vector3(forward.x, above.y, gameObject.transform.position.z);
        aboveBack = new Vector3(back.x, above.y, gameObject.transform.position.z);
        
        if(Physics2D.Raycast(above, Vector3.zero, Mathf.Infinity, path))
        {
            aboveHit = true;
        }
        if (Physics2D.Raycast(below, Vector3.zero, Mathf.Infinity, path))
        {
            belowHit = true;
        }
        if (Physics2D.Raycast(forward, Vector3.zero, Mathf.Infinity, path))
        {
            forwardHit = true;
        }
        if (Physics2D.Raycast(back, Vector3.zero, Mathf.Infinity, path))
        {
            backHit = true;
        }

        if (forwardHit)
        {
            if (backHit)
            {
                Instantiate(top, above, Quaternion.identity);
                Instantiate(bottom, below, Quaternion.identity);
            }
            else if (aboveHit)
            {
                Instantiate(bottom, below, Quaternion.identity);
                Instantiate(left, back, Quaternion.identity);
                Instantiate(bottomLeft, belowBack, Quaternion.identity);
            }
            else if (belowHit)
            {
                Instantiate(top, above, Quaternion.identity);
                Instantiate(left, back, Quaternion.identity);
                Instantiate(bottomRight2, belowForward, Quaternion.identity);
                Instantiate(topLeft, aboveBack, Quaternion.identity);
            }
            else
            {
                Instantiate(top, above, Quaternion.identity);
                Instantiate(bottom, below, Quaternion.identity);
            }
        }
        else if (backHit)
        {
            if (aboveHit)
            {
                Instantiate(bottom, below, Quaternion.identity);
                Instantiate(right, forward, Quaternion.identity);
                Instantiate(bottomRight, belowForward, Quaternion.identity);
            }
            else if (belowHit)
            {
                Instantiate(top, above, Quaternion.identity);
                Instantiate(right, forward, Quaternion.identity);
                Instantiate(bottomLeft2, belowBack, Quaternion.identity);
                Instantiate(topRight, aboveForward, Quaternion.identity);
            }
            else
            {
                Instantiate(top, above, Quaternion.identity);
                Instantiate(bottom, below, Quaternion.identity);
            }
        }
        else
        {
            Instantiate(right, forward, Quaternion.identity);
            Instantiate(left, back, Quaternion.identity);
        }
    }
    
}

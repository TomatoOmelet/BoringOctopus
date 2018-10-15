using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour {
    public GameObject target;
    private bool canMove= true;

    //public float leftLimit, rightLimit, upLimit, downLimit;
    // Use this for initialization
    void Start () {
        transform.position = new Vector3(0, 0, -10);
        Follow();
    }
	
	// Update is called once per frame
	void Update () {
        if (canMove)
        {
            Follow();
        }
     
              
	}

    public void Follow()
    //follow the character when in some limit
    {
        if(target.transform.position.x + 5 > 0)
        {
            transform.position = new Vector3(target.transform.position.x + 5, transform.position.y, -10);
        }  
    }

    public void ChangeCanMove(bool value)
    {
        canMove = value;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
    public float restTime;
    public float workTime;
    

    private float timer;
    private bool isWork = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckState();
        Rotate();
	}

    public void CheckState()
    {
        timer += Time.deltaTime;
        if(isWork == false)
            //test if should begin to work
        {
            if (timer > restTime)
            {
                timer = 0;
                isWork = true;
                GetComponent<AttractPlayer>().enabled = true;
                  
            }
        }
        //test if should begin to rest
        else
        {
            if (timer > workTime)
            {
                timer = 0;
                isWork = false;
                GetComponent<AttractPlayer>().enabled = false;
                //if the rotation has not finished, reset the rotation 
                if(transform.rotation.eulerAngles.z != 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
    }

    public void Rotate()
        //rotate the object when the 
    {
        if (isWork)
            //when the sun is attract the player, rotate itself by change rotation
        {
            transform.rotation = Quaternion.Euler(0, 0, 360 * timer / workTime); 
        }
      
    }

    /*
    IEnumerator Rotate()
    {
        for (int x = 0; x<=71; x++){
            transform.Rotate(new Vector3(0, 0, 5));
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    */
}

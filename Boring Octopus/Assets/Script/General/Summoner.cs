using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour {
    //create random item after a period of time
    public GameObject[] itemArray;
    public float timeInterval;
    private float timer=0;
	

	// Update is called once per frame
	void Update () {
        if (timer > timeInterval)
        {
            timer = 0;
            //generate
            if(itemArray.Length == 1)
            {
                GameObject.Instantiate(itemArray[0], transform.position, Quaternion.identity);
            }
            else
            {
                GameObject.Instantiate(itemArray[Random.Range(0,itemArray.Length)], transform.position, Quaternion.identity);
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
	}
}

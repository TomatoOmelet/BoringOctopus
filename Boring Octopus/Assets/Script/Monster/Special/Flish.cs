 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flish : MonoBehaviour {
    public float upperLimit, lowerLimit;
    public float speed;
    public float moveTimeInterval;

    private void Start()
    {
        StartCoroutine(Move());
    }

    void Update () {
        //test the limit
        if (transform.position.y > upperLimit)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-0.02f, 0), Random.Range(-0.02f, 0)) * speed;
        }
        else if(transform.position.y < lowerLimit)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-0.02f, 0), Random.Range(0, 0.02f)) * speed;
        }
        
	}

    //move up or down randomly 
    public IEnumerator Move()
    {
        while (true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-0.02f, -0.01f), Random.Range(-0.02f, 0.02f)) * speed;
            yield return new WaitForSeconds(Random.Range(moveTimeInterval/2, moveTimeInterval));
        }   
    }
}

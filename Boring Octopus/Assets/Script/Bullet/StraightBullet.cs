using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : MonoBehaviour {
    //a bullet that move at a straight line
    public Vector2 velocity;
    public float duration;
	
	// Update is called once per frame
	void Start () {
        GetComponent<Rigidbody2D>().velocity = velocity;
        Destroy(gameObject, duration);
	}
    
}

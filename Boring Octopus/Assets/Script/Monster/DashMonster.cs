using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMonster : MonoBehaviour {
    private bool hasMove = false;
    private GameObject player;
    public Vector2 velocity;
    public float time;
    public float beginDifferX;

    private void Start()
    {
        //get the player
        player = GameObject.Find("Octo");
    }

    void Update () {
		if((transform.position.x - player.transform.position.x < beginDifferX) && hasMove == false)
            //when player is close enough, begin to move
        {
            hasMove = true;
            StartCoroutine(Move());
        }
	}

    IEnumerator Move()
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
        yield return new WaitForSeconds(time);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
    }
}

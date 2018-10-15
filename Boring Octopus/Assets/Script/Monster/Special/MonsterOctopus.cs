using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOctopus : MonoBehaviour {
    public float upDistance;//how long monster will move up and down
    public float moveStep;//how long each 0.01s moves
    public bool canMove = true;
    private float downSpeed = 0;
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        transform.Rotate(new Vector3(0, 180, 0));
        if (canMove)
        {
            StartCoroutine(Move());
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Move()
    {
        yield return new WaitForSeconds(Random.Range(0,3f));
        while (true)
        {
            //move up
            animator.SetTrigger("OctoUp");
            for (float x = 0; x <= upDistance; x += moveStep)
            {
                transform.Translate(new Vector3(0, moveStep, 0));
                yield return new WaitForSeconds(0.01f);
            }
            //move down
            animator.SetTrigger("OctoFall");
            downSpeed = 0;
            for (float x = 0; x <= upDistance; x += downSpeed)
            {
                downSpeed += 0.005f;
                transform.Translate(new Vector3(0, -downSpeed, 0));
                yield return new WaitForSeconds(0.01f);
            }
        }     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            //shoot by player's bullet, player reduce one life
            GameObject.Find("Octo").GetComponent<Player>().GetHurt();
            Destroy(this.gameObject);
        }
    }
}

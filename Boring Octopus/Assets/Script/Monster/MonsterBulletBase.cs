using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletBase : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //if touch Wall, destroy itself
        if(collider.gameObject.tag == "Wall")
        {
            //play animation
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Animator>().SetTrigger("blast");
        }
        else if(collider.gameObject.tag == "Player")
            //if touch player, player get hurt
        {
            //play animation
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Animator>().SetTrigger("blast");
            //get hurt
            collider.gameObject.GetComponent<Player>().GetHurt();
        }else if (collider.gameObject.tag == "BlackHole")
        {
            Destroy(gameObject);
        }
    }

}

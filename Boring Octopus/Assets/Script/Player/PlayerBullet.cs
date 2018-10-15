using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    //SE
    public AudioClip explosionSE;

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag == "Wall" || collider.gameObject.tag == "Monster")
        {
            //play animation
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Animator>().SetTrigger("blast");
            //play SE
            AudioSource.PlayClipAtPoint(explosionSE, new Vector3(0, 0, 0), AudioManager.seVolume);
        }
        else if(collider.gameObject.tag == "BlackHole")
        {
            Destroy(gameObject);
        }

    }
}

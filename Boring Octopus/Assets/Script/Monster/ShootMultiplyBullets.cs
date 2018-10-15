using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMultiplyBullets : MonoBehaviour {
    //shot bullet to different direction
    public Vector2[] velocityArray;
    public GameObject bullet;
    public float timeInterval;
    public Vector3 positionDifference;
    private float timer = 0;
    private GameObject player;
    //SE
    public AudioClip shootSE;

    private void Start()
    {
        player = GameObject.Find("Octo");
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > timeInterval)
        //after enough time, shoot a bullet
        {
            AudioSource.PlayClipAtPoint(shootSE, new Vector3(0, 0, 0), AudioManager.seVolume);
            foreach(Vector2 velocity in velocityArray)
            {
                GameObject newBullet=GameObject.Instantiate(bullet, new Vector3(gameObject.transform.position.x + positionDifference.x, gameObject.transform.position.y + positionDifference.y, -2), Quaternion.identity);
                newBullet.GetComponent<Rigidbody2D>().velocity = velocity;
                Destroy(newBullet, 5);
            }
            
            timer = 0;
        }
        else
        {
            //increase the timer
            timer += Time.deltaTime;
        }

        //if player has pass itself 5units destroy itself(to stop the SE)
        if (player.transform.position.x - transform.position.x > 5)
        {
            Destroy(gameObject);
        }
    }

}

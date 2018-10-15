using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRandomDirectionBullet : MonoBehaviour {
    public GameObject bullet;
    public float timeInterval;
    public float speed;
    public Vector3 positionDifference;
    public AudioClip shootSE;
    private float timer = 0;
    // Use this for initialization

	// Update is called once per frame
	void Update () {
        if (timer > timeInterval)
        //after enough time, shoot a bullet
        {
            AudioSource.PlayClipAtPoint(shootSE, new Vector3(0, 0, 0), AudioManager.seVolume);
            float radian = Random.Range(0, 6.28f);
            GameObject newBullet = GameObject.Instantiate(bullet, new Vector3(gameObject.transform.position.x + positionDifference.x, gameObject.transform.position.y + positionDifference.y, -2), Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(radian)*speed,Mathf.Sin(radian)*speed);
            Destroy(newBullet, 5);

            timer = 0;
        }
        else
        {
            //increase the timer
            timer += Time.deltaTime;
        }
    }
}

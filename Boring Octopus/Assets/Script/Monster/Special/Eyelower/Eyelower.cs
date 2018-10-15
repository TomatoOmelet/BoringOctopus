using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyelower : MonoBehaviour {
    //the boss of the forest
    private GameObject player;
    public GameObject smallbullet;
    public float smallbulletSpeed;
    public GameObject bigbullet;

    private bool canUseSkill = true;
    private float timer;
    private float skillInterval;

    //SE
    public AudioClip skillSE;

    private void Start()
    {
        //find the player
        player = GameObject.Find("Octo");
        //set initial interval
        skillInterval = Random.Range(2f, 5f);
    }

    private void Update()
    {
        MoveWithPlayer();
        if (canUseSkill)
        {
            CheckSkill();
        }
       
    }

    public void MoveWithPlayer()
        //Move with the Player
    {
        transform.position = new Vector3(player.transform.position.x + 10, transform.position.y, -1);
    }

    public void CheckSkill()
        //check if little eyelower can use the skill
    {
        timer += Time.deltaTime;
        if (timer > skillInterval)
        {
            //play SE
            AudioSource.PlayClipAtPoint(skillSE, new Vector3(0, 0, 0), AudioManager.seVolume);
            //use a random skill
            if (Random.Range(0, 4) == 0)
            {
                MouthBullet();
            }
            else
            {
                LeaveBullet();
            }
            //reset the timer and generate a new skillInterval;
            timer = 0;
            skillInterval = Random.Range(2f, 5f);
        }
    }

    public void LeaveBullet()
        //Eyelower's leave launch bullets
    {
        float extraRadian = Random.Range(0.15f, 0.55f);
        //create bullets at left leave
        for(int x =0; x<=4; x++)
        {
            //create bullet, set speed, destroy it after a period of time
            GameObject bullet =  GameObject.Instantiate(smallbullet, transform.position + new Vector3(-1, -0.6f, -2), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(smallbulletSpeed*Mathf.Cos(extraRadian+1.05f *(x+1)), smallbulletSpeed * Mathf.Sin(extraRadian+1.05f * (x + 1)));
            Destroy(bullet, 10);
        }
        //create bullets at right leave
        for (int x = 0; x <= 4; x++)
        {
            GameObject bullet = GameObject.Instantiate(smallbullet, transform.position + new Vector3(1, -0.6f, -2), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(smallbulletSpeed * Mathf.Cos(extraRadian + 1.05f * (x + 1)), smallbulletSpeed * Mathf.Sin(extraRadian + 1.05f * (x + 1)));
            Destroy(bullet, 10);
        }
    }

    public void MouthBullet()
        //EyeLower's Mouth launch a big butterfly
    {
        GameObject.Instantiate(bigbullet,transform.position+new Vector3(0,-3,-2),Quaternion.identity);
    }

    public void Defeated()
        //Eyelower is defeated, go to next stage
    {
        //play body's animation
        transform.Find("body").GetComponent<Animator>().SetTrigger("disappear");
        Destroy(gameObject, 3f);
        //change stage
        GameObject.Find("Manager").GetComponent<MapManager>().ChangeProcess(map.graveyard);
        GameObject.Find("Manager").GetComponent<MapManager>().FogAppear();
        //change the BGM
        GameObject.Find("Manager").GetComponent<AudioManager>().ChangeBGM("graveyardBGM");
        //recover the live and diamonds
        GameObject.Find("Manager").GetComponent<StateManager>().ChangeHP(5);
        GameObject.Find("Manager").GetComponent<StateManager>().ChangeBullet(5);
        //get Score
        GameObject.Find("Manager").GetComponent<StateManager>().ChangeScore(100);
       
    }

    public void LoseHead()
        //Lose the head, bode disappear and dont use the skill
    {
        //play animation
        transform.Find("body").GetComponent<Animator>().SetTrigger("disappear");
        canUseSkill = false;
    }
}

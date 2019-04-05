using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardBossManager : MonoBehaviour {
    private GameObject mainCamera;
    private GameObject player;
    private string playerDirection = "right";

    private int littleSkeletonsDefeated = 0;
    private bool defeatBoss = false;
    private bool finishBoss = false;
	// Use this for initialization
	void Start () {
        //get the camera and player
        mainCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("Octo");
        //stop the camera
        player.GetComponent<Player>().ChangeCanHorizontalMove(false);
        mainCamera.GetComponent<CameraFollow>().ChangeCanMove(false);

    }
	
	// Update is called once per frame
	void Update () {
        if (!defeatBoss)
        {
            CameraMove();
            PlayerCheckDirection();
            PlayerMove(5);
        }
        else if(!finishBoss)
        {
            if((Mathf.Abs(player.transform.position.x - transform.position.x+5) < 0.2f)&& playerDirection == "right")
                //player is at the place which able to change the camera to normal
            {
                FinishBoss();
            }
            else
            {
                CameraMove();
                PlayerCheckDirection();
                PlayerMove(5);
            }
        }
        
	}


    //====================================================================================
    //  Movement
    //====================================================================================

    public void CameraMove()
        //move the camera it by this script
    {
        if(mainCamera.transform.position.x<transform.position.x)
        {
            mainCamera.transform.position = new Vector3(player.transform.position.x + 5, transform.position.y, -10);
        }
        else
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }

    public void PlayerMove(float xSpeed)
        //move depends on the speed and direction
    {
       if(playerDirection == "right")
        {
            if (player.GetComponent<Rigidbody2D>().velocity.x < xSpeed)
            {
                player.GetComponent<Rigidbody2D>().velocity += new Vector2(0.2f, 0);
            }
            else
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else if (playerDirection == "left")
        {
            if (player.GetComponent<Rigidbody2D>().velocity.x > -xSpeed)
            {
                player.GetComponent<Rigidbody2D>().velocity -= new Vector2(0.2f, 0);
            }
            else
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(-xSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
            }
        }
    }

    public void PlayerCheckDirection()
        //change player's move direction
    {
        if((player.transform.position.x > transform.position.x + 8.3)&& playerDirection == "right")
        {
            //set the position and direction
            player.transform.position = new Vector3(transform.position.x + 8.3f, player.transform.position.y, player.transform.position.z);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(-player.GetComponent<Rigidbody2D>().velocity.x, player.GetComponent<Rigidbody2D>().velocity.y);
            playerDirection = "left";
        }
        else if ((player.transform.position.x < transform.position.x - 8.3) && playerDirection == "left")
        {
            //set the position and direction
            player.transform.position = new Vector3(transform.position.x - 8.3f, player.transform.position.y, player.transform.position.z);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(-player.GetComponent<Rigidbody2D>().velocity.x, player.GetComponent<Rigidbody2D>().velocity.y);
            playerDirection = "right";
        }
    }

    //====================================================================================
    //  Process
    //====================================================================================
    public void LittleSkeletonDefeated()
        //when a little skeleton is defeated
    {
        littleSkeletonsDefeated++;
        if (littleSkeletonsDefeated >= 3)
            //all 3 are defeated, Boss is defeated
        {
            defeatBoss = true;
        }
    }

    public void FinishBoss()
    {
        //make the player movement back to normal
        player.GetComponent<Player>().ChangeCanHorizontalMove(true);
        mainCamera.GetComponent<CameraFollow>().ChangeCanMove(true);
        finishBoss = true;
        //change map type
        GameObject.Find("Manager").GetComponent<MapManager>().ChangeProcess(map.sky);
        GameObject.Find("Manager").GetComponent<MapManager>().FogDisappear();
        //change the BGM
        GameObject.Find("Manager").GetComponent<AudioManager>().ChangeBGM("skyBGM");
        //recover the live and diamonds
        GameObject.Find("Manager").GetComponent<StateManager>().ChangeHP(5);
        GameObject.Find("Manager").GetComponent<StateManager>().ChangeBullet(5);
        //get Score
        GameObject.Find("Manager").GetComponent<StateManager>().ChangeScore(200);
        //unlock level
        LevelSelectManager.levelChosen = map.sky;
        LevelSelectManager.skyUnlocked = true;
    }
}

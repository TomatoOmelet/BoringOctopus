using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum screenTouch
//touch place on the screen
{
    none,
    left,
    right,
    both
}

public class Player : MonoBehaviour {
    public StateManager stateManager;

    private bool isFly = false;
    private bool canHurt = true;
    private bool canHorizontalMove = true;

    public GameObject bullet;
    private float shootTimer;
    private float shootInterval = 0.2f;

    public AudioClip getHurtSE;
    public AudioClip shootSE;

    private float bulletTimer = 0;
    private float getBulletInterval = 3;
    private void Update()
    {
        CheckFly();
        CheckSkill();
        GetBulletOverTime();
        if (canHorizontalMove)
        {
            HorizontalMove(5);
        }

        
    }



    //====================================================================================
    //  Movement
    //====================================================================================

    private void CheckFly()
        //change the y-speed and change the animation
    {
        if ((GetScreenTouch() == screenTouch.left || GetScreenTouch() == screenTouch.both) || 
        (Input.GetKey(KeyCode.Mouse0)&& Application.platform == RuntimePlatform.WindowsPlayer)||
        (Input.GetKey(KeyCode.Mouse0)&& Application.platform == RuntimePlatform.WindowsEditor)) 
        //if(Input.GetKey(KeyCode.Mouse0))
        {
            isFly = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,5);
            GetComponent<Animator>().SetTrigger("OctoUp");
        }
        else 
        {
            if(isFly == true)
            {
                //if has just change from flying to falling, set the y-speed to 0
                isFly = false;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);             
            }
            GetComponent<Animator>().SetTrigger("OctoFall");
        }
  
    }

    private void HorizontalMove(float xSpeed)
        //check if the x speed changes during the game, if so reset it
    {
        if(GetComponent<Rigidbody2D>().velocity.x < xSpeed)
        {
            GetComponent<Rigidbody2D>().velocity += new Vector2(0.2f, 0);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    public void ChangeCanHorizontalMove(bool value)
    {
        canHorizontalMove = value;
    }

    private void ShootCommonBullet()
    //fire the bullet 
    {
        if (shootTimer > shootInterval)
        {
            //shoot the bullet, set the position
            GameObject newBullet = GameObject.Instantiate(bullet,new Vector3(transform.position.x + 0.5f, transform.position.y - 0.08f, -2),Quaternion.identity);
            //reset the timer
            shootTimer = 0;
            //reduce bullet
            stateManager.ChangeBullet(-1);
            //play SE
            AudioSource.PlayClipAtPoint(shootSE, new Vector3(0, 0, 0), AudioManager.seVolume);
        }
    }

    private screenTouch GetScreenTouch()
        //get which part of the screen is touched
    {
        
        if (Input.touchCount == 0)
            //no touch on the screen
        {
            return screenTouch.none;
        }
        else if (Input.touches[0].position.x <= Screen.width / 2)
        //has a touch at left screen(touch one)
        {
            for(int x = 1; x < Input.touches.Length;  x++)
            {
                if (Input.touches[x].position.x > Screen.width / 2)
                    //has a touch at right
                {
                    return screenTouch.both;
                }
            }
            //no touch at right
            return screenTouch.left;
        }
        else
        //the first touch is at right
        {
            for (int x = 1; x < Input.touches.Length; x++)
            {
                if (Input.touches[x].position.x < Screen.width / 2 )
                //has a touch at left
                {
                    return screenTouch.both;
                }
            }

            //no touch at left
            return screenTouch.right;
        }

    }

    //====================================================================================
    //  Battle
    //====================================================================================
    private void GetBulletOverTime()
    {
        bulletTimer += Time.deltaTime;
        if(bulletTimer > getBulletInterval)
        {
            bulletTimer = 0;
            stateManager.ChangeBullet(1);
        }
    }

    private void CheckSkill()
    //after get touch, check if player can use skill, use if he/she can
    {
        //if has bullet and touch right screen, shot
        if ((GetScreenTouch() == screenTouch.right || GetScreenTouch() == screenTouch.both || Input.GetKeyDown(KeyCode.Mouse1))&&StateManager.bulletNumber>0){
            ShootCommonBullet();
        }
        else
        //screen is not touch, increase the timer
        {
            shootTimer += Time.deltaTime;
        }
    }

    public void GetHurt()
    //reduce heart and invincible for a while
    {
        if (canHurt == true)
        {
            //play SE
            AudioSource.PlayClipAtPoint(getHurtSE, new Vector3(0, 0, 0), AudioManager.seVolume);
            stateManager.ChangeHP(-1);
            //make the player incincivable for a while
            canHurt = false;
            StartCoroutine(Invincivable());
           
        }
    }

    IEnumerator Invincivable()
    {
        //let the player invincible for 2 seconds after lose one heart
        for (int x = 1; x <= 10; x++)
        {//shining
            if (x % 2 == 1)
            {
                this.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 200, 155);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 200, 255);
            }
            yield return new WaitForSeconds(0.25f);

        }
        canHurt = true;// able to get hurt again

    }
}



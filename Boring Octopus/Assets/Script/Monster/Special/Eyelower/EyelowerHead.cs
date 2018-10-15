using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyelowerHead : MonoBehaviour
//Eyelower's head, when HP decrease it will rotate, hp<50% it will launch 
{
    public int maxHp;
    private int hp;
    public float speed;
    private float rotateSpeed;

    private bool hasMove = false;
    private StateManager stateManager;

    //SE
    public AudioClip collisionSE;

    private void Start()
    {
        hp = maxHp;
        stateManager = GameObject.Find("Manager").GetComponent<StateManager>();
        GetComponent<ShootRandomDirectionBullet>().enabled = false;
    }

    private void Update()
    {
        Rotate();
        if(hasMove == true)
        {
            TestBound();
        }
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {

            //reduce hp
            hp -= 1;
            stateManager.ChangeScore(5);
            //rotate quicker
            if (rotateSpeed < 7.5)
            {
                rotateSpeed += 0.5f;
            }
            //test if need to launch the head
            if (hp < maxHp/2 )
            {
                if(hasMove == false)
                {
                    RandomVelocity(0, 6.28f, speed);
                    hasMove = true;
                    transform.parent.gameObject.GetComponent<Eyelower>().LoseHead();
                    GetComponent<ShootRandomDirectionBullet>().enabled = true;
                }
            
            }
            //test if has died
            if (hp <= 0)
            {
                GetComponent<ShootRandomDirectionBullet>().enabled = false;
                //play animation
                GetComponent<Animator>().SetTrigger("disappear");
                //this monster is defeated
                transform.parent.gameObject.GetComponent<Eyelower>().Defeated();
            }
        }
    }

    private void TestBound()
        //When the head get some bound, reset the velocity
    {
        //get Top
        if(DifferPosition().y > 3.5)
        {
            RandomVelocity(-3.14f, 0,speed);
            //SE
            AudioSource.PlayClipAtPoint(collisionSE, new Vector3(0, 0, 0), AudioManager.seVolume);
        }
        else if(DifferPosition().y < -3.5)
            //get the bottom
        {
            RandomVelocity(0, 3.14f, speed);
            //SE
            AudioSource.PlayClipAtPoint(collisionSE, new Vector3(0, 0, 0), AudioManager.seVolume);
        }
        else if (DifferPosition().x <-12.2)
        //get the left 
        {
            RandomVelocity(-0.57f, 0.57f, speed);
            //SE
            AudioSource.PlayClipAtPoint(collisionSE, new Vector3(0, 0, 0), AudioManager.seVolume);
        }
        else if (DifferPosition().x > 1.7)
        //get the right
        {
            RandomVelocity(1.57f, 5.61f, speed);
            //SE
            AudioSource.PlayClipAtPoint(collisionSE, new Vector3(0, 0, 0), AudioManager.seVolume);
        }

    }

    private void Rotate()
        //Rotate the head
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed));
    }

    private void RandomVelocity(float min, float max, float speed)
        //get the bound of the radian, the speed, return the velocity
    {
        float radian = Random.Range(min,max);
        GetComponent<Rigidbody2D>().velocity =new Vector2(Mathf.Cos(radian) * speed, Mathf.Sin(radian) * speed);
    }

    private Vector3 DifferPosition()
        //return the differ of this gamobject and the parent
    {
        return transform.position - transform.parent.position;
    }
}

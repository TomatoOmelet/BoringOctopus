using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transkeleton : MonoBehaviour {
    public int hp;
    private bool alive = true;

    private float skillTimeInterval;
    private float skillTimer = 0;

    private int typeIndex = 0;
    private float animaTimer;

    public GameObject skillFire;
    public AudioClip skill1SE;

    public GameObject skillBullet;
    public float skillBulletSpeed;
    public AudioClip skill2SE;

    public GameObject skillGhost;
    public AudioClip skill3SE;

    public GameObject littleTranskeletons;

    private void Start()
    {
        skillTimeInterval = Random.Range(2f, 4f);
    }

    private void Update()
    {
        if (alive)
        {
            CheckAnimation();
            CheckSkill();
        }
       
    }
    //====================================================================================
    //  Movement
    //====================================================================================
    public void CheckAnimation()
        //change its animation each second
    {
        animaTimer += Time.deltaTime;
        if (animaTimer > 1){
            animaTimer = 0;
            typeIndex++;
            if(typeIndex == 3)
                //typeIndex is from 0 to 2
            {
                typeIndex = 0;
            }
            GetComponent<Animator>().SetInteger("typeIndex",typeIndex);
        }
    }

    public void CheckSkill()
    {
        skillTimer += Time.deltaTime;
        if(skillTimer> skillTimeInterval)
            //after enough timer, use the skill
        {
            //set a new interval and reset the timer
            skillTimeInterval = Random.Range(2f,4f);
            skillTimer = 0;
            //use skill depends on the animation state
            switch (typeIndex)
            {
                case 0:
                    Skill1();
                    break;
                case 1:
                    Skill2();
                    break;
                case 2:
                    Skill3();
                    break;

            }
        }
    }

    public void Skill1()
    //summon some fire from the sky
    {
        AudioSource.PlayClipAtPoint(skill1SE, new Vector3(0, 0, 0), AudioManager.seVolume);
        for (int x = 1; x<=3; x++)
        {
            GameObject.Instantiate(skillFire, new Vector3(transform.parent.position.x, transform.parent.position.y, -1), Quaternion.identity);
        }
     
    }

    public void Skill2()
    //shoot bullets
    {
        AudioSource.PlayClipAtPoint(skill2SE, new Vector3(0, 0, 0), AudioManager.seVolume);
        float extraRadian = Random.Range(0, 1.256f);
        for(int x = 1;x<=5; x++)
        {
            //instantiate the bullet and set its velocity
            GameObject newBullet = GameObject.Instantiate(skillBullet, transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = 
                new Vector2(Mathf.Cos(extraRadian + 1.256f*x)*skillBulletSpeed, Mathf.Sin(extraRadian + 1.256f * x)*skillBulletSpeed);
        }
    }

    public void Skill3()
    //summon a ghost
    {
        AudioSource.PlayClipAtPoint(skill3SE, new Vector3(0, 0, 0), AudioManager.seVolume);
        GameObject.Instantiate(skillGhost, transform.position, Quaternion.identity);
    }

    //====================================================================================
    //  Process
    //====================================================================================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            //reduce hp
            hp -= 1;
            //addScore
            GameObject.Find("Manager").GetComponent<StateManager>().ChangeScore(10);
            //if hp<=0 ,go to the next part 
            if (hp <= 0)
            {
                GetComponent<Animator>().SetTrigger("disappear");
            }
            alive = false;
            this.Invoke("LittleTranskeletonAppear", 1);
        }
    }

    private void LittleTranskeletonAppear()
    {
        littleTranskeletons.SetActive(true);
    }


}

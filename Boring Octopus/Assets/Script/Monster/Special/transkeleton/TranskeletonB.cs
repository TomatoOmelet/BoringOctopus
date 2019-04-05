using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranskeletonB : MonoBehaviour {
    public int hp;
    private bool alive = true;

    private float skillTimeInterval;
    private float skillTimer = 0;

    public GameObject skillBullet;
    public float skillBulletSpeed;
    public AudioClip skill2SE;


    // Use this for initialization
    void Start () {
        skillTimeInterval = Random.Range(2f, 4f);
    }
	
	// Update is called once per frame
	void Update () {
        if (alive)
        {
            CheckSkill();
        }
	}

    public void CheckSkill()
    {
        skillTimer += Time.deltaTime;
        if (skillTimer > skillTimeInterval)
        //after enough timer, use the skill
        {
            //set a new interval and reset the timer
            skillTimeInterval = Random.Range(1f, 3f);
            skillTimer = 0;
            Skill();
        }
    }

    public void Skill()
    //shoot bullets
    {
        AudioSource.PlayClipAtPoint(skill2SE, new Vector3(0, 0, 0), AudioManager.seVolume);
        float extraRadian = Random.Range(0, 2.093f);
        for (int x = 1; x <= 3; x++)
        {
            //instantiate the bullet and set its velocity
            GameObject newBullet = GameObject.Instantiate(skillBullet, transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity =
                new Vector2(Mathf.Cos(extraRadian + 2.093f * x) * skillBulletSpeed, Mathf.Sin(extraRadian + 2.093f * x) * skillBulletSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            //reduce hp
            hp -= 1;
            //addScore
            GameObject.Find("Manager").GetComponent<StateManager>().ChangeScore(10);
            //if hp<=0 ,died 
            if (hp <= 0)
            {
                GetComponent<Animator>().SetTrigger("disappear");
                alive = false;
                transform.parent.parent.gameObject.GetComponent<GraveyardBossManager>().LittleSkeletonDefeated();
            }
            
        }
    }

}

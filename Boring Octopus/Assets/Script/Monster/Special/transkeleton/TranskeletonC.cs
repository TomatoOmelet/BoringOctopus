using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranskeletonC : MonoBehaviour {
    public int hp;
    private bool alive = true;

    private float skillTimeInterval;
    private float skillTimer = 0;

    public GameObject skillGhost;
    public AudioClip skill3SE;


    // Use this for initialization
    void Start () {
        skillTimeInterval = Random.Range(4f, 7f);
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
            skillTimeInterval = Random.Range(2f, 5f);
            skillTimer = 0;
            Skill();
        }
    }

    public void Skill()
    //summon a ghost
    {
        AudioSource.PlayClipAtPoint(skill3SE, new Vector3(0, 0, 0), AudioManager.seVolume);
        GameObject.Instantiate(skillGhost, transform.position, Quaternion.identity);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour {
    //ghost will appear and chase the player
    public float speed;
    public float liveTime;
    public float appearDifferX;

    private bool testAppearFinish = false;
    private bool hasAppeared = false;
    private bool hasDisappeared = false;
    private float timer = 0;
    private Animator anima;
    private string direction = "left";

    private GameObject player;
    // Use this for initialization
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 0);
        player = GameObject.Find("Octo");
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //test if should appear
        if (testAppearFinish == false)
        {
            if ((transform.position.x - player.transform.position.x <= appearDifferX) || appearDifferX == -1) 
            {
                StartCoroutine(Appear());
                testAppearFinish = true;
            }
        }

        //chase the player
        if (hasAppeared)
        {
            Chase();
            timer += Time.deltaTime;
        }

        //test if should disappear
        if (hasDisappeared == false && timer > liveTime)
        {
            StartCoroutine(Disappear());
        }

        //TestDirection();
    }

    public void Chase()
    //chase the player
    {
        //get the direction
        float radian = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x);
        //caculate the velocity
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(radian) * speed, Mathf.Sin(radian) * speed);
    }

    public void TestDirection()
    {
        if (player.transform.position.x > transform.position.x && direction == "left")
        {
            anima.SetTrigger("TurnRight");
            direction = "right";
        }
        if (player.transform.position.x < transform.position.x && direction == "right")
        {
            anima.SetTrigger("TurnLeft");
            direction = "left";
        }
    }

    IEnumerator Appear()
    {
        for (float x = 0; x <= 1; x += 0.05f)
        {
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, x);
            yield return new WaitForSeconds(0.01f);
        }
        hasAppeared = true;
    }

    IEnumerator Disappear()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        hasDisappeared = true;
        for (float x = 1; x >= 0; x -= 0.02f)
        {
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, x);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }

 
}

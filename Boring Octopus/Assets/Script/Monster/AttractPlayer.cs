using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractPlayer : MonoBehaviour {
    public float power;
    public float beginDistance;
    public float timerInterval;

    private GameObject player;
    private float timer;
    private bool isAttract;

    private void Start()
    {
        player = GameObject.Find("Octo");
    }
    // Update is called once per frame
    void Update () {
        //no timeInterval , attract all the time
        if(timerInterval == 0)
        {
            if (DistanceBetween(transform.position, player.transform.position) <= beginDistance)
            {
                Attract(power);
            }
        }
        else
        //attract for a while and stop for a while
        {
            if (isAttract)
            {
                if(timer > timerInterval)
                    //change mode after enough time
                {
                    isAttract = false;
                }
                else
                {
                    //add time and attract
                    timer += Time.deltaTime;
                    if (DistanceBetween(transform.position, player.transform.position) <= beginDistance)
                    {
                        Attract(power);
                    }
                }
            }
            else
            {
                if (timer <= 0)
                //change mode after enough time
                {
                    isAttract = true;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }
        }
        
	}

    private void Attract(float power)
    {
        float radian = Mathf.Atan2(transform.position.y - player.transform.position.y, transform.position.x - player.transform.position.x);
        player.transform.position += new Vector3(Mathf.Cos(radian)*power, Mathf.Sin(radian)*power, 0);
    }

    private float DistanceBetween(Vector3 position1, Vector3 position2)
    //caculate the distance between two position
    {
        return Mathf.Sqrt(Mathf.Pow(position1.x - position2.x, 2) + Mathf.Pow(position1.y - position2.y, 2));
    }
}

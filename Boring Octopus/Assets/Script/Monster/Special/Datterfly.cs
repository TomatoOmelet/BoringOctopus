using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datterfly : MonoBehaviour {
    public int power = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            //after touch player change itself to trigger
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().velocity * power);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour {
    public int hp;
    private StateManager stateManager;

    private void Start()
    {
        stateManager = GameObject.Find("Manager").GetComponent<StateManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerBullet")
        {
            
            //reduce hp
            hp -= 1;
            //if hp<=0 monster dies,so sad
            if (hp <= 0)
            {

                Destroy(this.gameObject);
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossiblyAppear : MonoBehaviour {
    public int possibility;
	// Use this for initialization
	void Start () {
		if(Random.Range(0,100)> possibility)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

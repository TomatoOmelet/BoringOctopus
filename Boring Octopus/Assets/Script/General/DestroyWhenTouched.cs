using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenTouched : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Mouse0)||Input.GetKeyDown(KeyCode.Mouse1))
        {
            Destroy(gameObject);
        }
	}
}

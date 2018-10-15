using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour {
    //Fog keep moveing left
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<RectTransform>().Translate(-Time.deltaTime*30, 0, 0);
        //if fog get limit, set it back
        if (GetComponent<RectTransform>().localPosition.x > -2000 && GetComponent<RectTransform>().localPosition.x < -1280)
        {
    
            //GetComponent<RectTransform>().Translate(1270, 0, 0);
            GetComponent<RectTransform>().localPosition += new Vector3(1280, 0, 0);
         
            
        }
	}
}

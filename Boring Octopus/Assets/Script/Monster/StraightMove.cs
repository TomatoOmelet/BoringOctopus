using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMove : MonoBehaviour {
    public List<string> velocityList; // x_movement y_movement range waittime 
	// Use this for initialization
	void Start () {
        StartCoroutine(Move());
    }
	
	// Update is called once per frame
	void Update () {
        
	}


    IEnumerator Move()
    {
        while(true){
            foreach (string line in velocityList)
            {   //transfer the information get from public to list
                string[] stringList=line.Split(' ');
                for (float x=1; x<=int.Parse(stringList[2]); x+=Time.timeScale) { //for each list, move
                    //yield return new WaitForSeconds(float.Parse(stringList[3])/Time.timeScale);// wait
                    yield return new WaitForSeconds(float.Parse(stringList[3]));// wait
                    MoveOneTime(1, new Vector2(float.Parse(stringList[0])*Time.timeScale, float.Parse(stringList[1]) * Time.timeScale));
                }
                
            }
        }
    }

    public void MoveOneTime(float scale,Vector2 velocity)
    {
        transform.position = new Vector2(transform.position.x + scale*velocity.x, transform.position.y + scale*velocity.y);
    }


}

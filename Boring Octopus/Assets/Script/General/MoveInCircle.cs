using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInCircle : MonoBehaviour {
    public GameObject center;
    public float distance;
    public float speed;
    public float initialRadian;
    private float radian;

    void Start()
    {
        radian = initialRadian;
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            //set the position
            transform.position = center.transform.position + new Vector3(distance*Mathf.Cos(radian), distance*Mathf.Sin(radian), 0);
            //chande radian
            radian += speed;
            if (radian > 6.28f)
            {
                radian -= 6.28f;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}

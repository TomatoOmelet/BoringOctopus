using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTranslate : MonoBehaviour {
    public Vector2 firstRandomTranslate;
    public Vector2 secondRandomTranslate;

    private void Start()
    {
        transform.position += new Vector3(Random.Range(firstRandomTranslate.x, secondRandomTranslate.x), Random.Range(firstRandomTranslate.y, secondRandomTranslate.y),0);
    }
}

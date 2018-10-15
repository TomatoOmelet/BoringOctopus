using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBossMap : MonoBehaviour {
    public GameObject[] itemArray;//the items that are possible to generate, 0->Heart 1->Diamond 2->Monster
    public float distanceBetweenItems = 2f;

    private int previousRamdomNumber;
    private Vector3[] differPositionArray = new Vector3[3];//differPosition of the items

    private void Start()
    {
        //get three differPositions, make them at least 1units away from each other
        differPositionArray[0] = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-4.2f, 4.2f), -1);
        //the next position should be away from the first one
        differPositionArray[1] = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-4.2f, 4.2f), -1);
        while (DistanceBetween(differPositionArray[1], differPositionArray[0]) < distanceBetweenItems)
          // if the distance is not enough, set again
        {
            differPositionArray[1] = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-4.2f, 4.2f), -1);
        }
        //the third position should be away from the previous two one
        differPositionArray[2] = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-4.2f, 4.2f), -1);
        while ((DistanceBetween(differPositionArray[2], differPositionArray[0]) < distanceBetweenItems) || 
            (DistanceBetween(differPositionArray[2], differPositionArray[1]) < distanceBetweenItems))
        // if the distance is not enough, set again
        {
            differPositionArray[2] = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(-4.2f, 4.2f), -1);
        }

        //Then set three items to three position
        GameObject.Instantiate(OneRandomItem(), gameObject.transform.position + differPositionArray[0], Quaternion.identity);
        GameObject.Instantiate(OneRandomItem(), gameObject.transform.position + differPositionArray[1], Quaternion.identity);
        GameObject.Instantiate(OneRandomItem(), gameObject.transform.position + differPositionArray[2], Quaternion.identity);
    }

    private float DistanceBetween(Vector3 position1, Vector3 position2)
        //caculate the distance between two position
    {
        return Mathf.Sqrt(Mathf.Pow(position1.x - position2.x, 2) + Mathf.Pow(position1.y - position2.y, 2));
    }

    private GameObject OneRandomItem()
        //get one random item from the array
    {
        int x = Random.Range(0, 10);
        while(x == previousRamdomNumber)
            //wait until get a different number
        {
            x = Random.Range(0, 10);
        }
        previousRamdomNumber = x;
        if (x <= 1)
        {
            return itemArray[0];
        }
        else if (x < 6)
        {
            return itemArray[1];
        }
        else
        {
            return itemArray[2];
        }
     
    }
}

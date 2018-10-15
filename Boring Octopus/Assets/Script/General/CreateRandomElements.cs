using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandomElements : MonoBehaviour {
    public int createMod;//1->display the items in the itemArray      2->choose serval elements from the array randomly and display them
    public GameObject[] itemArray;//the items that are possible to generate, 0->Heart 1->Diamond 2->Monster
    public float distanceBetweenItems;
    public int elementNumber;
    public Vector2 xBound;
    public Vector2 yBound;
    public float elementZ;
    

    private int previousRamdomNumber;
    private Vector3[] differPositionArray;

    private void Start()
    {
        differPositionArray = new Vector3[elementNumber];

        //get the differPositions, make them away from each other
        SetPosition();
        //create the items
        if(createMod == 1)
        {
            for(int x=0;x<elementNumber;x++)
            {
                GameObject.Instantiate(itemArray[x], gameObject.transform.position + differPositionArray[x], Quaternion.identity);
               // Debug.Log(Quaternion.identity.eulerAngles);
            }
        }
        else
        {
            for (int x = 0; x < elementNumber; x++)
            {
                GameObject.Instantiate(OneRandomItem(), gameObject.transform.position + differPositionArray[x], Quaternion.identity);
            }
        }
       
    }

    private void SetPosition()
        //set the differPositions and save then to the array(position are away from each other
    {
        //random the first one
        differPositionArray[0] = new Vector3(Random.Range(xBound.x, xBound.y), Random.Range(yBound.x, yBound.y), elementZ);
        for(int x=1;x<elementNumber; x++)
        {
            bool randomFinish = false;
            int awayPositionCount = 0;
            while (randomFinish == false)
            {
                awayPositionCount = 0;
                //create a position
                differPositionArray[x] = new Vector3(Random.Range(xBound.x, xBound.y), Random.Range(yBound.x, yBound.y), elementZ);
                //test if away from previous position
                for(int y=0; y < x; y++)
                {
                    if (DistanceBetween(differPositionArray[y], differPositionArray[x]) >= distanceBetweenItems)
                    {
                        awayPositionCount++;
                    }
                }
                //if away from all the previous positions, this position is create successfully
                if(awayPositionCount>= x)
                {
                    randomFinish = true;
                }
            }

        }
    }

    private float DistanceBetween(Vector3 position1, Vector3 position2)
    //caculate the distance between two position
    {
        return Mathf.Sqrt(Mathf.Pow(position1.x - position2.x, 2) + Mathf.Pow(position1.y - position2.y, 2));
    }

    private GameObject OneRandomItem()
    //get one random item from the array
    {
        int x = Random.Range(0, elementNumber);
        while (x == previousRamdomNumber)
        //wait until get a different number
        {
            x = Random.Range(0, elementNumber);
        }
        previousRamdomNumber = x;

        return itemArray[x];
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum map
{
    forest,
    forestBoss,
    graveyard,
    graveyardBoss,
    sky
}

public class MapManager : MonoBehaviour {
    public GameObject[] initialMap;
    public GameObject[] forestMapArray;
    public GameObject forestBossMap;
    public GameObject forestBoss;
   

    public GameObject[] graveyardMapArray;
    public GameObject graveyardBossMap;
    public GameObject[] fogArray;

    public GameObject[] skyMapArray;

    public GameObject leftBound;
    public GameObject player;
    private int mapIndex = 1;
    private int bossCount = 0;
    private map process = map.forest;
    private float mapWidth = 17.775f;

    private int previousMapListIndex = -1;

    private List<GameObject> mapObjectsList = new List<GameObject>();
       
    void Start () {
        //add the first map to the list
        if(LevelSelectManager.levelChosen == map.forest)
        {
            mapObjectsList.Add(initialMap[0]); 
            mapObjectsList[0].SetActive(true);
            process = map.forest;
        }else if(LevelSelectManager.levelChosen == map.graveyard)
        {
            mapObjectsList.Add(initialMap[1]); 
            mapObjectsList[0].SetActive(true);
            process = map.graveyard;
        }else{
            mapObjectsList.Add(initialMap[2]); 
            mapObjectsList[0].SetActive(true);
            process = map.sky;
        }
        //generate the first map
        OneNewMap(process);
        bossCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        CheckMapType();
        CheckMap();
	}

    //====================================================================================
    // General
    //====================================================================================
    private void CheckMap()
        //when player get further, set new map and destroy the old one 
    {
        if(player.transform.position.x > (mapIndex-1)*mapWidth - 9)
        {
            if(process == map.forest)
                //create differentMap depends on the process
            {
                OneNewMap(map.forest);
                
               
            }else if (process == map.forestBoss)
            {
                OneNewMap(map.forestBoss);
            }else if(process == map.graveyard)
            {
                OneNewMap(map.graveyard);
            }else if (process == map.sky)
            {
                OneNewMap(map.sky);
            }
          
            //if there are more than three map Object, destroy one
            if (mapObjectsList.Count > 4)
            {
                Destroy(mapObjectsList[0]);
                mapObjectsList.RemoveAt(0);
                //Move the leftBound;
                leftBound.transform.position = new Vector3(mapWidth * (mapIndex - 3) - 11.2f, 0, 0);
            }
        }
        
    }

    private void CheckMapType()
        //check if the map type need to be change
    {
        if(process == map.forest)
        {
            if (bossCount > 13)
            //stay in forest for 13 maps, begin to generate boss map prepare for the boss
            {
                ChangeProcess(map.forestBoss);
            }
           
        }
        else if(process == map.forestBoss)
        {
            if (bossCount == 2)
            //stay in forest for 15 maps, boss will appear
            {
                GameObject.Instantiate(forestBoss, new Vector3(player.transform.position.x + 10, 0, 0), Quaternion.identity);
                bossCount += 1;
            }
        }
        else if(process == map.graveyard)
        {
            if (bossCount > 20)
            {
                ChangeProcess(map.graveyardBoss);
                OneNewMap(map.graveyardBoss);
            }
        }

    }

    private void OneNewMap(map mapType)
        //generate one map
    {       
        GameObject newMap = null;
        if (mapType == map.forest)
        {
            int mapListIndex = Random.Range(0, forestMapArray.Length);
            //loop until the next map is not the same as the previous
            while (mapListIndex == previousMapListIndex)
            {
                mapListIndex = Random.Range(0, forestMapArray.Length);
            }
            //set the previousMap
            previousMapListIndex = mapListIndex;
            //create new map
            newMap = GameObject.Instantiate(forestMapArray[mapListIndex], new Vector3(mapWidth * mapIndex, 0, 0), Quaternion.identity);
            bossCount += 1;
            //get Score
            gameObject.GetComponent<StateManager>().ChangeScore(50);
        }
        else if(mapType == map.forestBoss)
        {
            newMap = GameObject.Instantiate(forestBossMap, new Vector3(mapWidth * mapIndex, 0, 0), Quaternion.identity);
            if (bossCount < 2)
            {
                //at this time the map player past is still common forest map
                gameObject.GetComponent<StateManager>().ChangeScore(50);
            }
            bossCount += 1;         
        }
        else if (mapType == map.graveyard)
        {
            int mapListIndex = Random.Range(0, graveyardMapArray.Length);
            //loop until the next map is not the same as the previous
            while (mapListIndex == previousMapListIndex)
            {
                mapListIndex = Random.Range(0, graveyardMapArray.Length);
            }
            //set the previousMap
            previousMapListIndex = mapListIndex;
            //create new map
            newMap = GameObject.Instantiate(graveyardMapArray[mapListIndex], new Vector3(mapWidth * mapIndex, 0, 0), Quaternion.identity);
            bossCount += 1;
            //get score, but not for the first two one, since st this time the maps player cross are forest boss map
            if (bossCount > 2)
            {
                gameObject.GetComponent<StateManager>().ChangeScore(50);
            }
            
        }
        else if (mapType == map.graveyardBoss)
        {
            newMap = GameObject.Instantiate(graveyardBossMap, new Vector3(mapWidth * mapIndex, 0, 0), Quaternion.identity);
            if (bossCount < 2)
            {
                //at this time the map player past is still common graveyard map
                gameObject.GetComponent<StateManager>().ChangeScore(50);
            }
            bossCount += 1;
        }else if(mapType == map.sky)
        {
            int mapListIndex = Random.Range(0, skyMapArray.Length);
            //loop until the next map is not the same as the previous
            while (mapListIndex == previousMapListIndex)
            {
                mapListIndex = Random.Range(0, skyMapArray.Length);
            }
            //set the previousMap
            previousMapListIndex = mapListIndex;
            //create new map
            newMap = GameObject.Instantiate(skyMapArray[mapListIndex], new Vector3(mapWidth * mapIndex, 0, 0), Quaternion.identity);
            bossCount += 1;
            gameObject.GetComponent<StateManager>().ChangeScore(50);
            
        }
        mapObjectsList.Add(newMap);
        mapIndex += 1;
    }

    public void ChangeProcess(map type)
    {
        process = type;
        bossCount = 0;
    }

    //====================================================================================
    //  Graveyard
    //====================================================================================
    public void FogAppear()
    {
        foreach(GameObject fog in fogArray)
        {
            StartCoroutine(AppearByOpaqueness(fog, 0.01f));
        }
    }

    public void FogDisappear()
    {
        foreach (GameObject fog in fogArray)
        {
            StartCoroutine(DisappearByOpaqueness(fog, 0.01f));
        }
    }

    IEnumerator AppearByOpaqueness(GameObject ob, float time)
    //make an GameObject appear slowly(increase opaqueness
    {
        //make the object transparent and active
        ob.GetComponent<Image>().color = new Color(ob.GetComponent<Image>().color.r, ob.GetComponent<Image>().color.g, ob.GetComponent<Image>().color.b, 0);
        ob.SetActive(true);
        //appear
        for (int x = 0; x < 255; x+=3)
        {
            ob.GetComponent<Image>().color = new Color(ob.GetComponent<Image>().color.r, ob.GetComponent<Image>().color.g, ob.GetComponent<Image>().color.b, x);
            yield return new WaitForSeconds(time);
        }
    }

    IEnumerator DisappearByOpaqueness(GameObject ob, float time)
    //make an GameObject appear slowly(increase opaqueness
    {
        float a = ob.GetComponent<Image>().color.a;
        //disappear
        for (int x = (int)a*255; x >0; x -= 3)
        {
            ob.GetComponent<Image>().color = new Color(ob.GetComponent<Image>().color.r, ob.GetComponent<Image>().color.g, ob.GetComponent<Image>().color.b, x);
            yield return new WaitForSeconds(time);
        }
        ob.SetActive(false);
    }
}

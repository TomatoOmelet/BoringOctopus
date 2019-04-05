using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    static public bool graveyardUnlocked;
    static public bool skyUnlocked;
    public Vector3 selectedScale;
    public Color lockedColor;
    public GameObject blackCurtain;
    public AudioClip comfirmSE;
    public GameObject[] levelButtons;
    public GameObject skyInfoText;
    public static map levelChosen;
    public int current;

    private bool canAct;
    // Start is called before the first frame update
    void Start()
    {
        canAct = true;
        current = 0;
        levelButtons[current].transform.localScale = selectedScale;
        skyInfoText.SetActive(false);
        levelChosen = map.forest;
        //check unlock
        if(!graveyardUnlocked)
        {
            levelButtons[1].GetComponent<Button>().interactable = false;
            levelButtons[1].GetComponent<Image>().color = lockedColor;
        }
        if(!skyUnlocked)
        {
            levelButtons[2].GetComponent<Button>().interactable = false;
            levelButtons[2].GetComponent<Image>().color = lockedColor;
        }
    }


    public void SwitchLevelButton(int newLevel)
    {
        if(!canAct)
            return;
        AudioSource.PlayClipAtPoint(comfirmSE, new Vector3(0, 0, 0), AudioManager.seVolume);
        levelButtons[current].transform.localScale = Vector3.one;
        current = newLevel;
        levelButtons[current].transform.localScale = selectedScale;
        skyInfoText.SetActive(false);
        switch(current)
        {
            case 0:
                levelChosen = map.forest;
                break;
            case 1:
                levelChosen = map.graveyard;
                break;
            case 2:
                levelChosen = map.sky;
                skyInfoText.SetActive(true);
                break; 
        }
    }

    public void DepartButton()
    {
        if(!canAct)
            return;
        StartCoroutine(AdventureMode());
        canAct = false;
    }

    IEnumerator AdventureMode()
    {
        //show the black curtain
        DontDestroyOnLoad(blackCurtain.transform.parent.gameObject);
        DontDestroyOnLoad(gameObject);
        blackCurtain.SetActive(true);
        //play the SE
        AudioSource.PlayClipAtPoint(comfirmSE, new Vector3(0, 0, 0), AudioManager.seVolume);
        //change the curtain and audio
        for (float x = 0; x<=1; x+= 0.02f)
        {
            blackCurtain.GetComponent<Image>().color = new Color(0,0,0,x);
            gameObject.GetComponent<AudioSource>().volume = AudioManager.bgmVolume * (1 - x);
            yield return new WaitForSeconds(0.01f);
        }
        //transfer to next Scene!
        UnityEngine.SceneManagement.SceneManager.LoadScene("01-Game");
        //reset the timeScale, hp and bullet
        Time.timeScale = 1;
        StateManager.hp = StateManager.maxHp;
        StateManager.bulletNumber = StateManager.maxBulletNumber;
        //hide the curtain gradually and destroy 
        for (float x = 1; x >= 0; x -= 0.02f)
        {
            blackCurtain.GetComponent<Image>().color = new Color(0, 0, 0, x);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(blackCurtain.transform.parent.gameObject);
        Destroy(gameObject);

    }

}

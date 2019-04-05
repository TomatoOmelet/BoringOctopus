 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    //public GameObject levelText;
    public GameObject[] heartArray;
    public GameObject[] bulletArray;
    //public GameObject moneyText;
    //public GameObject attackText;
    //public GameObject defenceText;
    public GameObject scoreUI;
    public GameObject deathScoreUI;
    public GameObject deathUI;
    public GameObject pauseUI;

    //SE
    public AudioClip comfirmSE;

    // Use this for initialization
    void Start () {
        deathUI.SetActive(false);
        UpdateScore(0);
    }

    private void Update()
    {
        CheckPause();
    }
    //====================================================================================
    //  Change the UI
    //====================================================================================
    public void UpdateHp()
    {
        //set hearts UI by the hp
        for(int x =0; x< heartArray.Length; x++)
        {
            if(x< StateManager.hp)
            {
                heartArray[x].SetActive(true);
            }
            else
            {
                heartArray[x].SetActive(false);
            }
           
        }

    }

    public void UpdateBullet()
    {
        //set hearts UI by the bullet
        for (int x = 0; x < bulletArray.Length; x++)
        {
            if (x < StateManager.bulletNumber)
            {
                bulletArray[x].SetActive(true);
            }
            else
            {
                bulletArray[x].SetActive(false);
            }

        }

    }

    public void UpdateScore(int number)
    {
       if(TitleManager.language == language.English)
        {
            scoreUI.GetComponent<Text>().text = "Score: " + number.ToString();
            scoreUI.GetComponent<Text>().font = GameObject.Find("FontManager").GetComponent<FontManager>().enFont;
        }
        else
        {
            scoreUI.GetComponent<Text>().text = "分数: " + number.ToString();
            scoreUI.GetComponent<Text>().font = GameObject.Find("FontManager").GetComponent<FontManager>().chFont;
        }

    }
    
    //====================================================================================
    //  Change the UI to another Stage
    //====================================================================================
    public void ChangeToDeadUI()
        //enable the death UI
    {
        //if final score is higher than highest score, change it
        if (StateManager.finalScore > TitleManager.highestScore)
        {
            TitleManager.highestScore = StateManager.finalScore;
            SaveManager.Save();
        }
        deathUI.SetActive(true);
        //change the score at death UI to show the player
        if(TitleManager.language == language.English)
        {
            deathScoreUI.GetComponent<Text>().text = "Your Score is: " + StateManager.finalScore.ToString();
            deathScoreUI.GetComponent<Text>().font = GameObject.Find("FontManager").GetComponent<FontManager>().enFont;
        }
        else
        {
            deathScoreUI.GetComponent<Text>().text = "你最后的分数是："+ StateManager.finalScore.ToString();
            deathScoreUI.GetComponent<Text>().font = GameObject.Find("FontManager").GetComponent<FontManager>().chFont;
        }
    }

    //====================================================================================
    //  Buttons
    //====================================================================================
    public void ReplayButton()
        //replay the game
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene("01-Game");
        //reset the timeScale, hp and bullet
        Time.timeScale = 1;      
        //UpdateHp();
        //UpdateBullet();
        //SE
        AudioSource.PlayClipAtPoint(comfirmSE, new Vector3(0, 0, 0), AudioManager.seVolume);
    }    

    public void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseButton();
        }
    }

    public void PauseButton()
        //stop the game
    {
        AudioSource.PlayClipAtPoint(comfirmSE, new Vector3(0, 0, 0), AudioManager.seVolume);
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        
    }

    public void ContinueButton()
        //continue
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        AudioSource.PlayClipAtPoint(comfirmSE, new Vector3(0, 0, 0), AudioManager.seVolume);
    }

    public void ReturnButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("00-Title");
    }
}

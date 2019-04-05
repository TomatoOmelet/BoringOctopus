using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum language{
    English,
    Chinese
}

public class TitleManager : MonoBehaviour {
    public AudioClip comfirmSE;
    public GameObject settingPage;
    public GameObject bgmVolumeSlider;
    public GameObject seVolumeSlider;
    public GameObject languageDropDown;
    public GameObject[] textArray;
    public GameObject blackCurtain;
    //public GameObject[] longTextArray;

    public GameObject creditPage;

    public static language language;
    public GameObject highestScoreUI;
    public static int highestScore;

    private void Awake()
    {
        Time.timeScale = 1;
        gameObject.GetComponent<SaveManager>().Load();
    }

    //====================================================================================
    //  Enter Game
    //====================================================================================

    public void AdventureButton()
    {
        StartCoroutine(LevelSelect());
    }

    IEnumerator LevelSelect()
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelection");
        //reset the timeScale, hp and bullet
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

    //====================================================================================
    //  Setting
    //====================================================================================
    public void SettingButton()
    //show the setting page
    {
        settingPage.SetActive(true);
        AudioSource.PlayClipAtPoint(comfirmSE, new Vector3(0, 0, 0), AudioManager.seVolume);
    }

    public void ChangeBgmVolumeSlider()
        //change bgm 's volume
    {
        AudioManager.bgmVolume = bgmVolumeSlider.GetComponent<Slider>().value;
        gameObject.GetComponent<AudioSource>().volume = bgmVolumeSlider.GetComponent<Slider>().value;
        
    }

    public void ChangeSeVolumeSlider()
    //change se 's volume
    {
        AudioManager.seVolume = seVolumeSlider.GetComponent<Slider>().value;

    }

    public void ChangeLanguageDropDown()
    {
        if(languageDropDown.GetComponent<Dropdown>().value == 0)
        {
            TitleManager.language = language.English;
        }
        else
        {
            TitleManager.language = language.Chinese;
        }

        //change the texts that has been existed
        foreach(GameObject text in textArray)
        {
            text.GetComponent<ChangeTextByLanguage>().ChangeText();
        }
        /*foreach (GameObject text in longTextArray)
        {
            text.GetComponent<ChangeLongTextByLanguage>().ChangeText();
        }*/
        SetHighestScore(highestScore);
    }

    public void CloseSettingButton()
    {
        settingPage.SetActive(false);
        AudioSource.PlayClipAtPoint(comfirmSE, new Vector3(0, 0, 0), AudioManager.seVolume);
        SaveManager.Save();
    }

    //====================================================================================
    // Credit
    //====================================================================================
    public void CreditButton()
    //show the credit page
    {
        creditPage.SetActive(true);
        AudioSource.PlayClipAtPoint(comfirmSE, new Vector3(0, 0, 0), AudioManager.seVolume);
    }

    public void CloseCreditButton()
    //show the credit page
    {
        creditPage.SetActive(false);
        AudioSource.PlayClipAtPoint(comfirmSE, new Vector3(0, 0, 0), AudioManager.seVolume);
    }

    //====================================================================================
    // Set Values
    //====================================================================================
    public void SetHighestScore(int number)
    //set the highest score
    {
        TitleManager.highestScore = number;
        if (language == language.English)
        {
            highestScoreUI.GetComponent<Text>().text = "Highest Score: \n" + highestScore.ToString();
            highestScoreUI.GetComponent<Text>().font = GameObject.Find("FontManager").GetComponent<FontManager>().enFont;
        }
        else
        {
            highestScoreUI.GetComponent<Text>().text = "最高得分: \n" + highestScore.ToString();
            highestScoreUI.GetComponent<Text>().font = GameObject.Find("FontManager").GetComponent<FontManager>().chFont;
        }
    }

    public void SetBgmVolume(float number)
        //set the bgm Volume 
    {
        bgmVolumeSlider.GetComponent<Slider>().value = number;
        AudioManager.bgmVolume = number;
    }

    public void SetSeVolume(float number)
    //set the se Volume
    {
        seVolumeSlider.GetComponent<Slider>().value = number;
        AudioManager.seVolume = number;
    }

    public void SetLanguage(language language)
    //set the se Volume
    {
        TitleManager.language = language;
        if(language == language.English)
        {
            languageDropDown.GetComponent<Dropdown>().value = 0;
        }
        else
        {
            languageDropDown.GetComponent<Dropdown>().value = 1;
        }
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {
    
    //public static int level = 1;
    public static int maxHp = 5;
    public static int hp = 5;
    public static int maxBulletNumber = 5;
    public static int bulletNumber = 5;
    public static int score = -50;
    public static int finalScore = -50;

    public UIManager uiManager;

    private void Start()
    {
        maxBulletNumber = 5;
        maxHp = 5;
        hp = maxHp;
        bulletNumber = maxBulletNumber;
        score = -50;
        finalScore = -50;
        uiManager.UpdateBullet();
        uiManager.UpdateHp();

    }

    //====================================================================================
    //  Operate the states
    //====================================================================================
    public void ChangeHP(int number)
        //change the hp
    {
        hp += number;
        //test the limit
        if (hp > maxHp)
        {
            hp = maxHp;
        }
        //rewrite the UI
        uiManager.UpdateHp();
        if (hp <= 0)
            //game over
        {
            GameOver();
        }

    }

    public void ChangeBullet(int number)
    //reduce the bullet
    {
        bulletNumber += number;     
        //test the limit
        if (bulletNumber > maxBulletNumber)
        {
            bulletNumber = maxBulletNumber;
        }
        //rewrite the UI
        uiManager.UpdateBullet();
    }

    public void ChangeScore(int number)
    {
        if (score >= 0)
        {
            StartCoroutine(AddScore(number, 0.01f));
        }
        else//score will change from -50 to 0  at first, don't let the players know it(cross out
        {
            score += number;
        }
        finalScore += number;
    }

    IEnumerator AddScore(int number, float time)
    {
        for(int x = 1; x <= number; x++)
        {
            score++;
            uiManager.UpdateScore(score);
            yield return new WaitForSeconds(time);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        uiManager.ChangeToDeadUI();
        //stop the BGM
        gameObject.GetComponent<AudioManager>().StopAudio();
    }

    
    
}

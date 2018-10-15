using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static float bgmVolume = 1;
    public static float seVolume = 1;

    public AudioSource audiosource;
    public AudioClip graveyardBGM;
    public AudioClip skyBGM;
    // Use this for initialization
    void Start () {
        StartCoroutine(AudioFadeIn(0.01f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeBGM(string bgmName)
    {
        if (bgmName == "graveyardBGM")
        {
           StartCoroutine(ChangeMusicGradually(graveyardBGM,0.01f));
        }else if(bgmName == "skyBGM")
        {
            StartCoroutine(ChangeMusicGradually(skyBGM, 0.01f));
        }
        
    }

    IEnumerator ChangeMusicGradually(AudioClip audioClip,float time)
    {
        yield return AudioFadeOut(time);
        audiosource.clip = audioClip;
        audiosource.Play();
        yield return AudioFadeIn(time);
    }

    IEnumerator AudioFadeIn(float time)
    {
        for(float x = 0; x<=1; x += 0.02f)
        {
            audiosource.volume = bgmVolume * x;
            yield return new WaitForSeconds(time);
        }
    }

    IEnumerator AudioFadeOut(float time)
    {
        for (float x = 1; x >= 0; x -= 0.02f)
        {
            audiosource.volume = bgmVolume * x;
            yield return new WaitForSeconds(time);
        }
    }

    public void StopAudio()
    {
        audiosource.Stop();
    }
}

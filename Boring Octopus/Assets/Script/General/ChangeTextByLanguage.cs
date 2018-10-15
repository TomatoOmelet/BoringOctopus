using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextByLanguage : MonoBehaviour {
    public string chineseText;
    public string englishText;

    void Start()
    {
        ChangeText();
    }

    private void OnEnable()
    {
        ChangeText();
    }

    public void ChangeText()
    {
        if (TitleManager.language == language.English)
        {
            GetComponent<Text>().text = englishText;
        }
        else
        {
            GetComponent<Text>().text = chineseText;
        }
    }
}

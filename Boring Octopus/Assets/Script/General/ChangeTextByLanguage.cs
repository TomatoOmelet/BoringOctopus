using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextByLanguage : MonoBehaviour {
    public string chineseText;
    public string englishText;
    public Font enFont;
    public Font chFont;

    void Start()
    {
        enFont = GameObject.Find("FontManager").GetComponent<FontManager>().enFont;
        chFont = GameObject.Find("FontManager").GetComponent<FontManager>().chFont;
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
            GetComponent<Text>().font = enFont;
        }
        else
        {
            GetComponent<Text>().text = chineseText;
            GetComponent<Text>().font = chFont;
        }
    }
}

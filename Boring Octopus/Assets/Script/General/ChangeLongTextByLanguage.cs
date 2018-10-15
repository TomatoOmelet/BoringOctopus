using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLongTextByLanguage : MonoBehaviour {
    public string wholeText;
    // Use this for initialization
    private void Awake()
    {
        wholeText = GetComponent<Text>().text;
    }
    void OnEnable () {   
        ChangeText();   
	}
	
	public void ChangeText()
    {
        if (TitleManager.language == language.English)
        {
            GetComponent<Text>().text = GetSubString(wholeText, "[English]", "[EnglishEnd]");
        }
        else
        {
            GetComponent<Text>().text = GetSubString(wholeText, "[Chinese]", "[ChineseEnd]");
        }
    }

    public string GetSubString(string content,string start, string end)
        //get the whole string and the start&end, return the subString
    {
        
        int startIndex = content.IndexOf(start) + start.Length+1;
        int endIndex = content.IndexOf(end);
        return content.Substring(startIndex, endIndex - startIndex);

    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;


public class SaveManager : MonoBehaviour
{


    public static void Save()
    {
        //create a binary formatter 
        BinaryFormatter bf = new BinaryFormatter();
        //create a file stream
        string path = Application.persistentDataPath;
        if(Application.platform == RuntimePlatform.WebGLPlayer)
		{
			path = Application.persistentDataPath;
			string folderName = Path.GetFileName(path);
			while(folderName != "idbfs")
			{
				path = Path.GetDirectoryName(path);
				folderName = Path.GetFileName(path);
			}
        }
        path = Path.Combine(path, "BoringOctopus");
        path = Path.Combine(path, "Save.dat");
        
        //create directory if not exist
        if(!Directory.Exists(Path.GetDirectoryName(path)))
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        FileStream saveDataFile = File.Create(path);

        //save things we need at SaveData
        SaveData saveData = new SaveData();
        saveData.highestScore = TitleManager.highestScore;
        saveData.bgmVolume = AudioManager.bgmVolume;
        saveData.seVolume = AudioManager.seVolume;
        saveData.language = TitleManager.language;

        //serialize the saveData
        bf.Serialize(saveDataFile, saveData);
        //close the file, which professor always asks us to remember
        saveDataFile.Close();
    }

    public void Load()
    {
        string path = Application.persistentDataPath;
        if(Application.platform == RuntimePlatform.WebGLPlayer)
		{
			path = Application.persistentDataPath;
			string folderName = Path.GetFileName(path);
			while(folderName != "idbfs")
			{
				path = Path.GetDirectoryName(path);
				folderName = Path.GetFileName(path);
			}
        }
        path = Path.Combine(path, "BoringOctopus");
        path = Path.Combine(path, "Save.dat");
        Debug.Log("Checking Save File at: " + path);
        //test if the saveData exists
        if (File.Exists(path))
        {
            //the...emmmm...again
            BinaryFormatter bf = new BinaryFormatter();
            //file stream, this time is open
            FileStream saveDataFile = File.Open(path, FileMode.Open);
            //deserialize
            SaveData saveData = (SaveData)bf.Deserialize(saveDataFile);
            //close the file
            saveDataFile.Close();

            //use what in the save Data

            gameObject.GetComponent<TitleManager>().SetLanguage(saveData.language);
            gameObject.GetComponent<TitleManager>().SetBgmVolume(saveData.bgmVolume);
            gameObject.GetComponent<TitleManager>().SetSeVolume(saveData.seVolume);
            gameObject.GetComponent<TitleManager>().SetHighestScore(saveData.highestScore);
        }
        else
        {
            gameObject.GetComponent<TitleManager>().SetHighestScore(TitleManager.highestScore);
        }
    }

}

[Serializable]
class SaveData
{
    public int highestScore;
    public float bgmVolume;
    public float seVolume;
    public language language;

}
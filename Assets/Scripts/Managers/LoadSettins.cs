using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettins : MonoBehaviour
{
    void Start()
    {
        LevelList.CorrentLevel = PlayerPrefs.GetInt("CorrentLevel", 0);
        print(LevelList.CorrentLevel);
        MusicScript.MusicOn = (PlayerPrefs.GetString("MusicOn", "True") == "True");
    }
    
    public static void SaveIntToPrefs(string name,int value)
    {
        PlayerPrefs.GetInt(name, value);
    }
}

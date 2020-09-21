using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericButtonClass : MonoBehaviour
{
    //move to another scene Using it's name
    public static void SwapToScene(string SceneName)
    {
        print(SceneName);
        SceneManager.LoadScene(SceneName);
    }
    
    //Reloads the current scene
    public static void ReloadScene()
    {
        SwapToScene(SceneManager.GetActiveScene().name);
    }

    public static void OpenUrl(string URL)
    {
        System.Diagnostics.Process.Start(URL);
    }
}

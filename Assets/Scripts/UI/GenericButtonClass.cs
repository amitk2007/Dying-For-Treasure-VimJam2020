using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericButtonClass : MonoBehaviour
{
    public static void SwapToScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public static void ReloadScene()
    {
        SwapToScene(SceneManager.GetActiveScene().name);
    }

    public static void OpenUrl(string URL)
    {
        System.Diagnostics.Process.Start(URL);
    }
}

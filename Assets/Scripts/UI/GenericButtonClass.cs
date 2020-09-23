using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericButtonClass : MonoBehaviour
{
    private static AudioSource myButtonSfx;

    private void Start()
    {
        myButtonSfx = DoNotDestroyButtonAudio.ButtonAudioSource;
    }
    //move to another scene Using it's name
    public static void SwapToScene(string SceneName)
    {
        myButtonSfx.Play();
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

    public static void MoveToNextLevel()
    {
        for (int i = 0; i < LevelList.Levels.Length; i++)
        {
            if (SceneManager.GetActiveScene().name == LevelList.Levels[i])
            {
                if (i + 1 == LevelList.Levels.Length)
                {
                    //you finished the game
                }
                else
                {
                    LevelList.SetCurrentLevel(i + 1);
                    SwapToScene(LevelList.Levels[i + 1]);
                }
            }
        }
    }

    public static void StartGame()
    {
        SwapToScene(LevelList.Levels[LevelList.CorrentLevel]);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}

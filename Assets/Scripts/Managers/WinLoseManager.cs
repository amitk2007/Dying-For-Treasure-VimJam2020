using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is responsible for managing the win and lose conditions.
public class WinLoseManager : MonoBehaviour
{
    public static WinLoseManager winLoseManager;//This static manager exists in order to allow other objects in the scene to find this manager instantly, without the need for a reference
    [SerializeField] private Image losePanel;
    [SerializeField] private Image winPanel;
    private bool didCondition = false;


    //On awake, we make sure this is the only WinLose manager around.
    private void Awake()
    {
        if (winLoseManager == null)
        {
            winLoseManager = this;
            UnpauseGame();
        }
        else
            Destroy(this.gameObject);
    }

    public void DoWin(List<Artifact> artifacts)
    {
        if (!didCondition)
        {
            didCondition = true;
            Debug.Log("Win condition reached!");
            winPanel.gameObject.SetActive(true);
            winPanel.GetComponent<WinPanelScript>().SetupWinPanel(artifacts);
            PauseGame();
            //Open win window, pause game
        }
    }

    public void DoLose()
    {
        if (!didCondition)
        {
            didCondition = true;
            Debug.Log("Lose condition reached!");
            PauseGame();
            losePanel.gameObject.SetActive(true);
            //Open lose window, pause game
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        StopAllSounds();
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    private void StopAllSounds()
    {
        AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int index = 0; index < sources.Length; ++index)
        {
            if (sources[index].clip != MusicScript.musicScript.GetClip() && sources[index] != winPanel.GetComponent<AudioSource>() && sources[index] != losePanel.GetComponent<AudioSource>())
                sources[index].Stop();
        }
    }
}

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


    //On awake, we make sure this is the only WinLose manager around.
    private void Awake()
    {
        if (winLoseManager == null)
        {
            winLoseManager = this;
            unpauseGame();
        }
        else
            Destroy(this.gameObject);
    }

    public void DoWin(List<Artifact> artifacts)
    {
        Debug.Log("Win condition reached!");
        winPanel.gameObject.SetActive(true);
        winPanel.GetComponent<WinPanelScript>().SetupWinPanel(artifacts);
        pauseGame();
        //Open win window, pause game
    }

    public void DoLose()
    {
        Debug.Log("Lose condition reached!");
        losePanel.gameObject.SetActive(true);
        pauseGame();
        //Open lose window, pause game
    }

    private void pauseGame()
    {
        Time.timeScale = 0;
    }

    private void unpauseGame()
    {
        Time.timeScale = 1;
    }
}

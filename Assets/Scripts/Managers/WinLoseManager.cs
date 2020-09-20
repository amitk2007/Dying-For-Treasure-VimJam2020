using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is responsible for managing the win and lose conditions.
public class WinLoseManager : MonoBehaviour
{
    public static WinLoseManager winLoseManager;//This static manager exists in order to allow other objects in the scene to find this manager instantly, without the need for a reference
    [SerializeField] private Image losePanel;

    //On awake, we make sure this is the only WinLose manager around.
    private void Awake()
    {
        if (winLoseManager == null)
            winLoseManager = this;
        else
            Destroy(this.gameObject);
    }

    public void DoWin()
    {
        Debug.Log("Win condition reached!");
        //Open win window, pause game
    }

    public void DoLose()
    {
        Debug.Log("Lose condition reached!");
        losePanel.gameObject.SetActive(true);
        //Open lose window, pause game
    }
}

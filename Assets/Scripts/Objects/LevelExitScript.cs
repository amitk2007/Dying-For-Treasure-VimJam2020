using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script sits on the level exit object.
public class LevelExitScript : MonoBehaviour
{
    private WinLoseManager myWinLoseManager;

    private void Start()
    {
        myWinLoseManager = WinLoseManager.winLoseManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with " + collision.name);
        //When player is detected, the level is complete. NOTE - We may want to change this in the future to allow player to finish level only after aquiring artifact.
        if (collision.tag == "Player" && collision.gameObject.GetComponent<PlayerArtifacts>().HasArtifacts())
        {
            myWinLoseManager.DoWin(collision.gameObject.GetComponent<PlayerArtifacts>().GetArtifacts());
        }
    }
}

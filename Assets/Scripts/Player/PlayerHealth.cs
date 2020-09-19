using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This script is responsible for managing the player's health component
//It should be attached to player object, and its healthSliderUI field should contain the health slider object (dragged into field in Editor)
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxLife = 10;// Field is serialized so health can be changed if needed
    private int currentLife = 0;
    [SerializeField] private Slider healthSliderUI; //In Editor, slider object should be dragged in here.
    private WinLoseManager myWinLoseManager;

    //In the beginning we will set up player with full health & update UI
    void Start()
    {
        myWinLoseManager = WinLoseManager.winLoseManager;
        currentLife = maxLife;
        UpdateUI();
    }
    public void SetPlayerHealth(int newVal)
    {
        currentLife = newVal;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        currentLife = Mathf.Clamp(0, currentLife - damage, maxLife);
        UpdateUI();
        if (!IsPlayerAlive())
        {
            //Trigger lose condition
            myWinLoseManager.DoLose();
            //Trigger death animation
        }
        else
        {
            //Trigger damage animation
            //Perhaps stop or knock back player or something
        }
    }

    private bool IsPlayerAlive()
    {
        return currentLife != 0;
    }

    private void UpdateUI()
    {
        healthSliderUI.maxValue = maxLife;
        healthSliderUI.value = currentLife;
    }
}

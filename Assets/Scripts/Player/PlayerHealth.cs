using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This script is responsible for managing the player's health component
//It should be attached to player object, and its healthSliderUI field should contain the health slider object (dragged into field in Editor)
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxLife = 10;// Field is serialized so health can be changed if needed
    [SerializeField] private float InvinsibilityTime = 1f;//Field is serialized so value can be changed if needed
    private float invinsibilityTimer = 0f;
    private int currentLife = 0;
    [SerializeField] private Slider healthSliderUI; //In Editor, slider object should be dragged in here.
    private WinLoseManager myWinLoseManager;
    private PlayerCanvasAnimationManager myPlayerCanvasAnimationmanager;

    //In the beginning we will set up player with full health & update UI
    void Start()
    {
        myWinLoseManager = WinLoseManager.winLoseManager;
        myPlayerCanvasAnimationmanager = this.GetComponentInChildren<PlayerCanvasAnimationManager>();
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
        if (invinsibilityTimer == 0f)//Only take damage if timer is off
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
                myPlayerCanvasAnimationmanager.PlayAnimation(PlayerCanvasAnimationManager.PlayerCanvasAnimation.DamageTaken, "-" + damage.ToString());
                StartCoroutine(InvinsibilityTimer());
                //Perhaps stop or knock back player or something
            }
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

    //Function is responsible for making player invinsible for an allocated amount of time
    //Function also makes player visible/invisible every 0.25 seconds.
    //NOTE - I swap directly to clear and white here. If player is colored later on, this will need to be changed.
    private IEnumerator InvinsibilityTimer()
    {
        invinsibilityTimer = InvinsibilityTime;
        while(invinsibilityTimer > 0)
        {
            invinsibilityTimer = Mathf.Clamp(invinsibilityTimer - Time.deltaTime, 0f, InvinsibilityTime);
            if (invinsibilityTimer % 0.5f > 0.25f)
                this.GetComponent<SpriteRenderer>().color = Color.clear;
            else
                this.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}

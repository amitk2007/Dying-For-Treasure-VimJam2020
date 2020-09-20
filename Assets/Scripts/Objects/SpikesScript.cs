using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    [Range(0, 10)]
    public int spikesDamage;
    public bool IsOnTimer;
    [Range(0, 10)]
    public float freeTime;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(spikesDamage);
            if (IsOnTimer)
            {
                StartCoroutine(SpikesToTriggerOnTime(freeTime));
            }
            else
            {
                GetComponent<Collider2D>().isTrigger = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && IsOnTimer == false)
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }


    IEnumerator SpikesToTriggerOnTime(float seconds)
    {
        GetComponent<Collider2D>().isTrigger = true;
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(seconds);
        GetComponent<Collider2D>().isTrigger = false;
    }


}

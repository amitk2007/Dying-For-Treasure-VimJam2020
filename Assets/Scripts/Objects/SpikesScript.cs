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

    bool isInSpikes = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(spikesDamage);
            isInSpikes = true;
            if (IsOnTimer)
            {
                StartCoroutine(SpikesToTriggerOnTime(freeTime, collision.gameObject));
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
            isInSpikes = false;
        }
    }

    IEnumerator SpikesToTriggerOnTime(float seconds, GameObject player)
    {
        while (isInSpikes)
        {
            GetComponent<Collider2D>().isTrigger = true;
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(seconds);
            if (isInSpikes)
            {
                player.GetComponent<PlayerHealth>().TakeDamage(spikesDamage);
            }
        }
        GetComponent<Collider2D>().isTrigger = false;
    }
}

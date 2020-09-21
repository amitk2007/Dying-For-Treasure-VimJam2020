using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolderScript : MonoBehaviour
{
    #region Inspector Variables
    [Range(0, 10)]
    public int bolderDamage;
    [Range(0, 10)]
    public float TTL;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyThisBolder(TTL));
    }

    //Destroy this Gameobject after X seconds
    IEnumerator DestroyThisBolder(float seconds)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }

    //damage the player on collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(bolderDamage);
        }
    }
}

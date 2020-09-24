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
    [SerializeField] private float AudioRange = 13f;
    [SerializeField] private float AudioMaxRange = 5f;
    private Transform playerTransform;
    private AudioSource myAudio;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        myAudio = this.GetComponent<AudioSource>();
        StartCoroutine(DestroyThisBolder(TTL));
    }

    private void Update()
    {
        UpdateSoundVolume();
    }

    //Function changes boulder sound based on how close it is to the player
    private void UpdateSoundVolume()
    {
        float newVol = Mathf.Lerp(1,0,(DistanceFromPlayer() - AudioMaxRange) / (AudioRange - AudioMaxRange));
        //Debug.Log("Boulder volume: " + newVol);
        myAudio.volume = newVol;
    }

    //Function returns the distance between the player and the object
    private float DistanceFromPlayer()
    {
        return ((playerTransform.position - transform.position).magnitude);
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

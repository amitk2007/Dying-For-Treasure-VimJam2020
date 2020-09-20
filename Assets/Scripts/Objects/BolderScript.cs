using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolderScript : MonoBehaviour
{
    [Range(0, 10)]
    public float TTL;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyThisBolder(TTL));
    }

    IEnumerator DestroyThisBolder(float seconds)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}

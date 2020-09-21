using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BolderCreatorScript : MonoBehaviour
{
    #region Inspector Variables
    public GameObject bolder;
    public Vector2 startForce;
    [Range(0, 10)]
    public float secondToWait;
    [Range(0, 10)]
    public float BolderTTL = 0;
    #endregion
    #region Variables
    static GameObject thisBolder;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        CreateBolder();
        //WaitForSeconds(3);
        StartCoroutine(CreateBolders(secondToWait));
    }

    //call the creat bolder function every X seconds
    IEnumerator CreateBolders(float secondToWait)
    {
        while (true)
        {
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(secondToWait);
            CreateBolder();
        }
    }

    //Creat a bolder with start speed and TTL
    public void CreateBolder()
    {
        thisBolder = Instantiate(bolder, this.transform.localPosition, Quaternion.identity);
        thisBolder.GetComponent<Rigidbody2D>().AddForce(startForce);
        if (BolderTTL != 0)
        {
            thisBolder.GetComponent<BolderScript>().TTL = BolderTTL;
        }
    }
}

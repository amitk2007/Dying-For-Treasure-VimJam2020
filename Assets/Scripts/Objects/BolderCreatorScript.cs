using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BolderCreatorScript : MonoBehaviour
{
    public GameObject bolder;
    public Vector2 startForce;
    [Range(0, 10)]
    public float secondToWait;
    static GameObject thisBolder;

    // Start is called before the first frame update
    void Start()
    {
        CreateBolder();
        //WaitForSeconds(3);
        StartCoroutine(CreateBolders(secondToWait));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CreateBolders(float secondToWait)
    {
        while (true)
        {
            //Print the time of when the function is first called.
            Debug.Log("Started Coroutine at timestamp : " + Time.time);

            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(secondToWait);
            CreateBolder();
            //After we have waited 5 seconds print the time again.
            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        }
    }

    public void CreateBolder()
    {
        print("creating");
        thisBolder = Instantiate(bolder);
        thisBolder.GetComponent<Rigidbody2D>().AddForce(startForce);
    }
}

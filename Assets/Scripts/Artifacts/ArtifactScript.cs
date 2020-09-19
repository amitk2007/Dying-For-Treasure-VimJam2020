using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to artifact objects
public class ArtifactScript : MonoBehaviour
{
    [SerializeField] private ArtifactType myArtifactType;
    public Artifact MyArtifact;

    //In the beginning, we generate data for the artifact based on its type
    //We then configure the artifact visually accordingly
    void Start()
    {
        MyArtifact = new Artifact(myArtifactType);
        SetupArtifactObject();
    }

    private void SetupArtifactObject()
    {
        this.name = "Artifact: " + MyArtifact.GetName();
        this.GetComponent<SpriteRenderer>().color = MyArtifact.GetColor();
        this.GetComponent<SpriteRenderer>().sprite = MyArtifact.GetSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collided with player, time to bestow curse on him and give him artifact
        if (collision.tag=="Player")
        {
            collision.GetComponent<PlayerArtifacts>().GivePlayerArtifact(MyArtifact);
            if (myArtifactType == ArtifactType.Poison)
                Destroy(gameObject);
        }
    }
}

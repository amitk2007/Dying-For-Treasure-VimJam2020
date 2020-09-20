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
        SetupArtifactObject(this.gameObject, MyArtifact);
    }

    public static void SetupArtifactObject(GameObject artifactObject, Artifact artifact)
    {
        artifactObject.name = "Artifact: " + artifact.GetName();
        artifactObject.GetComponent<SpriteRenderer>().color = artifact.GetColor();
        artifactObject.GetComponent<SpriteRenderer>().sprite = artifact.GetSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collided with player, time to bestow curse on him and give him artifact
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerArtifacts>().GivePlayerArtifact(MyArtifact);
            if (myArtifactType == ArtifactType.Poison)
                Destroy(gameObject);
            else if (myArtifactType == ArtifactType.Slow)
            {
                collision.GetComponent<PlayerArtifacts>().BestowSlowCurse();
                Destroy(gameObject);
            }
        }
    }
}

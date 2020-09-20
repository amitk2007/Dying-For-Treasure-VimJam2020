using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script is attached to slow artifact which is now being carried by player
//It makes the artifact sit on top of his head in a funny way
public class SlowArtifactOnHeadScript : MonoBehaviour
{
    private Transform playerToFollow;
    private int stackNumber = 0;//Parameter tells us how many slow artifacts player has collected (inclusive)
    [SerializeField] private float EdgeFactor=0.15f;//Parameter controls how much to let artifact 'drag' from player's head. 0 means it completely follows player.

    public void SetPlayerToFollow(Transform transform, int stackNumber)
    {
        playerToFollow = transform;
        this.stackNumber = stackNumber;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, playerToFollow.position.x - EdgeFactor * stackNumber, playerToFollow.position.x + EdgeFactor * stackNumber), playerToFollow.position.y + stackNumber); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is attached to the player object, and is responsible for handling the artifacts he has
public class PlayerArtifacts : MonoBehaviour
{
    private List<Artifact> myArtifacts;//Contains a list of all the artifacts the player "owns"

    //Parameters specific to poison artifacts
    [SerializeField] private float PoisonDamageIntervals = 2f;
    [SerializeField] private int PoisonDamage = 1;
    [SerializeField] private float PoisonGreenPercentage = 0.5f;
    private Dictionary<Artifact, float> poisonArtifactTimers;

    //When starting, reset all artifacts and artifact effects
    private void Start()
    {
        ClearPlayerArtifacts();
    }

    public void ClearPlayerArtifacts()
    {
        myArtifacts = new List<Artifact>();
        poisonArtifactTimers = new Dictionary<Artifact, float>();
    }

    public void GivePlayerArtifact(Artifact artifact)
    {
        myArtifacts.Add(artifact);
    }

    //Returns the total value of all the artifacts the player owns
    public int GetArtifactValueTotal()
    {
        int total = 0;
        foreach (Artifact artifact in myArtifacts)
            total += artifact.GetValue();
        return total;
    }

    //On update, we handle each artifact the player owns, if it requires it
    private void Update()
    {
        foreach (Artifact artifact in myArtifacts)
            HandleArtifactCurse(artifact);
    }

    private void HandleArtifactCurse(Artifact artifact)
    {
        switch (artifact.GetArtifactType())
        {
            //For poison, our effect is as follows:
            // 1. Damage player by artifact damage every X seconds
            // 2. Gradually color player more and more green until he is damaged, looping
            case ArtifactType.Poison:
                if (!poisonArtifactTimers.ContainsKey(artifact))
                    poisonArtifactTimers.Add(artifact, 0f);
                poisonArtifactTimers[artifact] = Time.deltaTime + poisonArtifactTimers[artifact];
                float colorFlashMinus = 1 - poisonArtifactTimers[artifact]/PoisonDamageIntervals * PoisonGreenPercentage;
                //We will color the player only if this is the darkest green - if he is poisoned by other artifact, we don't want to override it.
                if (colorFlashMinus < this.GetComponent<SpriteRenderer>().color.r || poisonArtifactTimers[artifact] <= Time.deltaTime)
                    this.GetComponent<SpriteRenderer>().color = new Color(colorFlashMinus, 1f, colorFlashMinus);
                if (poisonArtifactTimers[artifact] >= PoisonDamageIntervals)
                {
                    poisonArtifactTimers[artifact] = 0f;
                    this.GetComponent<PlayerHealth>().TakeDamage(PoisonDamage);
                }
                break;
        }
    }
}

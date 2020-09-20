using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script contains the artifact class, which contains all the data relevant to identifying an artifact.
//If you are adding a new artifact type, this is where to do it. You will need to:
// 1. Add your artifact type to the ArtifactType enum
// 2. Add synonims for your artifact & to use its synonims in the switch case inside GenerateName
// 3. Add its range of values to the switch case inside GenerateValue
// 4. Add its sprite WITH THE NAME MATCHING THE ArtifactType ENUM to the folder resources/ArtifactSprites.
// 5. Add behavior of specific artifact either to PlayerArtifacts script under HandleArtifactCurse or under unique script

public class Artifact
{
    private string Name;
    private int Value;
    private Color Color;
    private Sprite Sprite;
    private ArtifactType ArtifactType;

    public string GetName()
    {
        return this.Name;
    }

    public int GetValue()
    {
        return this.Value;
    }

    public Color GetColor()
    {
        return this.Color;
    }

    public Sprite GetSprite()
    {
        return this.Sprite;
    }

    public ArtifactType GetArtifactType()
    {
        return this.ArtifactType;
    }

    public Artifact (ArtifactType artifactType)
    {
        ArtifactType = artifactType;
        Name = GenerateName(artifactType);
        Value = GenerateValue(artifactType);
        Color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
        Sprite = GetSpriteByArtifactType(ArtifactType);
    }

    private static string GenerateName(ArtifactType artifactType)
    {
        string typeSynonim = "";
        switch (artifactType)
        {
            case ArtifactType.Poison:
                typeSynonim = poisonSynonims[Random.Range(0, poisonSynonims.Length)];
                break;
        }
        if (Random.Range(0, 2) == 0)
            return (peopleNames[Random.Range(0, peopleNames.Length)] + "'s " + typeSynonim + " " + artifactSynonims[Random.Range(0, artifactSynonims.Length)]);
        else
            return (typeSynonim + " " + artifactSynonims[Random.Range(0, artifactSynonims.Length)] + " of " + peopleNames[Random.Range(0, peopleNames.Length)]);
    }

    private static string[] peopleNames = { "Solomon", "Goliath", "Gandalf", "Saruman", "Sauron", "David", "Messiah", "Poseidon", "Zeus", "Ares", "Athena", "Bob", "Hades", "EmmetGames", "Titan", "Pepe" };
    private static string[] artifactSynonims = { "Relic", "Antique", "Wonder", "Artifact", "Artifact", "Treasure", "Talisman" };
    private static string[] poisonSynonims = { "Poisonous", "Toxic", "Radioactive", "Entoxicating", "Cursed", "Leeching" };

    private static int GenerateValue(ArtifactType artifactType)
    {
        switch(artifactType)
        {
            case ArtifactType.Poison:
                return Random.Range(100, 200);
        }
        return 0;
    }

    private Sprite GetSpriteByArtifactType(ArtifactType artifactType)
    {
        return Resources.Load<Sprite>("ArtifactSprites/" + artifactType.ToString());
    }
}

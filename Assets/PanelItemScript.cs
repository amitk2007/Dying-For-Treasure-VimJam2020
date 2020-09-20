using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelItemScript : MonoBehaviour
{
    [SerializeField] private Image ArtifactSprite;
    [SerializeField] private TextMeshProUGUI ArtifactValue;
    [SerializeField] private TextMeshProUGUI ArtifactName;

    public void SetupArtifact(Sprite artifactSprite, int artifactValue, string artifactName )
    {
        ArtifactSprite.sprite = artifactSprite;
        ArtifactValue.text = "<color=#ffff00>" + artifactValue.ToString() + "G</color>";
        ArtifactName.text = artifactName;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WinPanelScript : MonoBehaviour
{
    [SerializeField] private PanelItemScript PanelItemPrefab;
    [SerializeField] private TextMeshProUGUI TotalGText;
    [SerializeField] private VerticalLayoutGroup ItemsCollectedParent;
    [SerializeField] private Button NextLevelButton;

    public void SetupWinPanel(List<Artifact> collectedArtifacts)
    {
        int sum = 0;
        foreach (Artifact artifact in collectedArtifacts)
        {
            GameObject go = (Instantiate(PanelItemPrefab.gameObject, ItemsCollectedParent.transform)) as GameObject;
            go.GetComponent<PanelItemScript>().SetupArtifact(artifact.GetSprite(), artifact.GetValue(), artifact.GetName());
            sum += artifact.GetValue();
        }
        TotalGText.text = "TOTAL: <color=#ffff00>" + sum + "G</color>";
        SetupNextLevelButton();
    }

    private void SetupNextLevelButton()
    {
        string nextLevelName = GetNextLevelName();
        if (nextLevelName == null)
            NextLevelButton.interactable = false;
        else
        {
            NextLevelButton.interactable = true;
            NextLevelButton.onClick.AddListener(delegate { GenericButtonClass.MoveToNextLevel(); });
        }
    }

    private string GetNextLevelName()
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        for (int i=0;i<LevelList.Levels.Length;i++)
        {
            if (currentLevel == LevelList.Levels[i] && i + 1 < LevelList.Levels.Length)
                return LevelList.Levels[i + 1];
        }
        return null;
    }
}

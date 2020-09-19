using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Script is responsible for animating the text on the player
//It is attached to his local world canvas
public class PlayerCanvasAnimationManager : MonoBehaviour
{
    public enum PlayerCanvasAnimation { Nothing, DamageTaken, ItemFound };
    private static Dictionary<PlayerCanvasAnimation, int> DictionaryAnimationToIndex;
    private Animator myAnimator;
    private TextMeshProUGUI myText;

    private void Start()
    {
        myAnimator = this.GetComponent<Animator>();
        myText = this.GetComponentInChildren<TextMeshProUGUI>();
        SetupAnimationDictionary();
    }

    public static int GetAnimationIndex(PlayerCanvasAnimation animation)
    {
        return DictionaryAnimationToIndex[animation];
    }

    //This dictionary is used to map between enums and animation indexes
    private void SetupAnimationDictionary()
    {
        DictionaryAnimationToIndex = new Dictionary<PlayerCanvasAnimation, int>();
        DictionaryAnimationToIndex.Add(PlayerCanvasAnimation.Nothing, 0);
        DictionaryAnimationToIndex.Add(PlayerCanvasAnimation.DamageTaken, 1);
        DictionaryAnimationToIndex.Add(PlayerCanvasAnimation.ItemFound, 2);
    }

    public void PlayAnimation(PlayerCanvasAnimation playerCanvasAnimation, string text = "")
    {
        myText.text = text;
        myAnimator.SetInteger("PlayerCanvasState", GetAnimationIndex(playerCanvasAnimation));
    }

    //Make sure UI doesn't rotate with player
    private void Update()
    {
        LockRotation();
    }

    private void LockRotation()
    {
        transform.localScale = new Vector3 (GetNormalizedNumber(transform.parent.localScale.x) * Mathf.Abs(transform.localScale.x), GetNormalizedNumber(transform.parent.localScale.y) * Mathf.Abs(transform.localScale.y), GetNormalizedNumber(transform.parent.localScale.z) * Mathf.Abs(transform.localScale.z));
    }

    private int GetNormalizedNumber(float num)
    {
        return ((int)(num / Mathf.Abs(num)));
    }
}

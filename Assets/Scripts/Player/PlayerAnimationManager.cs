using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script is responsible for managing and prioritizing player animations
public class PlayerAnimationManager : MonoBehaviour
{
    private Animator animator;

    private static Dictionary<PlayerAnimationState, int> DictionaryAnimationToIndex;
    private static Dictionary<PlayerAnimationState, int> DictionaryAnimationToPriority;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        SetupAnimationDictionary();
    }

    public static int GetAnimationIndex(PlayerAnimationState animation)
    {
        return DictionaryAnimationToIndex[animation];
    }

    //This dictionary is used to map between enums and animation indexes
    private void SetupAnimationDictionary()
    {
        AddToDictionaries(PlayerAnimationState.idle, 0);
        AddToDictionaries(PlayerAnimationState.walking, 1);
        AddToDictionaries(PlayerAnimationState.jump, 3);
        AddToDictionaries(PlayerAnimationState.hurt, 4,5);
        AddToDictionaries(PlayerAnimationState.death, 5,1);
        AddToDictionaries(PlayerAnimationState.victory, 6,4);
        AddToDictionaries(PlayerAnimationState.climbing, 7);
    }

    public void PlayNonInterruptAnimation(PlayerAnimationState state, PlayerAnimationState afterState)
    {
        StartCoroutine(PlayNonInterruptAnimationCoroutine(state, afterState));
    }

    private IEnumerator PlayNonInterruptAnimationCoroutine(PlayerAnimationState state, PlayerAnimationState afterState)
    {
        SetAnimationState(state);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetInteger("State", DictionaryAnimationToIndex[afterState]);
    }

    private void AddToDictionaries(PlayerAnimationState state, int index, int priority = 10)
    {
        if (DictionaryAnimationToIndex == null)
            DictionaryAnimationToIndex = new Dictionary<PlayerAnimationState, int>();
        if (DictionaryAnimationToPriority == null)
            DictionaryAnimationToPriority = new Dictionary<PlayerAnimationState, int>();
        DictionaryAnimationToIndex.Add(state, index);
        DictionaryAnimationToPriority.Add(state, priority);
    }

    public void SetAnimationState(int newStateIndex)
    {
        PlayerAnimationState newState = GetAnimationByIndex(newStateIndex);
        if (PriorityAnimation(newState))
        {
            animator.SetInteger("State", newStateIndex);
        }
    }

    public void SetAnimationState(PlayerAnimationState newState)
    {
        if (PriorityAnimation(newState))
        {
            animator.SetInteger("State", DictionaryAnimationToIndex[newState]);
        }
    }

    //Function returns true if animation is of higher priority than what is currently playing.
    //Priority is smaller number == bigger priority
    private bool PriorityAnimation(PlayerAnimationState newState)
    {
        return DictionaryAnimationToPriority[newState] <= GetPriorityOfCurrentAnimation();
    }

    private int GetPriorityOfCurrentAnimation()
    {
        int currentAnimationIndex = animator.GetInteger("State");
        return (DictionaryAnimationToPriority[GetAnimationByIndex(currentAnimationIndex)]);
    }

    private PlayerAnimationState GetAnimationByIndex(int index)
    {
        foreach (KeyValuePair<PlayerAnimationState, int> entry in DictionaryAnimationToIndex)
        {
            if (index == entry.Value)
                return (entry.Key);
        }
        return PlayerAnimationState.idle;
    }
}

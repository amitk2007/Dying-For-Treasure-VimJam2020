using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script is responsible for managing and prioritizing player animations
public class PlayerAnimationManager : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AudioClip Sfx_Running;
    [SerializeField] private AudioClip Sfx_Jumping;
    [SerializeField] private AudioClip Sfx_Climbing;
    [SerializeField] private AudioClip Sfx_Death;
    [SerializeField] private AudioClip Sfx_Damage;
    [SerializeField] private AudioClip Sfx_Victory;
    private AudioSource myAudioSource;

    private static Dictionary<PlayerAnimationState, int> DictionaryAnimationToIndex;
    private static Dictionary<PlayerAnimationState, int> DictionaryAnimationToPriority;
    private static Dictionary<PlayerAnimationState, AudioClip> DictionaryAnimationToSfx;
    private static Dictionary<PlayerAnimationState, bool> DictionaryAnimationToLoopSfx;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        myAudioSource = this.GetComponent<AudioSource>();
        SetupAnimationDictionary();
    }


    public static int GetAnimationIndex(PlayerAnimationState animation)
    {
        return DictionaryAnimationToIndex[animation];
    }
    
    //Function plays audio clip based on dictionary
    private void PlayCorrespondingSfx(PlayerAnimationState state)
    {
        AudioClip clipToPlay = DictionaryAnimationToSfx[state];
        if (myAudioSource.clip != clipToPlay)
        {
            myAudioSource.Stop();
            if (myAudioSource != null)
            {
                myAudioSource.clip = clipToPlay;
                myAudioSource.loop = DictionaryAnimationToLoopSfx[state];
                myAudioSource.Play();
            }
        }
    }

    //This dictionary is used to map between enums and animation indexes
    private void SetupAnimationDictionary()
    {
        AddToDictionaries(PlayerAnimationState.idle, 0, null, false);
        AddToDictionaries(PlayerAnimationState.walking, 1, Sfx_Running, true);
        AddToDictionaries(PlayerAnimationState.jump, 3, Sfx_Jumping, false);
        AddToDictionaries(PlayerAnimationState.hurt, 4, Sfx_Damage, false, 5);
        AddToDictionaries(PlayerAnimationState.death, 5, Sfx_Death, false, 1);
        AddToDictionaries(PlayerAnimationState.victory, 6, Sfx_Victory, false ,4);
        AddToDictionaries(PlayerAnimationState.climbing, 7, Sfx_Climbing, true);
    }

    public void PlayNonInterruptAnimation(PlayerAnimationState state, PlayerAnimationState afterState)
    {
        StartCoroutine(PlayNonInterruptAnimationCoroutine(state, afterState));
    }

    private IEnumerator PlayNonInterruptAnimationCoroutine(PlayerAnimationState state, PlayerAnimationState afterState)
    {
        SetAnimationState(state);
        PlayCorrespondingSfx(state);
        yield return new WaitForSeconds(Time.deltaTime);
        float waitTime = animator.GetCurrentAnimatorStateInfo(0).length;// + animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        yield return new WaitForSeconds(waitTime);
        animator.SetInteger("State", DictionaryAnimationToIndex[afterState]);
        PlayCorrespondingSfx(afterState);
    }

    //Returns false if player is doing the victory pose, meaning he shouldn't be damaged.
    public bool CheckNotVictoryPosing()
    {
        bool isNotDoingVictoryAnimation = animator.GetInteger("State") != DictionaryAnimationToIndex[PlayerAnimationState.victory];
        //Debug.Log(isNotDoingVictoryAnimation ? "Player is not doing the victory pose" : "Player is doing the victory pose");
        return (isNotDoingVictoryAnimation);
    }

    private void AddToDictionaries(PlayerAnimationState state, int index, AudioClip audioToPlay, bool loopAudio, int priority = 10)
    {
        if (DictionaryAnimationToIndex == null)
            DictionaryAnimationToIndex = new Dictionary<PlayerAnimationState, int>();
        if (DictionaryAnimationToPriority == null)
            DictionaryAnimationToPriority = new Dictionary<PlayerAnimationState, int>();
        if (DictionaryAnimationToSfx == null)
            DictionaryAnimationToSfx = new Dictionary<PlayerAnimationState, AudioClip>();
        if (DictionaryAnimationToLoopSfx == null)
            DictionaryAnimationToLoopSfx = new Dictionary<PlayerAnimationState, bool>();
        DictionaryAnimationToIndex.Add(state, index);
        DictionaryAnimationToPriority.Add(state, priority);
        DictionaryAnimationToSfx.Add(state, audioToPlay);
        DictionaryAnimationToLoopSfx.Add(state, loopAudio);
    }

    public void SetAnimationState(int newStateIndex)
    {
        PlayerAnimationState newState = GetAnimationByIndex(newStateIndex);
        if (PriorityAnimation(newState))
        {
            animator.SetInteger("State", newStateIndex);
            PlayCorrespondingSfx(newState);
        }
    }

    public void SetAnimationState(PlayerAnimationState newState)
    {
        if (PriorityAnimation(newState))
        {
            animator.SetInteger("State", DictionaryAnimationToIndex[newState]);
            PlayCorrespondingSfx(newState);
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

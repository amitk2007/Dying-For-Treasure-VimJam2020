using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyButtonAudio : MonoBehaviour
{
    private static DoNotDestroyButtonAudio doNotDestroyButtonAudio;
    public static AudioSource ButtonAudioSource;

    // Start is called before the first frame update
    void Awake()
    {
        if (doNotDestroyButtonAudio == null || doNotDestroyButtonAudio == this)
            doNotDestroyButtonAudio = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);
        ButtonAudioSource = this.GetComponent<AudioSource>();
    }
}

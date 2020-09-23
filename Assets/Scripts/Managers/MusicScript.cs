using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public static MusicScript musicScript;
    public static bool MusicOn = true;
    public static bool SoundOn = true;
    private AudioSource myAudio;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (musicScript == null || musicScript == this)
        {
            musicScript = this;
            myAudio = this.GetComponent<AudioSource>();
        }
        else
            Destroy(gameObject);
    }

    public AudioClip GetClip()
    {
        return myAudio.clip;
    }

    // Update is called once per frame
    void Update()
    {
        myAudio.mute = !MusicOn;
    }
}

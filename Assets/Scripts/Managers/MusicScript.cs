using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public static MusicScript musicScript;
    public static bool MusicOn = true;
    private AudioSource myAudio;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (musicScript == null)
            musicScript = this;
        else
            Destroy(gameObject);
        myAudio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        myAudio.mute = !MusicOn;
    }
}

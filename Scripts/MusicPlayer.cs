using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
    AudioSource fxSound;
    public AudioClip backMusic;
    // Use this for initialization
    void Start()
    {
        fxSound = GetComponent<AudioSource>();
        fxSound.Play();
    }
}

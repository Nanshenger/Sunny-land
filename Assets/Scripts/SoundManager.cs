using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /*
    BGM01 Jump1 Hit8 Shoot02
    */
    public static SoundManager instance;

    public AudioSource audioSource;
    public AudioClip jumpAudio, hurtAudio, cherryAudio;

    public void Awake()
    {
        instance = this;
    }
    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.volume = 0.1f;
        audioSource.Play();
    }
    public void HurtAudio()
    {
        audioSource.clip = hurtAudio;
        audioSource.volume = 0.1f;
        audioSource.Play();
    }
    public void CherryAudio()
    {
        audioSource.clip = cherryAudio;
        audioSource.volume = 0.1f;
        audioSource.Play();
    }
}

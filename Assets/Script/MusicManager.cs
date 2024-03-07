using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip playOnStart;

    [SerializeField] float timeToSwitch;

    AudioClip switchTo;

    [SerializeField] float volume;

    [Range(0f, 1f)]
    [SerializeField] float maxVolume;

    private void Start()
    {
        Play(playOnStart,true);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.KeypadMinus))
        {
            maxVolume -= Time.deltaTime;
            audioSource.volume = maxVolume;
        }
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            maxVolume += Time.deltaTime;
            audioSource.volume = maxVolume;
        }
    }

    public void Play(AudioClip musicToPlay, bool interrupt = false)
    {
        if(musicToPlay == null) { return; }

        if (interrupt)
        {
            audioSource.volume = maxVolume;
            audioSource.clip = musicToPlay;
            audioSource.Play();
        } else
        {
            switchTo = musicToPlay;
            StartCoroutine(SmoothSwitchMusic());
        }
    }

    IEnumerator SmoothSwitchMusic()
    {
        volume = maxVolume;
        while(volume > 0f)
        {
            volume -= Time.deltaTime/ timeToSwitch;

            if(volume < 0f)
            {
                volume = 0f;
            }
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }
        Play(switchTo, true);
    }
}

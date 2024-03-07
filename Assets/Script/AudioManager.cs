using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] GameObject audioSourcePrefab;

    [SerializeField] int audioSourceCount;

    List<AudioSource> audioSourcesList;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        audioSourcesList = new List<AudioSource>();

        for(int i = 0; i < audioSourceCount; i++)
        {
            GameObject go = Instantiate(audioSourcePrefab,transform);
            go.transform.localPosition = Vector3.zero;
            audioSourcesList.Add(go.GetComponent<AudioSource>());
        }
    }

    public void Play(AudioClip audioClip, float volume = 1f)
    {
        AudioSource audioSource = GetFreeAudioSorce();

        audioSource.volume = volume;
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private AudioSource GetFreeAudioSorce()
    {
        for(int i = 0;i < audioSourcesList.Count;i++)
        {
            if (audioSourcesList[i].isPlaying == false)
            {
                return audioSourcesList[i];
            }
        }

        return audioSourcesList[0];
    }

}

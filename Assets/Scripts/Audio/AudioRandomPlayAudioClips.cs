using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomPlayAudioClips : MonoBehaviour
{
    public List<AudioClip> audioClipList;

    public AudioSource audioSource;

    public void PlayRandom()
    {
        audioSource.clip = audioClipList[Random.Range(0, audioClipList.Count)];
        audioSource.Play();
    }



    /*outra forma de fazer lista de audio ("Pool")
    public List<AudioClip> audioClipList;

    public List<AudioSource> audioSourceList;

    private int _index = 0;

    public void PlayRandom()
    {
        if (_index >= audioSourceList.Count) index = 0;
        
        var audioSource = audioClipList[_index];

        audioSource.clip = audioClipList[Random.Range(0, audioClipList.Count)];
        audioSource.Play();

        _index++;
    }*/
}

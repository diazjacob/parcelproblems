using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip[] _allClips;

    public AudioClip _music;



    void Start()
    {
        
    }



    void Update()
    {
        
    }

    public void PlayClip(int i )
    {
        if (i >= 0 && i < _allClips.Length )
        {
            audio.Stop();
            audio.clip = _allClips[i];
            audio.Play();
        }
    }
}

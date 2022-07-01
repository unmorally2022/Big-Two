using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;
    // Start is called before the first frame update
    void Start()
    {
        AppManager.audioManager = this;   
    }

    public void PlayOnce(int index) {
        audioSources[index].Play(0);
    }
}

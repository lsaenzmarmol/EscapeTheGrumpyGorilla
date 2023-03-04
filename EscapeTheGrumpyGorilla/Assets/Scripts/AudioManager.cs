using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    public AudioSource speaker;
    public GameObject chill, rilla;
    public AudioClip slip, explosion, toss;
    public float volume;

    public void PlaySlip()
    {
        speaker.PlayOneShot(slip, volume);
    }
    public void PlayExplosion()
    {
        rilla.SetActive(true);
        chill.SetActive(false);
        //speaker.PlayOneShot(explosion, volume);
    }
    public void PlayToss()
    {
        speaker.PlayOneShot(toss, volume);
    }
    public void TurnOffMusic()
    {
        rilla.SetActive(false);
        chill.SetActive(false);
    }
    public void TurnOnMusic()
    {
        rilla.SetActive(false);
        chill.SetActive(true);
    }
}

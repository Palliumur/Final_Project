using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    public static Victory instance;
    public AudioClip victorySound;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().clip = victorySound;
        GetComponent<AudioSource>().Play();
    }
}

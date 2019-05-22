using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public static AudioSource output = GameObject.Find("Canvas").GetComponent<AudioSource>();




    public static void playSound(string path)
    {
        Debug.Log(path + " played");
        output.clip = Resources.Load<AudioClip>(path);
        output.Play();
    }
}

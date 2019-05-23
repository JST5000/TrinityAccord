using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public static AudioSource output = GameObject.Find("Canvas").GetComponent<AudioSource>();




    public static void playSound(string path)
    {
        if (output == null)
        {
              output = GameObject.Find("Canvas").GetComponent<AudioSource>();

        }
        Debug.Log(path + " played");
        output.clip = Resources.Load<AudioClip>(path);
        output.Play();
    }
}

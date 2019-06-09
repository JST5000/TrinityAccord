using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public static AudioSource output = GameObject.Find("Canvas").GetComponent<AudioSource>();


    static string folder = "Sounds/";

    public static void playSound(string path)
    {
        if (output == null)
        {
              output = GameObject.Find("Canvas").GetComponent<AudioSource>();

        }
        Debug.Log(folder + path + " played");
        output.clip = Resources.Load<AudioClip>(folder + path);
        output.Play();
    }
}

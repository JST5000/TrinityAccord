using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public static AudioSource output = GameObject.Find("Canvas").GetComponent<AudioSource>();

    public static bool EnemyAttackSoundsOn { get; set; } = true;

    public static bool PlayerCardSoundsOn { get; set; } = true;

    public static bool BackgroundMusicOn { get; set; } = true;

    static string folder = "Sounds/";

    //TODO make this the card sound effect method, then make one for each of the settings above!
    public static void PlayCardSFX(string path)
    {
        if (PlayerCardSoundsOn)
        {
            PlaySound(path);
        }
    }

    public static void PlayEnemySFX(string path)
    {
        if(EnemyAttackSoundsOn)
        {
            PlaySound(path);
        }
    }

    private static void PlaySound(string path)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMLoader : MonoBehaviour
{
    public static AudioSource[] backgroundMusicSources;

    private string folder = "Sounds/";

    private string waterIntro = "Intro-Water-Level-Draft-1 (1)";
    private string waterLoop = "Loop-Water-Level-Draft-1 (1)";

    private double timeToStartLoop = 0f;

    private bool playingMusic;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMusicSources = GameObject.Find("SoundSettingChanger").GetComponents<AudioSource>();
        PlayMusicWithIntro();
    }

    // Update is called once per frame
    void Update()
    {
        if(playingMusic != SoundManager.BackgroundMusicOn)
        {
            foreach(AudioSource source in backgroundMusicSources)
            {
                if (SoundManager.BackgroundMusicOn)
                {
                    source.UnPause();
                } else
                {
                    source.Pause();
                }
            }
            playingMusic = SoundManager.BackgroundMusicOn;
        }
    }

    void PlayMusicWithIntro()
    {
        backgroundMusicSources[0].loop = false;
        backgroundMusicSources[0].clip = Resources.Load<AudioClip>(folder + waterIntro);
        backgroundMusicSources[0].Play();

        backgroundMusicSources[1].loop = true;
        backgroundMusicSources[1].clip = Resources.Load<AudioClip>(folder + waterLoop);
        backgroundMusicSources[1].PlayScheduled(backgroundMusicSources[0].clip.length + AudioSettings.dspTime);

        if (!SoundManager.BackgroundMusicOn || !PermanentState.HasDraftedClassCard)
        {
            backgroundMusicSources[0].Pause();
            backgroundMusicSources[1].Pause();
        }

        playingMusic = SoundManager.BackgroundMusicOn;
    }
}

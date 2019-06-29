﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuManager : MonoBehaviour
{
    public Transform Help;

    public void Awake()
    {
        
    }

    public void Resume()
    {
        GetComponent<AdminHidden>().ExternalDisable();
    }

    public void OpenHelp()
    {
        Transform help = Instantiate(Help, GameObject.Find("Canvas").transform, false);
        Vector3 newScale = MatchTransform.SetScaleToMatch((RectTransform)help, (RectTransform)help.parent.transform);
        help.GetComponent<HelpManager>().RelativeScale = newScale;
    }

    public void OpenGlossary()
    {
        Transform instance = Instantiate(Help, GameObject.Find("Canvas").transform, false);
        Vector3 newScale = MatchTransform.SetScaleToMatch((RectTransform)instance, (RectTransform)instance.parent.transform);
        HelpManager help = instance.GetComponent<HelpManager>();
        help.RelativeScale = newScale;
        help.SetPage(HelpManager.GlossaryPage);
    }

    public void ExitToTitle()
    {
        SceneManager.LoadScene("Main Menu");
        Destroy(transform.parent.gameObject);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

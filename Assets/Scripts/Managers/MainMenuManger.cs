using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManger : MonoBehaviour
{

    public GameObject Help;
    public CanvasGroup Star;

    public void Awake()
    {
        //Shows star if player has completed the game!
        GameObject perm = GameObject.Find("HasWon");
        if (perm == null)
        {
            CanvasGroupManip.Disable(Star);
        }
    }

    public void StartNewGame()
    { 
        //Removes previous game data
        GameObject permanentState = GameObject.Find("PermanentState");
        if (permanentState != null)
        {
            Destroy(permanentState);
        }
        PermanentState.ResetStatics();
        SceneManager.LoadScene("Encounter");
    }

    public void OpenHelp()
    {
        Instantiate(Help, GameObject.Find("Canvas").transform, false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManger : MonoBehaviour
{

    public GameObject Help;

    public void StartNewGame()
    { 
        //Removes previous game data
        GameObject permanentState = GameObject.Find("PermanentState");
        if(permanentState != null)
        {
            Destroy(permanentState);
        }
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

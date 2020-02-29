using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        PermanentState.HasDraftedClassCard = false;
        SceneManager.LoadScene("Main Menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ChooseEncounterManager : MonoBehaviour
{
    private EnemyData[] selectedEncounter;
    public Button confirm;
    public Text errorMessage;
    public bool reloadSceneOnExit = true;

    public void SelectEasy()
    {
        selectedEncounter = GenerateEncounter.GetEncounter(Level.TUTORIAL);
        confirm.interactable = true;
    }

    public void SelectMedium()
    {
        selectedEncounter = GenerateEncounter.GetEncounter(Level.ONE);
        confirm.interactable = true;
    }

    public void SelectHard()
    {
        selectedEncounter = GenerateEncounter.GetEncounter(Level.TWO);
        confirm.interactable = true;
    }

    public void EnterCustomFight(TextMeshProUGUI inputField)
    {
        string input = inputField.text.Substring(0, inputField.text.Length - 1);
        try
        {
            selectedEncounter = EncounterInterpreter.InterpretText(input);
            confirm.interactable = true;
            errorMessage.text = "";
        }
        catch (KeyNotFoundException)            
        {
            confirm.interactable = false;
            errorMessage.text = "Error: Given text does not form a valid encounter";
        }
    }

    public void ConfirmSelectedEncounter()
    {
        if (selectedEncounter != null)
        {
            PermanentState.SetNextEncounter(selectedEncounter);
            selectedEncounter = null;
            //Reloads the scene, hopefully with the newly selected encounter.
            if (reloadSceneOnExit)
            {
                SceneManager.LoadScene("Encounter");
            }
            else
            {
                //If the scene does not load, get rid of this menu.
                Destroy(gameObject);
            }
        }
    }
}

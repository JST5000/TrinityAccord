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
        string input = inputField.text;
        if (Validate(input))
        {
            confirm.interactable = true;
        } else
        {
            errorMessage.text = "Error: Given text does not form a valid encounter";
        }
    }

    //TODO validate the input is a valid encounter string.
    private bool Validate(string input)
    {
        return false;
    }

    public void ConfirmSelectedEncounter()
    {
        if (selectedEncounter != null)
        {
            PermanentState.SetNextEncounter(selectedEncounter);
            selectedEncounter = null;
            //Reloads the scene, hopefully with the newly selected encounter.
            SceneManager.LoadScene("Encounter");
            //      Destroy(gameObject);
        }
    }
}

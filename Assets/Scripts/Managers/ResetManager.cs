using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    public Transform ChooseEncounterMenu;
    private bool isDisabled;

    // Start is called before the first frame update
    void Start()
    {
        isDisabled = false;
        DisableButton();
    }

    public void ChooseEncounter()
    {
        Instantiate(ChooseEncounterMenu, GameObject.Find("Canvas").transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.BackQuote))
        {
            EnableButton();
        } else
        {
            DisableButton();
        }

    }

    //Checks if change is needed, then updates
    public void DisableButton()
    {
        CanvasGroup reset = GetComponent<CanvasGroup>();
        if (!isDisabled)
        {
            reset.alpha = 0;
            reset.blocksRaycasts = false;
            reset.interactable = false;
            isDisabled = true;
        }
    }

    //Checks if change is needed, then updates
    public void EnableButton()
    {
        CanvasGroup reset = GetComponent<CanvasGroup>();
        if (isDisabled)
        {
            reset.alpha = 1;
            reset.blocksRaycasts = true;
            reset.interactable = true;
            isDisabled = false;
        }
    }
}

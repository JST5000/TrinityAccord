using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminHidden : MonoBehaviour
{

    public enum Reveal { PERMANENT, TOGGLE, WHILE_HELD };
    public Reveal revealType = Reveal.PERMANENT;
    public KeyCode opt1 = KeyCode.Tilde;
    public KeyCode opt2 = KeyCode.BackQuote;
    private bool revealed = false;
    private bool isDisabled;

    // Start is called before the first frame update
    void Start()
    {
        isDisabled = false;
        DisableButton();
    }

    public void ExternalDisable()
    {
        if(revealType == Reveal.TOGGLE)
        {
            DisableButton();
        } else if(revealType == Reveal.PERMANENT)
        {
            revealed = false;
            DisableButton();
        } else
        {
            DisableButton();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(revealType == Reveal.TOGGLE && (Input.GetKeyDown(opt1) || Input.GetKeyDown(opt2))) {
            if (isDisabled)
            {
                EnableButton();
            }
            else
            {
                DisableButton();
            }
        } else if (Input.GetKey(opt1) || Input.GetKey(opt2))
        {
            if(revealType == Reveal.TOGGLE)
            {
                //Do nothing
            } else if (revealType == Reveal.PERMANENT)
            {
                revealed = true;
                EnableButton();
            } else //RevealType == WHILE_HELD
            {
                EnableButton();
            }
        }
        else
        {
            if (!revealed && (revealType != Reveal.TOGGLE))
            {
                DisableButton();
            }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminHidden : MonoBehaviour
{

    public enum Reveal { PERMANENT, TOGGLE, WHILE_HELD };
    public Reveal revealType = Reveal.PERMANENT;
    private bool revealed = false;
    private bool isDisabled;

    // Start is called before the first frame update
    void Start()
    {
        isDisabled = false;
        DisableButton();
    }

    // Update is called once per frame
    void Update()
    {
        if(revealType == Reveal.TOGGLE && (Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown(KeyCode.Tilde))) {
            if (isDisabled)
            {
                EnableButton();
            }
            else
            {
                DisableButton();
            }
        } else if (Input.GetKey(KeyCode.BackQuote) || Input.GetKey(KeyCode.Tilde))
        {
            if(revealType == Reveal.TOGGLE)
            {
                //Do nothing
            } else if (revealType == Reveal.PERMANENT)
            {
                revealed = true;
                EnableButton();
            } else
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

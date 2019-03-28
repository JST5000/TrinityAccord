using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminHidden : MonoBehaviour
{
    public bool permanentReveal = false;
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
        if (Input.GetKey(KeyCode.BackQuote) || Input.GetKey(KeyCode.Tilde))
        {
            EnableButton();
            if (permanentReveal)
            {
                revealed = true;
            }
        }
        else
        {
            if (!revealed)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUIUpdater : MonoBehaviour
{
    public Text costText;
    public Text displayName;
    public TextMeshProUGUI cardEffect;
    public Image background;
    private Color prevBGColor;
    private bool highlighted = false;

    private Color normalTint;

    private CardManager cardHolder;

    public CanvasGroup cardCG;

    //Current status variable, used to save time in updating
    private bool IsOff = false;
    private bool IsDisabled = false;


    // Start is called before the first frame update
    void Start()
    {
        TurnOffCard();
        cardHolder = GetComponentInChildren<CardManager>();
        Button primaryButton = GetComponent<Button>();
        if (primaryButton != null)
        {
            normalTint = primaryButton.colors.normalColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Potentially replace with a notify method if performance becomes an issue
        if (!cardHolder.IsEmpty())
        {
            UpdateUI(cardHolder.GetUICardData());
            TurnOnCard();
        } else
        {
            TurnOffCard();
            EnableCard();
        }

    }

    //Checks if change is needed, then updates
    public void TurnOffCard()
    {
        if (!IsOff)
        {
            cardCG.alpha = 0;
            cardCG.blocksRaycasts = false;
            cardCG.interactable = false;
            IsOff = true;
        }
    }

    //Checks if change is needed, then updates
    public void TurnOnCard()
    {
        if (IsOff)
        {
            cardCG.alpha = 1;
            cardCG.blocksRaycasts = true;
            cardCG.interactable = true;
            IsOff = false;
        }
    }

    public void DisableCard()
    {
        if (!IsDisabled)
        {
            Button button = GetComponent<Button>();
            button.interactable = false;
            IsDisabled = true;
        }
    }

    public void EnableCard()
    {
        if (IsDisabled)
        {
            Button button = GetComponent<Button>();
            button.interactable = true;
            IsDisabled = false;
        }
    }

    public void UpdateUI(UICardData data)
    {
        costText.text = "" + data.cost;
        displayName.text = data.cardName;
        cardEffect.text = data.effectText;
        UpdateBGColor(data.cardType);
    }

    private void UpdateBGColor(UICardData.CardType type)
    {
        if (!highlighted)
        {
            if (type.Equals(UICardData.CardType.ATTACK))
            {
                background.color = Color.red;
            }
            else if (type.Equals(UICardData.CardType.SPELL))
            {
                background.color = Color.cyan;
            }
        }
    }

    public void Highlight()
    {
        highlighted = true;
        var color = background.color;
        prevBGColor = new Color(color.r, color.g, color.b, color.a);
        color = Color.yellow;
        background.color = color;
    }

    public void ResetHighlight()
    {
        highlighted = false;
        background.color = prevBGColor;
    }
}

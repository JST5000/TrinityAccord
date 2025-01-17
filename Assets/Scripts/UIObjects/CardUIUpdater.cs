﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI costText;
    public TextMeshProUGUI displayName;
    public TextMeshProUGUI cardEffect;
    public Image background;
    public Image CardArt;
    public TextMeshProUGUI sharpenedText;
    public CanvasGroup LockIconCG;


    public Sprite AttackBG;
    public Sprite SpellBG;
    public Sprite QuestBG;
    public Sprite HighlightBG;

    public bool blocksRaycasts = true;

    public bool ShowBuffText { get; set; } = true;

    private Sprite prevBG;
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
        LoadBackgrounds();
        Button primaryButton = GetComponent<Button>();
        if (primaryButton != null)
        {
            normalTint = primaryButton.colors.normalColor;
        }
    }

    private void LoadBackgrounds()
    {
        if (AttackBG == null)
        {
            AttackBG = Resources.Load<Sprite>("Card_Art/Attack_Card_Full");
        }
        if (SpellBG == null)
        {
            SpellBG = Resources.Load<Sprite>("Card_Art/Spell_Card_Full");
        }
        if (HighlightBG == null)
        {
            HighlightBG = Resources.Load<Sprite>("Card_Art/Selected_Card_Full");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Potentially replace with a notify method if performance becomes an issue
        //Currentmode as select card from hand means that all are selectable, even if they are not usable
        if (!cardHolder.IsEmpty())
        {
            if (cardHolder.GetUICardData() != null)
            {
                UpdateUI(cardHolder.GetUICardData());
            }
            CanvasGroupManip.SetVisibility(cardHolder.IsPreserved(), LockIconCG, blocksRaycasts);
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
            CanvasGroupManip.Disable(cardCG);
            IsOff = true;
        }
    }

    //Checks if change is needed, then updates
    public void TurnOnCard()
    {
        if (IsOff)
        {
            CanvasGroupManip.Enable(cardCG, blocksRaycasts);
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
        if(cardEffect.text != data.effectText)
        {
            cardEffect.text = data.effectText;
        }
        displayName.text = data.cardName;
        DisplayTextAndCardArtDynamically(data);
        UpdateBGColor(data.cardType);
    }

    private void DisplayTextAndCardArtDynamically(UICardData data)
    {
        if (data.displayOnlyCardArt)
        {
            DisplayWithFullCardArt(data);
        }
        else
        {
            DisplayFullTextWithFadedArt(data);
        }
    }

    //Hides text, shows full card art
    private void DisplayWithFullCardArt(UICardData data)
    {

        if (data.cardArt != null)
        {
            DisplayAvailableCardArt(data);
        } else
        {
            DisplayCardTextOnly(data);
        }


    }

    private void DisplayAvailableCardArt(UICardData data)
    {
        cardEffect.text = data.effectText;
        var color = cardEffect.color;
        color.a = 0f;
        cardEffect.color = color;

        CardArt.sprite = data.cardArt;
        SetCardArtOpacity(1f);
        SetSharpenedTextOpacity(1f);
    }

    private void DisplayCardTextOnly(UICardData data)
    {
        SetCardArtOpacity(0f);
        SetSharpenedTextOpacity(1f);
        cardEffect.text = data.effectText;
        var color = cardEffect.color;
        color.a = 1f;
        cardEffect.color = color;
    }

    private void SetCardArtOpacity(float opacity)
    {
        var color = CardArt.color;
        color.a = opacity;
        CardArt.color = color;
    }

    private void SetSharpenedTextOpacity(float opacity)
    {
        if (sharpenedText != null)
        {
            int amount = cardHolder.GetCardData().GetBonusDamage();


            if (ShowBuffText)
            {
                sharpenedText.text = (amount == 0)
                    ? ""
                    : "+ " + amount;

                sharpenedText.text += cardHolder.GetCardData().GetBonusEffectDescription();
            } else
            {
                sharpenedText.text = "";
            }

            var sharpenedColor = sharpenedText.color;
            sharpenedColor.a = opacity;
            sharpenedText.color = sharpenedColor;
        }
    }
    //Makes the art transparent and initializes text so text is fully readable
    private void DisplayFullTextWithFadedArt(UICardData data)
    {
        cardEffect.text = data.effectText;
        var color = cardEffect.color;
        color.a = 1f;
        cardEffect.color = color;

        float dim = .4f;
        if (data.cardArt != null)
        {
            CardArt.sprite = data.cardArt;
            SetCardArtOpacity(dim);
        } else
        {
            SetCardArtOpacity(0f);
        }
        SetSharpenedTextOpacity(dim);
    }

    private void UpdateBGColor(UICardData.CardType type)
    {
        if (!highlighted)
        {
            if (type.Equals(UICardData.CardType.ATTACK))
            {
                background.sprite = AttackBG;
            }
            else if (type.Equals(UICardData.CardType.SPELL))
            {
                background.sprite = SpellBG;
            }
            else if (type.Equals(UICardData.CardType.QUEST))
            {
                background.sprite = QuestBG;
            }
        }
    }

    public void Highlight()
    {
        highlighted = true;
        prevBG = background.sprite;
        background.sprite = HighlightBG;
    }

    public void ResetHighlight()
    {
        highlighted = false;
        background.sprite = prevBG;
    }
}

using System.Collections;
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


    public Sprite AttackBG;
    public Sprite SpellBG;
    public Sprite HighlightBG;


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
        cardEffect.text = "";
        CardArt.sprite = data.cardArt;
        SetCardArtOpacity(1f);
        SetSharpenedTextOpacity(1f);
    }

    private void DisplayCardTextOnly(UICardData data)
    {
        SetCardArtOpacity(0f);
        SetSharpenedTextOpacity(1f);
        cardEffect.text = data.effectText;
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
            var sharpenedColor = sharpenedText.color;
            sharpenedColor.a = opacity;
            sharpenedText.color = sharpenedColor;
        }
    }
    //Makes the art transparent and initializes text so text is fully readable
    private void DisplayFullTextWithFadedArt(UICardData data)
    {
        cardEffect.text = data.effectText;
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
        }
    }

    public void Highlight()
    {
        highlighted = true;
        prevBG = background.sprite;
        background.sprite = HighlightBG;
        /*
        var color = background.color;
        prevBGColor = new Color(color.r, color.g, color.b, color.a);
        color = Color.yellow;
        background.color = color;
        */
    }

    public void ResetHighlight()
    {
        highlighted = false;
        background.sprite = prevBG;
    }
}

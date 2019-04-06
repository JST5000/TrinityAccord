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
    private bool colorNotSet = true;
    private Color prevBGColor;
    private bool highlighted = false;

    private Button primaryButton;
    private Color normalTint;

    private CardManager cardHolder;

    public CanvasGroup cardCG;

    private Player player;
    //Current status variable, used to save time in updating
    private bool IsOff = false;
    private bool IsDisabled = false;


    // Start is called before the first frame update
    void Start()
    {
        TurnOffCard();
        cardHolder = GetComponentInChildren<CardManager>();
        primaryButton = GetComponent<Button>();
        normalTint = primaryButton.colors.normalColor;
        player = GameObject.Find("Player").GetComponent<Player>();
        //TODO attach this to the cards
        /*     int r = Random.Range(0, 2);
             if (r == 1)
             {
                 UICardData sampleAttack = new UICardData("Lightning", 3, "Deal 3 damage to 3 random enemies.", UICardData.CardType.ATTACK);
                 UpdateUI(sampleAttack);
             } else
             {
                 UICardData sampleSpell = new UICardData("Backpack", 0, "Draw 2, then Discard 2.", UICardData.CardType.SPELL);
                 UpdateUI(sampleSpell);
             } */
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
   /*     if (!cardHolder.IsEmpty())
        {
            int.TryParse(costText.text, out int cost);
            if(player.GetEnergy() < cost)
            {
                DisableCard();
            } else
            {
                EnableCard();
            }
        } */

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
        color = Color.yellow;// new Color(1f, 0.952f, 0.180f);
        background.color = color;
        /*
        var colors = primaryButton.colors;
        colors.normalColor = Color.yellow;
        primaryButton.colors = colors;
        */
        /*
        var color = outline.color;
        color.a = 1f;
        outline.color = color;
        */
    }

    public void ResetHighlight()
    {
        highlighted = false;
        background.color = prevBGColor;
        /*
        var colors = primaryButton.colors;
        colors.normalColor = normalTint;
        primaryButton.colors = colors;
  */
        /*
              var color = outline.color;
              color.a = 0f;
              outline.color = color;
              */
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUIUpdater : MonoBehaviour
{
    public Text costText;
    public Text displayName;
    public Text cardEffect;
    public Image background;

    private CardManager cardHolder;

    public CanvasGroup cardCG;

    private bool isDisabled = true;

    // Start is called before the first frame update
    void Start()
    {
        DisableCard();
        cardHolder = GetComponentInChildren<CardManager>();
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
        //TODO replace with a notify method
        UICardData data = cardHolder.getUICardData();
        if (data != null) { 
            UpdateUI(data);
        }
        if(isDisabled && data != null)
        {
            EnableCard();
            isDisabled = false;
        } else if(!isDisabled && data == null)
        {
            DisableCard();
            isDisabled = true;
        }
    }

    public void DisableCard()
    {
        cardCG.alpha = 0;
        cardCG.blocksRaycasts = false;
        cardCG.interactable = false;
    }

    public void EnableCard()
    {
        cardCG.alpha = 1;
        cardCG.blocksRaycasts = true;
        cardCG.interactable = true;
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
        if(type.Equals(UICardData.CardType.ATTACK))
        {
            background.color = Color.red;
        } else if(type.Equals(UICardData.CardType.SPELL))
        {
            background.color = Color.cyan;
        }
    }
}

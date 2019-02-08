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

    public CanvasGroup cardCG;

    // Start is called before the first frame update
    void Start()
    {
        //TODO attach this to the cards
        int r = Random.Range(0, 2);
        if (r == 1)
        {
            UICardData sampleAttack = new UICardData("Lightning", 3, "Deal 3 damage to 3 random enemies.", UICardData.CardType.ATTACK);
            updateUI(sampleAttack);
        } else
        {
            UICardData sampleSpell = new UICardData("Backpack", 0, "Draw 2, then Discard 2.", UICardData.CardType.SPELL);
            updateUI(sampleSpell);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void disableCard()
    {
        cardCG.alpha = 0;
        cardCG.blocksRaycasts = false;
        cardCG.interactable = false;
    }

    public void enableCard()
    {
        cardCG.alpha = 1;
        cardCG.blocksRaycasts = true;
        cardCG.interactable = true;
    }

    public void updateUI(UICardData data)
    {
        costText.text = "" + data.cost;
        displayName.text = data.cardName;
        cardEffect.text = data.effectText;
        updateBGColor(data.cardType);
    }

    private void updateBGColor(UICardData.CardType type)
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

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

    // Start is called before the first frame update
    void Start()
    {
        //TODO attach this to the cards
        UICardData sample = new UICardData();
        sample.cardName = "Test Attack";
        sample.cardType = UICardData.CardType.ATTACK;
        sample.cost = 3;
        sample.effectText = "Deal 3 damage to 3 random targets.";
        updateUI(sample);
    }

    // Update is called once per frame
    void Update()
    {
        
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

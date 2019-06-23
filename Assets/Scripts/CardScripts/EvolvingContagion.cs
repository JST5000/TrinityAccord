using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : CardData
{
    ContagionCard mode;
    bool displayDefaultMode;

    public Virus()
    {
        BecomeDefaultVirusCard();
    }

    protected override UICardData CreateUICardData()
    {
        if(displayDefaultMode)
        {
            return new UICardData("Virus", cost: 0, "Becomes a random Contagion card when drawn!", UICardData.CardType.SPELL);
        }

        switch (mode)
        {
            case ContagionCard.GREED:
                return new UICardData("Greed", cost: 0, "Draw 2", UICardData.CardType.SPELL);
            case ContagionCard.POWER:
                return new UICardData("Power", cost: 0, "Gain 2 energy", UICardData.CardType.SPELL);
            case ContagionCard.VILE_SWORD:
                return new UICardData("Vile Sword", cost: 2, "Deal " + GetVileSwordDamage() + " damage", UICardData.CardType.ATTACK);
            default:
                Debug.LogError("Unable to recognize mode: " + mode + ".");
                return null;
        }
    }

    public void BecomeRandomContagionCard(bool showDefaultCard = true)
    {
        displayDefaultMode = false;
        mode = ContagionCardMethods.GetRandom();
        switch (mode)
        {
            case ContagionCard.GREED:
                target = Target.BOARD;
                break;
            case ContagionCard.POWER:
                target = Target.BOARD;
                break;
            case ContagionCard.VILE_SWORD:
                target = Target.ENEMY;
                break;
        }
        UpdateUICardData();
    }

    private void BecomeDefaultVirusCard()
    {
        displayDefaultMode = true;
        target = Target.CARD;
        UpdateUICardData();
    }

    private int GetVileSwordDamage()
    {
        return 5 + sharpened;
    }

    public override void Action(EnemyManager[] enemys)
    {
        switch (mode)
        {
            case ContagionCard.GREED:
                draw();
                draw();
                break;
            case ContagionCard.POWER:
                addEnergy(2);
                break;
            case ContagionCard.VILE_SWORD:
                enemys[0].Damage(GetVileSwordDamage());
                break;
        }
        BecomeDefaultVirusCard();
    }

    public override void sharpen()
    {
        if (mode == ContagionCard.VILE_SWORD)
        {
            base.sharpen();
        }
    }

    public override void Action(CardData[] cards)
    {
        BecomeRandomContagionCard();
    }
    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {

    }

    public override void OnDraw()
    {
        playCardRandomTarget(this);
    }

    public override void OnDiscard()
    {
        BecomeDefaultVirusCard();
    }
    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }
}

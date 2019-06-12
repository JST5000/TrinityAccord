using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolvingContagion : CardData
{
    ContagionCard mode;

    public EvolvingContagion() { }

    protected override UICardData CreateUICardData()
    {
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

    public void SetMode()
    {
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
        SetMode();
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

    }
    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {

    }

    public override void OnDraw()
    {
        SetMode();
    }
    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juggle : CardData
{
    public Juggle()
    {
        target = Target.NONE;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Juggle", cost: 1, $"All damage stuns until the end of turn.", UICardData.CardType.SPELL, cardArtFileName: "Juggle");
    }

    private int GetDamage()
    {
        return 0;
    }

    public override void Sharpen()
    {
    }


    public override void Action(EnemyManager[] enemys)
    {
        SoundManager.PlayCardSFX("Juggle1");
        EncounterManager.Get().AllDamageStuns = true;
    }

    public override void Action(CardData[] cards)
    {

    }
    public override void Action(CardData[] cards, EnemyManager[] enemys)
    {

    }

    public override int SecondAction(CardManager card)
    {
        throw new System.NotImplementedException();
    }
}

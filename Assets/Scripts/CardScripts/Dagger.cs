using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : CardData
{
    public Dagger()
    {
        target = Target.ENEMY;
    }

    protected override UICardData CreateUICardData()
    {
        return new UICardData("Dagger", cost: 1, "Deal " + GetDamage() + " damage.", UICardData.CardType.ATTACK, cardArtFileName: "Dagger1");
    }

    private int GetDamage()
    {
        return 1 + sharpenDamage;
    }

    public override void Action(EnemyManager[] enemys)
    {
        SoundManager.playSound("Knife1");
        enemys[0].Damage(GetDamage());
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

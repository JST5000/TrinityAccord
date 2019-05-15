using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Rally : CardData
{
    private int growDamage = 0;
        public Rally()
        {
            cardData = new UICardData("Rally", cost: 2, "Deal 3 damage Grow 2", UICardData.CardType.ATTACK);
            cost = 2;
            target = Target.ENEMY;
        }

        public override void Action(EnemyManager[] enemys)
        {
            enemys[0].Damage(3+growDamage+sharpened);
            growDamage += 2;
            cardData = new UICardData("Rally", cost: 2, "Deal "+(3+growDamage+sharpened)+ " damage Grow 2", UICardData.CardType.ATTACK);

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
    public override void sharpen()
    {
        sharpened++;
        cardData = new UICardData("Rally", cost: 2, "Deal " + (3 + sharpened+growDamage) + " damage Grow 2", UICardData.CardType.ATTACK);
    }
}

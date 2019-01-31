using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum Targets {Enemies,board}
public abstract class CardData
{
    public abstract int Target();
    public abstract string CardName();
    public abstract int Cost();
    public abstract void Action(EnemyManager[] enemys);
    public abstract void Action(CardData[] cards);
    public abstract void Action(CardData[] cards, EnemyManager[] enemys);
}

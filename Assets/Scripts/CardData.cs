using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum Targets {Enemies,board}
public abstract class CardData : MonoBehaviour
{
    public int target;
    public string cardName;
    public int cost;
    public abstract void Action(EnemyManager[] enemys);
    public abstract void Action(CardData[] cards);
    public abstract void Action(CardData[] cards, EnemyManager[] enemys);
}

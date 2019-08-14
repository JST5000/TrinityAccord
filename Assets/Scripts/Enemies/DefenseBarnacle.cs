using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBarnacle : EnemyData
{
    bool firstAttackMode = true;

    private static int BaseDamage { get; set; } = 1;
    private static int MaxHPIncrease = 3;
    private static int Heal = 8;
    private static string Closed = "Defense_Barnacle_Closed";
    private static string Open = "Defense_Barnacle_Opened";

    public DefenseBarnacle()
        : base(name: "Barnacle", maxHP: 6, staggers: 2, damage: BaseDamage, timer: 2, effect: GetEffect(true), spriteName: Closed, "DefenseBarnacle")
    { }

    protected override void AttackUniqueEffect()
    {
        List<EnemyManager> whales = EncounterManager.GetEnemyManagersWithName(new Whale().EnemyName);
        foreach (EnemyManager whale in whales)
        {
            EnemyData whaleData = whale.GetEnemyData();
            if (firstAttackMode)
            {
                whaleData.MaxHP += MaxHPIncrease;
            }
            else
            {
                whaleData.CurrHP = Mathf.Min(whaleData.MaxHP, whaleData.CurrHP + Heal);
            }
            whale.UpdateUIData();
        }

        Flip();
        UpdateUIData();
        DisplayAttackSprite(Open, Closed);
    }

    private void Flip()
    {
        firstAttackMode = !firstAttackMode;
        Effect = GetEffect(firstAttackMode);
    }

    private static string GetEffect(bool firstAttackMode)
    {
        if (firstAttackMode)
        {
            return InLineIcon.DAMAGE + $": {BaseDamage},\nBuff Whale hp by {MaxHPIncrease},\nFlip";
        }
        else
        {
            return InLineIcon.DAMAGE + $": {BaseDamage},\nHeal Whale for {Heal}, Flip";
        }
    }
}
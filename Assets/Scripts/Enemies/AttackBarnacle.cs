using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBarnacle : EnemyData
{
    bool firstAttackMode = true;

    private static int BaseDamage { get; set; } = 1;
    private static readonly int whaleDamageBuff = 3;
    private static readonly int whaleAccelerateRate = 1;

    private static readonly string Closed = "Attack_Barnacle_Closed";
    private static readonly string Open = "Attack_Barnacle_Opened";

    public AttackBarnacle()
        : base(name: "Barnacle", maxHP: 5, staggers: 3, damage: BaseDamage, timer: 2, effect: GetEffect(true), spriteName: Closed, "AttackBarnacle")
    { }

    protected override void AttackUniqueEffect()
    {
        List<EnemyManager> whales = EncounterManager.GetEnemyManagersWithName(new Whale().EnemyName);
        foreach (EnemyManager whale in whales)
        {
            if (firstAttackMode)
            {
                whale.GetEnemyData().Damage += whaleDamageBuff;
            } else
            {
                whale.GetEnemyData().MaxTimer--;
                if (whale.GetEnemyData().CurrTimer > 1)
                {
                    whale.GetEnemyData().CurrTimer--;
                }                
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
            return InLineIcon.DAMAGE + $": {BaseDamage},\nBuff Whale damage by {whaleDamageBuff},\nFlip";
        }
        else
        {
            return InLineIcon.DAMAGE + $": {BaseDamage},\nAccelerate Whale by {whaleAccelerateRate} turn, Flip";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBarnacle : EnemyData
{
    bool firstAttackMode = true;

    private static int BaseDamage { get; set; } = 1;
    private static int WhaleDamageBuff = 3;
    private static int WhaleAccelerateRate = 1;

    private static string Closed = "Attack_Barnacle_Closed";
    private static string Open = "Attack_Barnacle_Opened";

    public AttackBarnacle()
        : base(name: "Barnacle", maxHP: 6, staggers: 2, damage: BaseDamage, timer: 2, effect: GetEffect(true), spriteName: Closed, "AttackBarnacle")
    { }

    protected override void AttackUniqueEffect()
    {
        List<EnemyManager> whales = EncounterManager.GetEnemyManagersWithName(new Whale().EnemyName);
        foreach (EnemyManager whale in whales)
        {
            if (firstAttackMode)
            {

                whale.GetEnemyData().Damage += 2;
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
            return InLineIcon.DAMAGE + $": {BaseDamage},\nBuff Whale damage by {WhaleDamageBuff},\nFlip";
        }
        else
        {
            return InLineIcon.DAMAGE + $": {BaseDamage},\nAccelerate Whale by {WhaleAccelerateRate} turn, Flip";
        }
    }
}

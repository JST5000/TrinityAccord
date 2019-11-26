using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangLeader : EnemyData
{
    bool summonMode = true;

    public GangLeader()
        : base(name: "Capone", maxHP: 8, lives: 3, damage: 0, timer: 2, effect: GetEffect(true), spriteName: "Gang Leader", "Gang Leader")
    { }

    override
    protected void AttackUniqueEffect()
    {
        if (summonMode)
        {
            for (int i = 0; i < 2; ++i)
            {
                EncounterManager.SpawnEnemyInDefaultManager(new GangWimp());
            }
        }
        Flip();
    }

    private void Flip()
    {
        summonMode = !summonMode;
        if(summonMode)
        {
            Damage = 0;
            Effect = GetEffect(true);
        } else
        {
            Damage = 3;
            Effect = GetEffect(false);
        }
    }

    private static string GetEffect(bool summonText)
    {
        if(summonText)
        {
            return "Summon 2 Gang Wimps, Flip";
        } else
        {
            return InLineIcon.DAMAGE + ": 3, Flip";
        }
    }

    
}

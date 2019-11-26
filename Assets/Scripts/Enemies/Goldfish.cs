using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Treasure chest event enemy :)
/// </summary>
public class Goldfish : EnemyData
{
    private bool GetMoneyOnDeath = true;

    public Goldfish() : base(name: "Goldfish", maxHP: 7, lives: 1, damage: 0, timer: 2, effect: "Run away! " + InLineIcon.ON_DISARM + ": Give 3 coins!", spriteName: "Goldfish")
    { }

    protected override void OnLossOfLife()
    {
        if (GetMoneyOnDeath)
        {
            EncounterManager.QueuedIncome += 3;
            SoundManager.PlayEnemySFX("GoldfishDeath");
        }
    }

    protected override void AttackUniqueEffect()
    {
        GetMoneyOnDeath = false;
    }

    /// <summary>
    /// Kills itself when it attacks
    /// </summary>
    /// <returns></returns>
    override public bool SelfHarm()
    {
        return DealDamage(MaxHP * Lives);
    }
}

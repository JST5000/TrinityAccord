using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : EnemyData {

    private static int baseDamage = 5;

    public Whale() : base(name: "Whale", maxHP: 10, staggers: 3, damage: baseDamage, timer: 3, effect: GetEffect(baseDamage), "Whale")
    {
        ImmuneToDebuffs = true;
    }

    private static string GetEffect(int dmg)
    {
        return InLineIcon.DAMAGE + ": " + dmg + ", Immune to debuffs";
    }

    public override void UpdateUIData()
    {
        base.UpdateUIData();
        Effect = GetEffect(Damage);
    }

}

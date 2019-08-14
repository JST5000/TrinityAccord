using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : EnemyData {

    private static int baseDamage = 3;

    public Whale() : base(name: "Whale", maxHP: 9, staggers: 3, damage: baseDamage, timer: 3, effect: GetEffect(baseDamage), "Whale")
    { }

    private static string GetEffect(int dmg)
    {
        return InLineIcon.DAMAGE + ": " + dmg;
    }

    public override void UpdateUIData()
    {
        base.UpdateUIData();
        Effect = GetEffect(Damage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : EnemyData
{
    public Rhino() : base(name: "Rhino", maxHP: 8, staggers: 2, damage: 5, timer: 3, effect: GetRhinoEffect(false), spriteName: "Charging Rhino")
    { }

    override
    protected void AttackUniqueEffect()
    {
    }

    protected override void OnLastLife()
    {
        //Reduce damage
        Damage = 2;
        Effect = GetRhinoEffect(true);
    }

    private static string GetRhinoEffect(bool lastLife)
    {
        if (lastLife)
        {
            return "Deal 2 damage.";
        } else {
            return "Deal 5 damage.Last Stand: Deal 2 damage instead of 5.";
        }
    }
}

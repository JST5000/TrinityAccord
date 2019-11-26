using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : EnemyData
{
    public Rhino() : base(name: "Rhino", maxHP: 8, lives: 2, damage: 5, timer: 3, effect: GetRhinoEffect(false), spriteName: "Charging Rhino", alternateNames: "Charging Rhino")
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
            return InLineIcon.DAMAGE + ": 2";
        } else {
            return InLineIcon.DAMAGE + ": 5, " + InLineIcon.ON_DISARM + ": " + InLineIcon.DAMAGE + ": 2";
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : EnemyData
{
    public Tiger() : base(name: "Tiger", maxHP: 7, staggers: 2, damage: 3, timer: 2, effect: GetEffect(false), spriteName: "Tiger", alternateNames: "Cowardly Tiger")
    { }

    protected override void OnLastLife()
    {
        //Slow down attack
        MaxTimer = 3;
        Effect = GetEffect(true);
    }

    private static string GetEffect(bool lastLife)
    {
        string front = InLineIcon.DAMAGE + ": 3";
        if (lastLife)
        {
            return front;
        } else
        {
            return front + ", " + InLineIcon.ON_STAGGER + ": Slow timer by 1.";
        }
    }

}

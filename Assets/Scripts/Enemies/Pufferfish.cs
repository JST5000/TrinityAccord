using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pufferfish : EnemyData
{
    private static int MaxHealthIncrease = 2;

    private static int BaseDamage = 2;

    private static string PufferSmall = "Pufferfish_Small";
    private static string PufferExpanded = "Pufferfish_Puffed2";

    public Pufferfish() : base(name: "Puffer", maxHP: 6, staggers: 2, damage: 2, timer: 2, 
        effect: InLineIcon.DAMAGE + $": {BaseDamage}, Gain {MaxHealthIncrease} health.", spriteName: PufferSmall, "Pufferfish")
    { }

    protected override void AttackUniqueEffect()
    {
        MaxHP += MaxHealthIncrease;
        CurrHP += MaxHealthIncrease;
        UpdateUIData();
        DisplayAttackSprite(PufferExpanded, PufferSmall);
    }

}

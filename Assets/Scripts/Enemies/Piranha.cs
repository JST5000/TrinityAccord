using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piranha : EnemyData
{
    private static string piranhaName = "Piranha";

    public Piranha() : base(name: piranhaName, maxHP: 4, lives: 2, damage: GetPiranhaDamage(), timer: 2, effect: GetPiranhaEffect(), spriteName: "Piranha")
    { }

    private static int GetPiranhaDamage()
    {
        int? output = EncounterManager.GetEnemyManagersWithName(piranhaName)?.Count;
        return output != null ? output.Value : 1;
    }

    private static string GetPiranhaEffect()
    {
        return InLineIcon.DAMAGE + ": " + GetPiranhaDamage() + " (Damage equals the number of Piranhas)";
    }

    public override void UpdateUIData()
    {
        base.UpdateUIData();
        Effect = GetPiranhaEffect();
        Damage = GetPiranhaDamage();
    }

    protected override void OnLossOfLife()
    {
        base.OnLossOfLife();
        EncounterManager.UpdateAllEnemyUI();
    }



}

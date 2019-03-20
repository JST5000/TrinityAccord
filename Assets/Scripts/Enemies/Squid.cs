using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squid : EnemyData
{
    public Squid() : base(name: "Squid", maxHP: 4, staggers: 3, damage: 1, timer: 3, effect: InLineIcon.DAMAGE + ": 1, Blind the player for 1 turn!", "Squid")
    { }

    protected override void AttackUniqueEffect()
    {
        base.AttackUniqueEffect();
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.Blind(1);
    }

    public override EnemyData Copy()
    {
        return new Squid();
    }
}

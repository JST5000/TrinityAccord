using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : EnemyData
{
    public Egg() : base(name: "Egg", maxHP: 3, lives: 1, damage: 0, timer: 2, effect:"Deal 1 damage to itself!\n" + InLineIcon.ON_DISARM + ": Become an Fledgling!", "Owl_Egg")
    { }

    override public bool SelfHarm()
    {
        return DealDamage(1);
    }

    public override EnemyData TransformIntoOnDeath()
    {
        return new Fledgling();
    }
}

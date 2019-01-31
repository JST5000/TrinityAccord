using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData
{
    public string enemyName;
    public int HP;
    public int staggers;
    public int damage;
    public int timer;
    public string effect;

    public EnemyData(string name, int HP, int staggers, int damage, int timer, string effect)
    {
        this.enemyName = name;
        this.HP = HP;
        this.staggers = staggers;
        this.damage = damage;
        this.timer = timer;
        this.effect = effect;
    }

}

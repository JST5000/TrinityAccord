using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnemyData
{
    public string enemyName;
    public int HP;
    public int staggers;
    public int damage;
    public int timer;
    public string effect;

    public UIEnemyData(string name, int HP, int staggers, int damage, int timer, string effect)
    {
        this.enemyName = name;
        this.HP = HP;
        this.staggers = staggers;
        this.damage = damage;
        this.timer = timer;
        this.effect = effect;
    }
}

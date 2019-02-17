using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData
{
    private string enemyName;
    private int maxHP;
    private int currHP;
    private int staggers;
    private int damage;
    private int maxTimer;
    private int currTimer;
    private string effect;

    public string EnemyName { get => enemyName; set => enemyName = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int CurrHP { get => currHP; set => currHP = value; }
    public int Staggers { get => staggers; set => SetStaggers(value); }
    public int Damage { get => damage; set => damage = value; }
    public int MaxTimer { get => maxTimer; set => maxTimer = value; }
    public int CurrTimer { get => currTimer; set => currTimer = value; }
    public string Effect { get => effect; set => effect = value; }

    public EnemyData(string name, int maxHP, int staggers, int damage, int timer, string effect)
    {
        this.enemyName = name;
        this.maxHP = maxHP;
        this.currHP = maxHP;
        this.staggers = staggers;
        this.damage = damage;
        this.maxTimer = timer;
        currTimer = maxTimer;
        this.effect = effect;
    }

    public UIEnemyData GetUIData()
    {
        return new UIEnemyData(enemyName, currHP: currHP, maxHP: maxHP, staggers, damage, maxTimer, currTimer, effect);
    }

    public void Attack() {
        //Default damage effect
        GameObject.Find("Player").GetComponent<Player>().Damage(damage);
        AttackUniqueEffect();
    }

    //Override for individual enemies if the effect needs to be triggered
    protected virtual void AttackUniqueEffect() { }

    public void SetStaggers(int value)
    {
        if (value < staggers && value == 1)
        {
            //For last stand effects like in Rhino and Boar
            OnLastLife();
        }
        staggers = value;
    }
    protected virtual void OnLastLife() { }

   

}

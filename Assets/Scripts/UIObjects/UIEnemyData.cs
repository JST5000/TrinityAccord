using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnemyData
{
    private string enemyName;
    private int currHP;
    private int maxHP;
    private int lives;
    private int damage;
    private int maxTimer;
    private int currTimer;
    private string effect;
    private Sprite picture;
    private bool stunned;
    private int sleepTimer;

    public string EnemyName { get => enemyName;}
    public int CurrHP { get => currHP;}
    public int MaxHP { get => maxHP;}
    public int Lives { get => lives;}
    public int Damage { get => damage;}
    public int MaxTimer { get => maxTimer;}
    public int CurrTimer { get => currTimer;}
    public string Effect { get => effect;}
    public Sprite Picture { get => picture; }
    public bool Stunned { get => stunned; }
    public int SleepTimer { get => sleepTimer; }

    public UIEnemyData(string name, int currHP, int maxHP, int lives, int damage, int maxTimer, int currTimer, string effect, Sprite picture, bool stunned, int sleepTimer)
    {
        this.enemyName = name;
        this.currHP = currHP;
        this.maxHP = maxHP;
        this.lives = lives;
        this.damage = damage;
        this.maxTimer = maxTimer;
        this.currTimer = currTimer;
        this.effect = effect;
        this.picture = picture;
        this.stunned = stunned;
        this.sleepTimer = sleepTimer;
    }
}

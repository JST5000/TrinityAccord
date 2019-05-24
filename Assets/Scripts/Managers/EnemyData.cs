using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyData
{
    private string enemyName;
    private int maxHP;
    private int currHP;
    private int staggers;
    private int damage;
    private int maxTimer;
    private int currTimer;
    private string effect;
    private Sprite picture;
    private string spriteName;
    private string[] alternateNames;
    private bool stunned;
    private int sleepTimer;

    public string EnemyName { get => enemyName; set => enemyName = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int CurrHP { get => currHP; set => currHP = value; }
    public int Staggers { get => staggers; set => SetStaggers(value); }
    protected int Damage { get => damage; set => damage = value; }
    public int MaxTimer { get => maxTimer; set => maxTimer = value; }
    public int CurrTimer { get => currTimer; set => currTimer = value; }
    public string Effect { get => effect; set => effect = value; }
    public Sprite Picture { get => picture; set => picture = value; }
    public string SpriteName { get => spriteName; set => spriteName = value; }
    public string[] AlternateNames { get => alternateNames; set => alternateNames = value; }
    public bool Stunned { get => stunned; set => stunned = value; }
    public int SleepTimer { get => sleepTimer; set => sleepTimer = value; }

    public static int MaxSleepTimer = 3;

    public EnemyData(string name, int maxHP, int staggers, int damage, int timer, string effect, string spriteName, params string[] alternateNames)
    {
        this.enemyName = name;
        this.maxHP = maxHP;
        this.currHP = maxHP;
        this.staggers = staggers;
        this.damage = damage;
        this.maxTimer = timer;
        currTimer = maxTimer;
        this.effect = effect;
        this.spriteName = spriteName;
        LoadPicture(spriteName);
        this.alternateNames = alternateNames;
        this.sleepTimer = 0;
    }

    //Creates a new EnemyData of the same type (Does NOT copy fields)
    public EnemyData Clone()
    {
        Type type = this.GetType();
        EnemyData copy = (EnemyData)Activator.CreateInstance(type);
        return copy;
    }

    public UIEnemyData GetUIData()
    {
        return new UIEnemyData(enemyName, currHP: currHP, maxHP: maxHP, staggers, damage, maxTimer, currTimer, effect, picture, stunned: stunned, sleepTimer: sleepTimer);
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
        if (value < staggers)
        {
            OnLossOfLife();
            if (value == 1)
            {
                //For last stand effects like in Rhino and Boar
                OnLastLife();
            }
        }
        staggers = value;
    }

    public virtual int GetModifiedDamageOnEachHit(int damage)
    {
        return damage; //By default no change
    }

    //Wrapper for body to allow for damage reduction effects
    //Returns true if the enemy has died
    public bool DealDamage(int damage)
    {
        int modifiedDamage = GetModifiedDamageOnEachHit(damage);
        return DamageRecursive(modifiedDamage);
    }

    //Returns true 
    public bool DamageRecursive(int damage)
    {
        if(damage == 0)
        {
            return false;
        }
        if (sleepTimer > 0)
        {
            sleepTimer = 0;
        }
        int currDamage = damage;
        while (CurrHP > 0 && currDamage > 0)
        {
            currDamage--;
            CurrHP--;
        }
        if (CurrHP == 0)
        {
            //Must drop staggers first for onStagger effects to activate (Ex. Tiger -> Timer increase)
            Staggers--;

            StaggerEnemy();
            CurrHP = MaxHP;
            if (Staggers > 0)
            {
                return DamageRecursive(currDamage);
            }
            else
            {
                Debug.Log(EnemyName + " was killed.");
                return true;
            }
        }
        //Did not die
        return false;
        
    }

    public virtual void StaggerEnemy()
    {
        CurrTimer = MaxTimer;
        Stun();
    }

    public virtual void Stun()
    {
        Stunned = true;
    }

    public virtual void Drowsy()
    {
        sleepTimer = MaxSleepTimer;
    }

    protected void LoadPicture(string givenSpriteName)
    {
        string folderName = "Enemy_Sprites/";
        this.picture = Resources.Load<Sprite>(folderName + givenSpriteName);
    }

    public virtual bool SelfHarm() { return false; }
    protected virtual void OnLossOfLife() { }
    protected virtual void OnLastLife() { }

   

}

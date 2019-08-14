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
    public int Damage { get => damage; set => damage = value; }
    public int MaxTimer { get => maxTimer; set => maxTimer = value; }
    public virtual int CurrTimer { get => currTimer; set => currTimer = value; }
    public string Effect { get => effect; set => effect = value; }
    public Sprite Picture { get => picture; set => picture = value; }
    public string SpriteName { get => spriteName; set => spriteName = value; }
    public string[] AlternateNames { get => alternateNames; set => alternateNames = value; }
    public bool Stunned { get => stunned; set => stunned = value; }
    public int SleepTimer { get => sleepTimer; set => sleepTimer = value; }
    public bool Vulnerable { get; set; }
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
        Vulnerable = false;
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

    //Used by children to add triggered updates to effects and such
    public virtual void UpdateUIData() { }

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
        int recievedDamage = damage;

        if (Vulnerable)
        {
            recievedDamage *= 2;
        }

        int modifiedDamage = GetModifiedDamageOnEachHit(recievedDamage);
        return DamageRecursive(modifiedDamage);
    }

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
        if(sleepTimer == 0)
        {
            sleepTimer = MaxSleepTimer; //Drowsy
        } else
        {
            sleepTimer = MaxSleepTimer - 1; //Still asleep
        }
    }

    public void WakeUp()
    {
        sleepTimer = 0;
    }

    string enemySpriteFolder = "Enemy_Sprites/";

    protected void LoadPicture(string spriteName)
    {
        this.SpriteName = spriteName;
        this.picture = Resources.Load<Sprite>(enemySpriteFolder + spriteName);
    }

    protected void LoadPicture(string spriteName, Sprite preloadedSprite)
    {
        this.SpriteName = spriteName;
        this.picture = preloadedSprite;
    }

    public virtual bool SelfHarm() { return false; }
    protected virtual void OnLossOfLife() { }
    protected virtual void OnLastLife() { }

    protected void DisplayAttackSprite(string attackSpriteName, string originalSpriteName)
    {
        Sprite original = Resources.Load<Sprite>(enemySpriteFolder + originalSpriteName);

        DisplayAttackSpriteCommon(original, originalSpriteName, attackSpriteName);
    }

    protected void DisplayAttackSprite(string attackSpriteName)
    {
        string originalSpriteName = this.SpriteName;
        Sprite original = this.picture;

        DisplayAttackSpriteCommon(original, originalSpriteName, attackSpriteName);
    }

    private void DisplayAttackSpriteCommon(Sprite original, string originalSpriteName, string attackSpriteName)
    {
        Sprite attack = Resources.Load<Sprite>(enemySpriteFolder + attackSpriteName);

        GameObject.Find("MonoBehaviorUtil").GetComponent<ExternalMonoBehavior>().
            UseStartCoroutine(TriggerStillFrameAttackAnimation(attack, attackSpriteName, original, originalSpriteName));
    }

    private IEnumerator TriggerStillFrameAttackAnimation(Sprite attack, string attackSpriteName, Sprite original, string originalSpriteName)
    {
        float durationOfChange = .3f;
        LoadPicture(attackSpriteName, attack);
        yield return new WaitForSeconds(durationOfChange);
        LoadPicture(originalSpriteName, original);
        yield return null;
    }


}

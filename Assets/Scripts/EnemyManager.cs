using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    string enemyName;
    int maxHP;
    int maxStagger;
    int maxTimer;
    int currentHP;
    int currentStagger;
    int currentTimer;   

    public void Init(EnemyData enemyData)
    {
        this.enemyName = enemyData.enemyName;
        this.maxHP = enemyData.HP;
        this.maxStagger = enemyData.staggers;
        this.maxTimer = enemyData.timer;
        this.currentHP = maxHP;
        this.currentStagger = maxStagger;
        this.currentTimer = maxTimer;

    }
    public void Damage(int damage)
    {
        while (currentHP > 0 && damage>0)
        {
            damage--;
            currentHP--;
        }
        if (currentHP == 0)
        {
            currentTimer = maxTimer;
            currentStagger--;
            if (currentStagger != 0)
            {
                Damage(damage);
            }
            else
            {
                Die();

            }
        }


    }
    public void EndTurn()
    {
        currentTimer--;
        if (currentTimer == 0)
        {
            currentTimer = maxTimer;
            Attack();
        }
    }
    private void Attack()
    {



    }
    private void Die()
    {

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private EnemyData data;
    private bool isEmpty = true;

    public void Init(EnemyData enemyData)
    {
        data = enemyData;
        isEmpty = false;
    }

    //Returns true if successfully dealt damage, false if it did not damage anything
    public bool Damage(int damage)
    {
        //AOEs may hit all without checking, so this prevents nulls
        if (!isEmpty)
        {
            bool isDead = data.DealDamage(damage);
            if (isDead)
            {
                Die();
            }
            return true;
        }  
        return false;
    }

    public bool Stagger()
    {
        if (!isEmpty)
        {
            data.StaggerEnemy();
            return true;
        }
        return false;
    }
    public bool Stun()
    {
        if (!isEmpty)
        {
            data.Stun();
            return true;
        }
        return false;
    }
    public void EndTurn()
    {
        if (!isEmpty)
        {
            if (data.Stunned)
            {
                data.Stunned = false;
            }
            else
            {
                data.CurrTimer--;
            }
            if (data.CurrTimer == 0)
            {
                data.CurrTimer = data.MaxTimer;
                Attack();
            }
        }
    }

    private void Attack()
    {
        data.Attack();
        //Returns true if dead
        if(data.SelfHarm())
        {
            Die();
        }
    }
    private void Die()
    {
        SetEmpty(); //Removes UI element
        EncounterManager encounterMan = GetComponentInParent<EncounterManager>();
        encounterMan.OnEnemyDeath(); //Alerts the encounterManager of death
    }

    public void SetEmpty()
    {
        isEmpty = true;
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

    public bool IsStunned()
    {
        return data.Stunned;
    }

    public UIEnemyData GetUIData()
    {
        return data.GetUIData();
    }
}


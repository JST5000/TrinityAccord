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
            
            bool skipTimerDecrease = HandleCrowdControl(data);
            if(!skipTimerDecrease) {
                data.CurrTimer--;
            }
            if (data.CurrTimer == 0)
            {
                data.CurrTimer = data.MaxTimer;
                Attack();
            }
        }
    }

    //Returns true if the timer is stopped this turn
    private bool HandleCrowdControl(EnemyData data)
    {
        bool skipTimerDecrease = false;
        if (data.Stunned)
        {
            data.Stunned = false;
            skipTimerDecrease = true;
        }
        if (data.SleepTimer > 0)
        {
            if (data.SleepTimer < EnemyData.MaxSleepTimer)
            {
                skipTimerDecrease = true;
            }
            data.SleepTimer--;
        }
        return skipTimerDecrease;
    }

    private void Attack()
    {
        GetComponent<UpdateEnemyUI>().AttackEnemy();
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

    public bool IsAlive()
    {
        return !IsEmpty() && data.CurrHP > 0;
    }

    public void Drowsy()
    {
        data.Drowsy();
    }

    public void WakeUp()
    {
        data.WakeUp();
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

    public void UpdateUIData()
    {
        data.UpdateUIData();
    }

    public void SetTargetIndicator(bool targetStatus)
    {
        //Never want to show the target when there is no enemy!
        if (isEmpty)
        {
            GetComponent<UpdateEnemyUI>().SetTarget(false);
        }
        else
        {
            GetComponent<UpdateEnemyUI>().SetTarget(targetStatus);
        }
    }


    /// <summary>
    /// Should only be used when there is a direct field that needs to be modified. Ex. Whale + Barnacle interaction
    /// </summary>
    /// <returns></returns>
    public EnemyData GetEnemyData()
    {
        return data;
    }
}


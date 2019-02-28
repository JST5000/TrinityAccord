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
    public bool Damage(int damage)
    {
        //AOEs may hit all without checking, so this prevents nulls
        if (!isEmpty)
        {
            int currDamage = damage;
            while (data.CurrHP > 0 && currDamage > 0)
            {
                currDamage--;
                data.CurrHP--;
            }
            if (data.CurrHP == 0)
            {
                data.CurrTimer = data.MaxTimer + 1;
                data.CurrHP = data.MaxHP;
                data.Staggers--;
                if (data.Staggers != 0)
                {
                    Damage(currDamage);
                }
                else
                {
                    Debug.Log(data.EnemyName + " was killed.");
                    Die();
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public void EndTurn()
    {
        if (!isEmpty)
        {
            data.CurrTimer--;
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

    public UIEnemyData GetUIData()
    {
        return data.GetUIData();
    }

    float currTime = 0;
    private void Update()
    {
        /*    //Testing, remove with real implementation
            currTime += Time.deltaTime;
            if(currTime >= 1f)
            {
                currTime = 0;
                Damage(1);
            }
        } */
    }
}


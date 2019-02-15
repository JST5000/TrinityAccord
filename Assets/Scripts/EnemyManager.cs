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
    public void Damage(int damage)
    {
        while (data.CurrHP > 0 && damage>0)
        {
            damage--;
            data.CurrHP--;
        }
        if (data.CurrHP == 0)
        {
            data.CurrTimer = data.MaxTimer;
            data.Staggers--;
            if (data.Staggers != 0)
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
        data.CurrTimer--;
        if (data.CurrTimer == 0)
        {
            data.CurrTimer = data.MaxTimer;
            Attack();
        }
    }

    private void Attack()
    {



    }
    private void Die()
    {

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
}


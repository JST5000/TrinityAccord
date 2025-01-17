﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpdateEnemyUI : MonoBehaviour
{
    public TextMeshProUGUI nameDisplay;
    public HealthManager hpManager;
    public LifeManager livesHolder;
    public Image enemyPicture;
    public Image timerBackground;
    public TextMeshProUGUI timerDisplay;
    public TextMeshProUGUI effectDisplay;
    public CanvasGroup effectCanvasGroup;
    public TextMeshProUGUI debuffText;

    public TextMeshProUGUI attackQuickReferenceText;

    public CanvasGroup TargetArrowCG;
    private bool targetVisibility;

    private EnemyManager enemyHolder;

    private bool isDisabled = false;
    public CanvasGroup enemyCG;

    private Transform HealthBar;

    // Start is called before the first frame update
    void Start()
    {
        enemyHolder = GetComponent<EnemyManager>();
        DisableEnemy();

        targetVisibility = (TargetArrowCG.alpha > .1f);

        HealthBar = GameObject.Find("Player Health Bar")?.transform;
    }

    public void PopulateUI(UIEnemyData data)
    {
        nameDisplay.text = data.EnemyName;
        hpManager.SetMaxHealth(data.MaxHP);
        hpManager.SetCurrHealth(data.CurrHP);
        livesHolder.SetLives(data.Lives);
        timerDisplay.text = data.CurrTimer + "/" + data.MaxTimer;
        effectDisplay.text = data.Effect;
        //TODO populate the image field
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyHolder.IsEmpty())
        {
            UpdateUI(enemyHolder.GetUIData());
            EnableEnemy();
        }
        else
        {
            DisableEnemy();
        }
    }

    public void DisplayEffect(bool visible)
    {
        CanvasGroupManip.SetVisibility(visible, effectCanvasGroup, false);
    }

    //Checks if change is needed, then updates
    public void DisableEnemy()
    {
        if (!isDisabled)
        {
            enemyCG.alpha = 0;
            enemyCG.blocksRaycasts = false;
            enemyCG.interactable = false;
            isDisabled = true;
        }
    }

    //Checks if change is needed, then updates
    public void EnableEnemy()
    {
        if (isDisabled)
        {
            enemyCG.alpha = 1;
            enemyCG.blocksRaycasts = true;
            enemyCG.interactable = true;
            isDisabled = false;
        }
    }

    public void AttackEnemy()
    {
        GetComponent<MoveToBump>().StartBump(transform, HealthBar, .5f, true);
    }

    public void UpdateUI(UIEnemyData data)
    {
        nameDisplay.text = data.EnemyName;

        hpManager.SetCurrHealth(data.CurrHP);
        hpManager.SetMaxHealth(data.MaxHP);

        livesHolder.SetLives(data.Lives);

        enemyPicture.sprite = data.Picture;
        RectTransform rt = enemyPicture.GetComponent<RectTransform>();
        if (data.WideSprite)
        {
            rt.sizeDelta = new Vector2(150, 100);
        } else {
            rt.sizeDelta = new Vector2(100, 100);
        }

        //SetTimerColor(data);
        string timerSpriteName = "Timers/Timer" + data.MaxTimer + "-";
        if (GetComponent<MoveToBump>().bumping)
        {
            timerSpriteName += data.MaxTimer + 1;
        } else
        {
            timerSpriteName += (data.MaxTimer - data.CurrTimer + 1);
        }
        timerBackground.sprite = Resources.Load<Sprite>(timerSpriteName);
        timerDisplay.text = data.CurrTimer + " / " + data.MaxTimer;

        SetDebuffText(data);

        effectDisplay.text = data.Effect;
        attackQuickReferenceText.text = GetAttack(data.Effect) + InLineIcon.DAMAGE;
    }

    private string GetAttack(string effect)
    {
        int attack = 0;
        string precursor = InLineIcon.DAMAGE;
        if(effect.Contains(precursor))
        {
            char[] delimiters = { ' ', '\n', ':', ',' };
            string[] tokens = effect.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tokens.Length; ++i)
            {
                if (tokens[i].Equals(precursor))
                {
                    int.TryParse(tokens[i + 1], out attack);
                    break;
                }
            }
        }
        return ""+attack;
    }

    public void SetTimerColor(UIEnemyData data)
    {
        if(data.CurrTimer == 1)
        {
            timerBackground.color = Color.red;
        } else if (data.CurrTimer == 2 || data.CurrTimer < data.MaxTimer )
        {
            timerBackground.color = Color.yellow;
        } else if (data.CurrTimer >= data.MaxTimer)
        {
            timerBackground.color = Color.green;
        }

        if(data.Stunned || (data.SleepTimer > 0 && data.SleepTimer < EnemyData.MaxSleepTimer))
        {
            Color c = timerBackground.color;
            float dim = .6f;
            timerBackground.color = new Color(c.r * dim, c.g * dim, c.b * dim, c.a);
        }
    }

    public void SetDebuffText(UIEnemyData data)
    {
        if (data.Stunned)
        {
            debuffText.text = "Stunned!";
        }
        else if (data.SleepTimer == EnemyData.MaxSleepTimer)
        {
            debuffText.text = "Drowsy...";
        }
        else if (data.SleepTimer > 1 && data.SleepTimer < EnemyData.MaxSleepTimer)
        {
            debuffText.text = "Asleep...";
        }
        else if(data.SleepTimer == 1) {
            debuffText.text = "Waking...";
        }
        else if(data.Vulnerable)
        {
            debuffText.text = "Vulnerable";
        } 
        else
        {
            debuffText.text = "";
        }
    }

    public void SetTarget(bool target)
    {
        if(this.targetVisibility != target)
        {
            targetVisibility = target;
            CanvasGroupManip.SetVisibility(target, this.TargetArrowCG);
        }
    }
}

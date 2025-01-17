﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public EnergyManager energyUI;
    public HealthManager healthUI;
    public TextMeshProUGUI statusMessage;

    public int defaultEnergy = 3;

    public bool CanDie = false;
    public bool resetHealthBetweenEncounters = false;

    private int blindDuration = 0;
    private bool blindWasSet = false;

    private int currEnergy;
    private int maxHealth;
    private int currHealth;
    private int bonusEnergy;

    public static Player Get()
    {
        return GameObject.Find("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetEnergy();
        if (resetHealthBetweenEncounters)
        {
            InitHealth(PermanentState.MaxHealth, PermanentState.MaxHealth);
        }
        else
        {
            InitHealth(PermanentState.MaxHealth, PermanentState.Health);
        }
        //Start invisible
        CanvasGroupManip.Disable(statusMessage.GetComponent<CanvasGroup>());
    }

    public void InitHealth(int max, int curr)
    {
        SetMaxHealth(max);
        SetCurrentHealth(curr);
    }

    public void EndTurn()
    {
        ResetEnergy();
        if (blindWasSet && blindDuration > 0)
        {
            blindDuration--;
            if(blindDuration == 0)
            {
                CanvasGroupManip.Disable(statusMessage.GetComponent<CanvasGroup>());
            } 
        }
        
    }

    public void ResetEnergy()
    {
        currEnergy = defaultEnergy+bonusEnergy;
        bonusEnergy = 0;
        UpdateEnergyUI();
        HandManager.Get().UpdateAllCardsInHand();
    }
    public void addBonusEnergy(int amount)
    {
        bonusEnergy += amount;
    }
    public int GetEnergy()
    {
        return currEnergy;
    }

    public void PayEnergy(int cost)
    {
        currEnergy -= cost;
        UpdateEnergyUI();
        HandManager.Get().UpdateAllCardsInHand();
    }
    public void AddEnergy(int amount)
    {
        currEnergy += amount;
        UpdateEnergyUI();
        HandManager.Get().UpdateAllCardsInHand();
    }

    public void Damage(int dmg)
    {
        SetCurrentHealth(currHealth - dmg);
        SoundManager.PlayEnemySFX("Belt_Smack");
        UpdateHealthUI();
        if (currHealth <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        if(CanDie)
        {
            PermanentState.hasDraftedClassCard = false;
            PermanentState.PushEncounterData();
            SceneManager.LoadScene("LoseScreen");
        }
    }

    public void Heal(int heal)
    {
        //Cannot exceed max health
        SetCurrentHealth(Mathf.Min(heal + currHealth, maxHealth));
        UpdateHealthUI();
    }

    public void SetCurrentHealth(int curr)
    {
        currHealth = curr;
        PermanentState.Health = curr;
        UpdateHealthUI();
    }

    public int GetCurrentHealth()
    {
        return currHealth;
    }

    public void SetMaxHealth(int max)
    {
        maxHealth = max;
        PermanentState.MaxHealth = max;
        UpdateHealthUI();
    }

    public void Blind(int duration)
    {
        Debug.Log("Blinded!");
        blindWasSet = true;
        blindDuration = Mathf.Max(blindDuration, duration);
        statusMessage.text = "BLINDED!";
        CanvasGroupManip.Enable(statusMessage.GetComponent<CanvasGroup>());
    }

    public bool IsBlind()
    {
        return blindDuration > 0;
    }

    private void UpdateHealthUI()
    {
        healthUI.SetCurrHealth(currHealth);
        healthUI.SetMaxHealth(maxHealth);
    }

    private void UpdateEnergyUI()
    {
        energyUI.SetEnergy(currEnergy);
    }
}

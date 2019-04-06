using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public EnergyManager energyUI;
    public HealthManager healthUI;

    public int defaultEnergy = 3;
    public int defaultHealth = 10;

    public bool CanDie = false;

    private int blindDuration = 0;

    private int currEnergy;
    private int maxHealth;
    private int currHealth;
    private int bonusEnergy;


    // Start is called before the first frame update
    void Start()
    {
        ResetEnergy();
        SetMaxHealth(defaultHealth);
        SetCurrentHealth(defaultHealth);
    }

    public void EndTurn()
    {
        ResetEnergy();
        if (blindDuration > 0)
        {
            blindDuration--;
        }
    }

    public void ResetEnergy()
    {
        currEnergy = defaultEnergy+bonusEnergy;
        bonusEnergy = 0;
        UpdateEnergyUI();
        GameObject.Find("Hand").GetComponent<HandManager>().UpdateAllCardsInHand();
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
        GameObject.Find("Hand").GetComponent<HandManager>().UpdateAllCardsInHand();
    }
    public void AddEnergy(int amount)
    {
        currEnergy += amount;
        UpdateEnergyUI();
        GameObject.Find("Hand").GetComponent<HandManager>().UpdateAllCardsInHand();
    }

    public void Damage(int dmg)
    {
        currHealth -= dmg;
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
            SceneManager.LoadScene("LoseScreen");
        }
    }

    public void Heal(int heal)
    {
        //Cannot exceed max health
        currHealth = Mathf.Min(heal + currHealth, maxHealth);
        UpdateHealthUI();
    }

    public void SetCurrentHealth(int curr)
    {
        currHealth = curr;
        UpdateHealthUI();
    }

    public void SetMaxHealth(int max)
    {
        maxHealth = max;
        UpdateHealthUI();
    }

    public void Blind(int duration)
    {
        blindDuration = Mathf.Max(blindDuration, duration);
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

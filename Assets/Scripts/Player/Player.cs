using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public EnergyManager energyUI;
    public HealthManager healthUI;

    public int defaultEnergy = 3;
    public int defaultHealth = 10;

    private int currEnergy;
    private int maxHealth;
    private int currHealth;

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
    }

    public void ResetEnergy()
    {
        currEnergy = defaultEnergy;
        UpdateEnergyUI();
    }

    public int GetEnergy()
    {
        return currEnergy;
    }

    public void PayEnergy(int cost)
    {
        currEnergy -= cost;
        UpdateEnergyUI();
    }
    public void AddEnergy(int amount)
    {
        currEnergy += amount;
        UpdateEnergyUI();
    }

    public void Damage(int dmg)
    {
        currHealth -= dmg;
        if(currHealth <= 0)
        {
            //TODO on death
        }
        UpdateHealthUI();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : ShopItem
{
    int heal;

    public HealthItem(int heal, int? cost = null) : base("Heal " + heal, GetCost(heal, cost), imageName: "Life")
    {
        this.heal = heal;
    }

    private static int GetCost(int heal, int? cost)
    {
        if (cost == null)
        {
            int healPerCost = 2;
            return heal / healPerCost;
        } else
        {
            return cost.Value;
        }
    }

    override public void  Effect()
    {
        PermanentState.health = Mathf.Min(PermanentState.health + heal, PermanentState.maxHealth);
    }

    public override bool OtherRequirementsMet()
    {
        return PermanentState.health < PermanentState.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

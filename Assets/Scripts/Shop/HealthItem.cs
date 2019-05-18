using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : ShopItem
{
    int heal;

    public HealthItem(int heal) : base("Heal " + heal, GetCost(heal), imageName: "Life")
    {
        this.heal = heal;
    }

    private static int GetCost(int heal)
    {
        int healPerCost = 2;       
        return heal / healPerCost;
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

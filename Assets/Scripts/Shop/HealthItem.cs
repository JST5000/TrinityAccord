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
        int cost = 0;
        int costPerHeal = 2;
        if (heal <= 2)
        {
            cost = costPerHeal * heal;
        } else if(heal <= 5)
        {
            cost = costPerHeal * heal - costPerHeal/2;
        } else
        {
            cost = costPerHeal * heal - costPerHeal;
        }
        return cost;
    }

    override public void  Effect()
    {
        PermanentState.health = Mathf.Max(PermanentState.health + heal, PermanentState.defaultHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

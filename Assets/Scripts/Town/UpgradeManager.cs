using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : ShopManager
{
    // Start is called before the first frame update
    void Start()
    {
        List<ShopItem> allUpgrades = new List<ShopItem>();
        allUpgrades.Add(new HealthItem(5, 0));
        allUpgrades.Add(new PackItem(0));
        allUpgrades.Add(new RemovalItem(0));
        Init(Resources.Load<Sprite>("People/James"), "Upgrade!", allUpgrades, true, "Choose One!");
    }

    // Update is called once per frame
    void Update()
    {
        //This is rather hacky. It should be that when any item is bought, the shop closes, not an active wait.
        if(HasAnItemBeenBought)
        {
            Exit();
        }
    }
}

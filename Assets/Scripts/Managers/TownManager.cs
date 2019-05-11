using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TownManager : MonoBehaviour
{
    public TextMeshProUGUI response;
    public TextMeshProUGUI moneyCounter;

    public void OpenAttackShop()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new PackItem());
        Enter("Attack Shop", "Jenny", inventory);
    }

    public void OpenQuestStand()
    {
        Enter("Quest Stand", "Dave", new List<ShopItem>());
    }

    public void OpenHealthShop()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new HealthItem(1));
        inventory.Add(new HealthItem(3));
        inventory.Add(new HealthItem(7));
        Enter("Health Shop", "James", inventory, true);
    }

    public void OpenCardRemovalStand()
    {
        Enter("Card Removal Stand", "Jenny", new List<ShopItem>());
    }

    public void LeaveTown(Text exit)
    {
        if (exit.text == "Yes")
        {
            response.text = "You have left the town.";
        }
        else
        {
            response.text = "Are you sure you want to leave?";
            exit.text = "Yes";
        }
    }

    private void Start()
    {
        int money = 0;
        if (GameObject.Find("PermanentState") != null)
        {
            money = PermanentState.money;
        }
        moneyCounter.text = "Coins: " + money;
    }

    private void Enter(string name, string shopKeeperName, List<ShopItem> items)
    {
        Enter(name, shopKeeperName, items, false);
    }

    private void Enter(string name, string shopKeeperName, List<ShopItem> items, bool showHealth)
    {
        response.text = "You entered the " + name + "!";
        GameObject instance = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/ShopUI"));
        instance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        instance.transform.position = new Vector3(0, 0, 0);
        Sprite shopKeeper = Resources.Load<Sprite>("People/" + shopKeeperName);
        instance.GetComponent<ShopManager>().Init(shopKeeper, name, items, showHealth);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TownManager : MonoBehaviour
{
    public TextMeshProUGUI response;
    public TextMeshProUGUI moneyCounter;
    public HealthManager healthDisplay;

    private void Start()
    {
        healthDisplay.SetMaxHealth(PermanentState.maxHealth);
    }

    public void OpenAttackShop()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new PackItem());
        Enter("Attack Shop", "Jenny", inventory);
    }

    public void OpenCabin()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new HealthItem(10, 3));
        Enter("Emma's Cabin", "Emma", inventory, true, "Oh you poor thing...");
    }

    public void OpenSailboat()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new TravelItem("Safe Travel", 2, "SailingIcon", PermanentState.GetTownForNWins(PermanentState.wins + 1), true));
        inventory.Add(new TravelItem("Risky Travel", 0, "SwimmingIcon", "Encounter", false));
        Enter("Harbor", "WaterBoy", inventory, true, "Pay for safe travel?");
    }

    public void OpenArtistHill()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new HealthItem(2, 1));
        inventory.Add(new PackItem());
        inventory.Add(new TravelItem("Onward", 0, "JumpingOffCliffIcon", "Encounter", false));
        Enter("Kwame's Hill", "Kwame", inventory, true, "Hahaha, welcome!");
    }

    public void OpenTent()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        //inventory.Add(new PackItem());
        inventory.Add(new HealthItem(3, 2));
        Enter("Explorer's Tent", "Explorer", inventory, true, "I can fix you... for a price.");
    }

    public void OpenQuestStand()
    {
        Enter("Quest Stand", "Dave", new List<ShopItem>());
    }

    public void OpenHealthShop()
    {
        Enter("Health Shop", "James", GetHealthShopInventory(), showHealth: true);
    }

    public void SallyShop()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new PackItem(5));
        Enter("Sarah's World", "Sarah_Kid", inventory, false, "Hi Crystal!\nI found this, do you want it?");
    }

    private List<ShopItem> GetHealthShopInventory()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new HealthItem(2));
        inventory.Add(new HealthItem(5));
        return inventory;
    }

    public void OpenHealthShopTown2()
    {
        Enter("Health Shop", "Darnell_Closeup", GetHealthShopInventory(), showHealth: true, "Sally found something!");
    }

    public void OpenCardRemovalStand()
    {
        Enter("Card Removal Stand", "Jenny", new List<ShopItem>());
    }

    public void LeaveTown(Text exit)
    {
        PermanentState.ChooseNextFight();
        SceneManager.LoadScene("Encounter");
    }



    private void Enter(string name, string shopKeeperName, List<ShopItem> items)
    {
        Enter(name, shopKeeperName, items, false);
    }

    private void Enter(string name, string shopKeeperName, List<ShopItem> items, bool showHealth, string message = "")
    {
        GameObject instance = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/ShopUI"));
        instance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        instance.transform.position = new Vector3(0, 0, 0);
        Sprite shopKeeper = Resources.Load<Sprite>("People/" + shopKeeperName);
        instance.GetComponent<ShopManager>().Init(shopKeeper, name, items, message);
    }

    private void Update()
    {
        moneyCounter.text = "Coins: " + PermanentState.money;
        healthDisplay.SetCurrHealth(PermanentState.health);
    }
}

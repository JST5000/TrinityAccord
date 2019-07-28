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

    public void OpenCampfire()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new HealthItem(5, 2));
        inventory.Add(new RemovalItem());
        Enter("Mapper's Camp", "CampfireManOnly", inventory, true, "Left, Right. Are you lost?");
    }

    public void OpenSailboat()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new RelativeTravelItem(true, 2, "SailingIcon", skipLevel: true));
        inventory.Add(new RelativeTravelItem(true, 0, "SwimmingIcon", skipLevel: false));
        inventory.Add(new RelativeTravelItem(false, 2, "SailingIcon", skipLevel: true));
        inventory.Add(new RelativeTravelItem(false, 0, "SwimmingIcon", skipLevel: false));
        Enter("Harbor", "WaterBoy", inventory, true, "Pay for safe travel?");
    }

    public void OpenArtistHill()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new HealthItem(2, 1));
        inventory.Add(new PackItem());
        inventory.Add(new RelativeTravelItem(false, 0, "JumpingOffCliffIcon", false, "Onward"));
        Enter("Kwame's Hill", "Kwame", inventory, true, "Hahaha, welcome!");
    }

    public void OpenTent()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new HealthItem(3, 2));
        Enter("Explorer's Tent", "Explorer", inventory, true, "The right is new... hmmm...");
    }

    public void OpenQuestStand()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        
        Enter("Novelty Shop", "Dave", inventory, false, "New wares!");
    }

    public void OpenHealthShop()
    {
        Enter("Health Shop", "James", GetHealthShopInventory(), showHealth: true);
    }

    public void SallyShop()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new PackItem(4));
        Enter("Sarah's World", "Sarah_Kid", inventory, false, "Hi Crystal!\nI found this, do you want it?");
    }

    public void OpenOasis()
    {
        List<ShopItem> inventory = GetHealthShopInventory();
        Enter("Oasis", "Rainman_Closeup", inventory, true, "Preparing for a rainy day?");
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
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new RemovalItem());
        Enter("Dagger Removal", "Jenny", inventory, true, "I'll destroy that dagger!");
    }

    public void OpenVantage()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new HealthItem(5, 1));
        inventory.Add(new RelativeTravelItem(true, 0, "MountainPath", false));
        Enter("Vantage Point", "Hiker", inventory, true, "Quite a sight eh?");
    }

    public void LeaveTown(RelativePathName path)
    {
        PermanentState.MoveToNextTown(path.left);
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

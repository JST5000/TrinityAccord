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

    public void OpenQuestStand()
    {
        Enter("Quest Stand", "Dave", new List<ShopItem>());
    }

    public void OpenHealthShop()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new HealthItem(2));
        inventory.Add(new HealthItem(5));
        Enter("Health Shop", "James", inventory, true);
    }

    public void OpenCardRemovalStand()
    {
        Enter("Card Removal Stand", "Jenny", new List<ShopItem>());
    }

    public void LeaveTown(Text exit)
    {
        ChooseNextFight();
        SceneManager.LoadScene("Encounter");
    }

    private void ChooseNextFight()
    {
        EnemyData[] selectedEncounter = GenerateEncounter.GetEncounter(PermanentState.expectedLevel);
        PermanentState.SetNextEncounter(selectedEncounter);
        PermanentState.expectedLevel = GenerateEncounter.GetHarder(PermanentState.expectedLevel);
    }

    private void Enter(string name, string shopKeeperName, List<ShopItem> items)
    {
        Enter(name, shopKeeperName, items, false);
    }

    private void Enter(string name, string shopKeeperName, List<ShopItem> items, bool showHealth)
    {
        GameObject instance = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/ShopUI"));
        instance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        instance.transform.position = new Vector3(0, 0, 0);
        Sprite shopKeeper = Resources.Load<Sprite>("People/" + shopKeeperName);
        instance.GetComponent<ShopManager>().Init(shopKeeper, name, items, showHealth);
    }

    private void Update()
    {
        moneyCounter.text = "Coins: " + PermanentState.money;
        healthDisplay.SetCurrHealth(PermanentState.health);
    }
}

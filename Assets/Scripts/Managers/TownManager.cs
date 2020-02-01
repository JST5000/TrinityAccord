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

    public CardItem[] stock = null;

    private void Start()
    {
        healthDisplay.SetMaxHealth(PermanentState.MaxHealth);
        ResetStock();
    }

    private void ResetStock()
    {
        stock = null;
    }
    
    public void OpenAttackShop()
    {
        AddStock(CardDataUtil.ChooseNWithoutReplacement(CardPools.GetAllDraftableAttacks(), 3));

        List<ShopItem> inventory = new List<ShopItem>(stock);

        Enter("Attack Shop", "Jenny", inventory, false, "See any you like?");
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

        List<int> costs = new List<int>();
        costs.Add(3);
        AddStock(CardDataUtil.ChooseNWithoutReplacement(CardPools.GetAllDraftableSpells(), 1), costs);
        inventory.Add(stock[0]);

        inventory.Add(new RemovalItem());
        Enter("Mapper's Camp", "CampfireManOnly", inventory, true, "Left, Right. Are you lost?");
    }

    public void OpenSailboat()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new RelativeTravelItem(true, 1, "SailingIcon", skipLevel: true, increasedDifficulty: false));
        inventory.Add(new RelativeTravelItem(false, 0, "SwimmingIcon", skipLevel: false, increasedDifficulty: false));
        Enter("Harbor", "WaterBoy", inventory, true, "Pay for safe travel?");
    }

    public void OpenArtistHill()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new HealthItem(2, 1));
        inventory.Add(new PackItem());
        inventory.Add(new RelativeTravelItem(false, 0, "JumpingOffCliffIcon", false, increasedDifficulty: false, "Onward"));
        Enter("Kwame's Hill", "Kwame", inventory, true, "Hahaha, welcome!");
    }

    public void OpenTent()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        inventory.Add(new HealthItem(3, 2));

        AddStock(CardDataUtil.ChooseNWithoutReplacement(CardPools.GetAllDraftableCards(), 1));
        inventory.Add(stock[0]);

        Enter("Explorer's Tent", "Explorer", inventory, true, "The right is new!");
    }

    public void OpenNoveltyShop()
    {
        List<CardData> cards = new List<CardData>();
        cards.Add(new Assault());
        cards.Add(new Arcana());
        cards.Add(new Assassin());
        AddStock(cards);

        List<ShopItem> inventory = new List<ShopItem>(stock);
        Enter("Novelty Shop", "Dave", inventory, false, "New wares!");
    }

    public void OpenHealthShop()
    {
        Enter("Health Shop", "James", GetHealthShopInventory(), showHealth: true);
    }

    public void SallyShop()
    {
        List<ShopItem> inventory = new List<ShopItem>();
        List<int> costs = new List<int>();
        costs.Add(0);

        AddStock(CardDataUtil.ChooseNWithoutReplacement(CardPools.GetAllDraftableCards(), 1), costs);
        
        inventory.Add(stock[0]);
        Enter("Sarah's World", "Sarah_Kid", inventory, false, "I found this, do you want it?");
    }

    public void OpenOasis()
    {
        List<ShopItem> inventory = GetHealthShopInventory();

        AddStock(CardDataUtil.ChooseNWithoutReplacement(CardPools.GetAllDraftableSpells(), 1));
        inventory.Add(stock[0]);

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
        Enter("Health Shop", "Darnell_Closeup", GetHealthShopInventory(), showHealth: true, "Sarah found something!");
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
        inventory.Add(new HealthItem(2, 1));
        inventory.Add(new RelativeTravelItem(true, 0, "MountainPath", false, increasedDifficulty: false));
        Enter("Vantage Point", "Hiker", inventory, true, "Quite a sight eh?");
    }

    public void LeaveTown(RelativePathName path)
    {
        bool increasedDifficulty = IsIncreasedDifficulty(path);
        PermanentState.MoveToNextTown(path.left);
        PermanentState.ChooseNextFight(increasedDifficulty);
        SceneManager.LoadScene("Encounter");
    }

    /// <summary>
    /// Checks for a warning sign, then sees if we went down that path
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private bool IsIncreasedDifficulty(RelativePathName path)
    {
        if(WarningSign.IsInScene()) {
            return WarningSign.GetDirectionOfChallengeFromScene() == path.left;
        } else
        {
            //No warning, so nothing to be worried about!
            return false;
        }
    }

    public void FightBoss(string encounterList)
    {
        //TODO add ability to choose left/right even as you set a specific fight
        //This is fine for Boss's since there is no next town, but will need to correct for other use cases
        PermanentState.MoveToNextTown(true);
        PermanentState.SetNextEncounter(EncounterInterpreter.GetEncounterFromText(encounterList));
        SceneManager.LoadScene("Encounter");
    }

    /// <summary>
    /// Keeps the stock the same between opens of the shop. Only used for CardData
    /// </summary>
    /// <param name="cards"></param>
    private void AddStock(List<CardData> cards, List<int> costs = null)
    {
        if (stock == null)
        {
            List<CardItem> items = new List<CardItem>();
            for (int i = 0; i < cards.Count; ++i)
            {
                if (costs == null)
                {
                    items.Add(new CardItem(cards[i]));
                }
                else
                {
                    items.Add(new CardItem(cards[i], costs[i]));
                }
            }
            stock = items.ToArray();
        }
    }


    private void Enter(string name, string shopKeeperName, List<ShopItem> items)
    {
        Enter(name, shopKeeperName, items, false);
    }

    private void Enter(string name, string shopKeeperName, List<ShopItem> items, bool showHealth, string message = "")
    {
        
        /*Shops have been removed from the game currently.
        GameObject instance = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/ShopUI"));
        instance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        instance.transform.position = new Vector3(0, 0, 0);
        Sprite shopKeeper = Resources.Load<Sprite>("People/" + shopKeeperName);
        instance.GetComponent<ShopManager>().Init(shopKeeper, name, items, message); */
    }

    private void Update()
    {
        moneyCounter.text = "Coins: " + PermanentState.Money;
        healthDisplay.SetCurrHealth(PermanentState.Health);
    }
}

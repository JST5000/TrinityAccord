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
        Enter("Attack Shop");
    }

    public void OpenQuestStand()
    {
        Enter("Quest Stand");
    }

    public void OpenHealthShop()
    {
        Enter("Health Shop");
    }

    public void OpenCardRemovalStand()
    {
        Enter("Card Removal Stand");
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

    private void Enter(string name)
    {
        response.text = "You entered the " + name + "!";
        GameObject instance = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/ShopUI"));
        instance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        instance.transform.position = new Vector3(0, 0, 0);
    }
}

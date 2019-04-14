using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HelpManager : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI textContent;
    public CanvasGroup left;
    public CanvasGroup right;
    public TextMeshProUGUI pageCounter;

    private List<HelpPage> pages;
    public Transform CardWithLabels;
    public Transform EnemyWithLabels;


    private int currPage = 0;
    private Transform currentPagePrefab;

    // Start is called before the first frame update
    void Start()
    {
        InitializePages();
        if(currPage <= 0)
        {
            CanvasGroupManip.Disable(left);
        }
        if(currPage >= pages.Count - 1)
        {
            CanvasGroupManip.Disable(right);
        }
        LoadPage(pages[currPage]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializePages()
    {
        pages = new List<HelpPage>();
        pages.Add(new HelpPage("General Help", "This is a battle for survival."
            + "\n\nIn each fight you must disarm your opponents before they attack you by using your deck of tools."
            + "\n\nTo win, defeat 6 encounters of increasing difficulty. ", null));
        pages.Add(new HelpPage("Cards", "", CardWithLabels));
        pages.Add(new HelpPage("Enemies", "", EnemyWithLabels));
        pages.Add(new HelpPage("Deck and Hand", "Your deck starts with 8 cards." +
            "\n\nEvery turn you draw 4 cards." +
            " At the end of each turn, all your cards are discarded." +
            "\n\nIf your deck runs out of cards, your discard is shuffled in." +
            "\n\nPlay cards by clicking on them, then on their target."
            , null));
        pages.Add(new HelpPage("Progression", "After every encounter, you get to add a new card to your deck." +
            "\n\nIf you lose, your deck loses all cards you added." +
            "\n\nGood Luck!", null));
    }

    private void ClearPrevPrefab()
    {
        if (currentPagePrefab != null)
        {
            Destroy(currentPagePrefab.gameObject);
            currentPagePrefab = null;
        }
    }


    private void LoadPage(HelpPage page)
    {
        //Unload previous
        ClearPrevPrefab();
        //Load new page
        title.text = page.Title;
        textContent.text = page.TextContent;
        if(page.Prefab != null)
        {
            currentPagePrefab = Instantiate(page.Prefab, GameObject.Find("Canvas").transform, false);
        }
        UpdatePageCounter();
    }

    private void UpdatePageCounter()
    {
        //Indexing from 1
        pageCounter.text = (currPage + 1) + "/" + pages.Count;
    }

    public void ClickLeft()
    {
        currPage--;
        if (currPage == 0)
        {
            CanvasGroupManip.Disable(left);
        }
        CanvasGroupManip.Enable(right);
        LoadPage(pages[currPage]);
        UpdatePageCounter();
    }

    public void ClickRight()
    {
        currPage++;
        if (currPage == pages.Count - 1)
        {
            CanvasGroupManip.Disable(right);
        }
        CanvasGroupManip.Enable(left);
        LoadPage(pages[currPage]);
        UpdatePageCounter();
    }



    public void ExitHelp()
    {
        ClearPrevPrefab();
        Destroy(gameObject);
    }
}

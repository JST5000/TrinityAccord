using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransitionManager : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI SubHeader;

    public float MaxDurationSeconds = 3;
    private float CurrentDuration = 0;

    private CanvasGroup CanvasG;

    public void Init(string title, string quote, string attribution = "")
    {
        this.Title.text = title;
        this.SubHeader.text = GetQuote();
        CanvasGroupManip.Enable(GetCanvasGroup());
    }



    // Start is called before the first frame update
    void Start()
    {
        GameUI.SetVisibilityOfGameUI(false);
        CanvasGroupManip.Disable(GetCanvasGroup());
        Init(PermanentState.GetFightTitle(), GetQuote());
    }

    // Update is called once per frame
    void Update()
    {
        CurrentDuration += Time.deltaTime;
        if(CurrentDuration >= MaxDurationSeconds)
        {
            this.Disable();
        }
    }

    private CanvasGroup GetCanvasGroup()
    {
        if(CanvasG == null)
        {
            CanvasG = GetComponent<CanvasGroup>();
        }
        return CanvasG;
    }

    public void Disable()
    {
        //This means the selection is done, so the quote is relevant
        if (!PermanentState.PauseGameInteraction)
        {
            GameUI.SetVisibilityOfGameUI(true);
            CanvasGroupManip.Disable(GetCanvasGroup());
        }
    }
    
    private class Quote
    {
        public string fullQuote;

        public Quote(string quote, string attribution)
        {
            fullQuote = GenerateQuote(quote, attribution);
        }

        private string GenerateQuote(string quote, string givenAttribution)
        {
            string actualAttribution = givenAttribution;
            if (givenAttribution == null || givenAttribution == "")
            {
                actualAttribution = "Anonymous";
            }
            return "\"" + quote + "\"\n- " + actualAttribution;
        }
    }

    public string GetQuote()
    {
        Quote[] quotes =
        {
            new Quote("To venture into the unknown is folly. To wait, doubly so.", "Phil"),
            new Quote("Never let anybody tell you that you're not lost in the wilderness.", "Inspiro"), //Inspiro
            new Quote("Just because you're a failure doesn't mean you're human.", "Common Wisdom"),
            new Quote("Immortality is a gift, humanity is the curse.", "Anonymous"),
            new Quote("Don't die.", "Common Wisdom"),
            new Quote("Peace is unstable, conflict is inevitable.", "Common Wisdom"),
            new Quote("Attack, Disarm, Live.", "You"),
            new Quote("The creature you are about to fight had a great backstory I'm sure.", "J.K."),
            new Quote("Where there's a Will, there's incompetence.", "Will's Sergeant"),
            new Quote("I'm good. How are you?", "Will"),
            new Quote("Dust to dust, ad infinitum.", "James"),
            new Quote("To die is human, to continue onward is our way.", "Common Wisdom"),
            new Quote("My mark defines me, it gives me purpose.", "James"),
            new Quote("I saw them! The benevolent behemoths of the sea.", "a Fisherman"),
            new Quote("Fire flows fast, blossoms bloom slow.", "Common Wisdom"),
            new Quote("If you open your mind, one day you might become an individual.", "Anonymous"), //Inspiro
            new Quote("If you find you are dying a lot, get good.", "Daniel"),
            new Quote("Risky is a terrible name for a road!", "Anonymous"),
            new Quote("Hello, [tap] [tap] is this thing on?", "The Captain"),
            new Quote("When the dust settles, we will all be mourned.", "Scripture 3:14"),
            new Quote("Ashes scare me, they look like dust, but they never revive.", "The Executioner"),
            new Quote("So I told him, 'No! You can't fit a behemoth bone there!'", "The Architect"),
            new Quote("I can fix you... for a price.", "The 'Explorer'"),

        };
        if(PermanentState.unusedQuotes.Count == 0)
        {
            for(int i = 0; i < quotes.Length; ++i)
            {
                PermanentState.unusedQuotes.Add(i);
            }
        }
        //Guarentees unique quotes for a given run in O(1) after initializing the list
        int unusedQuoteIndex = Random.Range(0, PermanentState.unusedQuotes.Count - 1);
        int randIndex = PermanentState.unusedQuotes[unusedQuoteIndex];
        PermanentState.unusedQuotes.Remove(unusedQuoteIndex);
        return quotes[randIndex].fullQuote;
    }
}

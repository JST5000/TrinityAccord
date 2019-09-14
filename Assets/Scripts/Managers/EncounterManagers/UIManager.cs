using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum GameMode {SelectCard, PickTarget, Animation,PickCardInHand, SelectingOption  }; 

public class UIManager : MonoBehaviour
{
    public static GameMode currentMode = GameMode.SelectCard;
    private static GameMode prevMode = currentMode;

    //Finding user input to cast card
    static Target requiredInput = Target.NONE;
    static CardManager selectedCard = null;
    static StackManager cardStack;
    static CardData actionCard;
    static int actionsNeeded;//Used to discard multiple cards for backpack

    private bool canEndTurn = true;

    private enum Status { USED, UNUSED };

    public static float timeSinceAutoEndTurn = 0;

    private static string StatusMessageObjectName = "StatusMessage";

    private static GameMode GetCurrentMode()
    {
        return currentMode;
    }

    private static void SetCurrentMode(GameMode mode)
    {
        prevMode = currentMode;
        currentMode = mode;
    }
    public static void selectCardInHand(CardData card,int count)
    {
        SetCurrentMode(GameMode.PickCardInHand);
        HandManager.Get().UpdateAllCardsInHand();
        actionCard = card;
        actionsNeeded = count;
        StackManager.Get().PauseExecution();
        GameObject.Find("EndTurnButton").GetComponent<EndTurnUI>().PauseAutoEndTurn();

        GameObject statusMessage = GameObject.Find(StatusMessageObjectName);
        statusMessage.GetComponent<TextMeshProUGUI>().text = $"Choose {count} card(s)";
        CanvasGroupManip.Enable(statusMessage.GetComponent<CanvasGroup>());
    }
    public static void cardInHandClicked(CardManager card)
    {
        actionsNeeded -= actionCard.SecondAction(card);
        if (actionsNeeded <= 0)
        {
            StackManager.Get().ResumeExecution();
            SetCurrentMode(GameMode.SelectCard);

            CanvasGroupManip.Disable(GameObject.Find(StatusMessageObjectName).GetComponent<CanvasGroup>());

            GameObject.Find("EndTurnButton").GetComponent<EndTurnUI>().ResumeAutoEndTurn();
        }
    }

    public static void StartSelectingOption()
    {
        SetCurrentMode(GameMode.SelectingOption);
    }

    public static void DoneSelectingOption()
    {
        SetCurrentMode(prevMode);
    }

    private static void SetAndHighlightSelectedCard(CardManager newSelection)
    {
        if (selectedCard != null)
        {
            selectedCard.GetComponent<CardUIUpdater>().ResetHighlight();
        }
        selectedCard = newSelection;
        if (newSelection != null)
        {
            selectedCard.GetComponent<CardUIUpdater>().Highlight();
            //Ex. Clone uses this to enable all disabled cards in hand
            selectedCard.GetCardData().OnSelectedInHand();
        }

    }

    private void Start()
    {
        currentMode = GameMode.SelectCard;
        cardStack = GameObject.Find("StackHolder").GetComponent<StackManager>();
    }

    private void Awake()
    {
        currentMode = GameMode.SelectCard;
    }

    private void Update()
    {
        if(GetCurrentMode().Equals(GameMode.Animation))
        {
            if(cardStack.IsEmpty())
            {
                SetCurrentMode(GameMode.SelectCard);
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            ResetSelection();
        }
        timeSinceAutoEndTurn += Time.deltaTime;
    }

    private void ResetSelection()
    {
        if (selectedCard != null)
        {
            selectedCard.GetComponent<CardUIUpdater>().ResetHighlight();
            selectedCard = null;
            if(currentMode == GameMode.PickCardInHand || currentMode == GameMode.PickTarget)
            {
                currentMode = GameMode.SelectCard;
            }
            HandManager.Get().UpdateAllCardsInHand();
        }
    }


    public void clickEnemy(GameObject clicked)
    {
        Debug.Log("Clicked an Enemy named: " + clicked.name);
        if (GetCurrentMode().Equals(GameMode.PickTarget) && requiredInput.Equals(Target.ENEMY))
        {
     
            selectedCard.GetCardData().SelectedTarget = clicked;
            PlayCard();
        }
    }
    public void clickBoard(GameObject clicked)
    {
        Debug.Log("Clicked the Board");
        /*
        if (GetCurrentMode().Equals(GameMode.PickTarget) && requiredInput.Equals(Target.ALL_ENEMIES)) //|| requiredInput.Equals(Target.BOARD)))
        {
            selectedCard.GetCardData().selectedTarget = clicked;
            PlayCard();
            //Triggers the card effect
            
        }
        */
    }

    public void clickCardInHand(GameObject clicked)
    {
        if (clicked.GetComponentInParent<HandManager>())
        {
            if (GetCurrentMode().Equals(GameMode.SelectCard))
            {
                SelectCard(clicked);
            }
            else if (currentMode.Equals(GameMode.PickTarget))
            {
                if (requiredInput.Equals(Target.CARD))
                {

                    CardData[] card = { clicked.GetComponent<CardManager>().GetCardData() };
                    if (!card[0].Equals(selectedCard.GetCardData()))
                    {
                        selectedCard.GetCardData().SelectedTarget = clicked;
                        PlayCard();
                    }
                }
                else
                {
                    SelectCard(clicked);
                }
            }
            else if (currentMode.Equals(GameMode.PickCardInHand))
            {
                cardInHandClicked(clicked.GetComponent<CardManager>());
            }
        }
        
        Debug.Log("Clicked a Card named: " + clicked.name);
    }

    private void SelectCard(GameObject clicked) {
        CardManager cardMan = clicked.GetComponent<CardManager>();
        if (cardMan.IsPlayable())
        {
            SetAndHighlightSelectedCard(cardMan);
            Debug.Log("Selected Card = " + selectedCard);
            requiredInput = selectedCard.GetTargets();
            if (requiredInput == Target.BOARD || requiredInput == Target.ALL_ENEMIES)
            {
                selectedCard.GetCardData().SelectedTarget = GameObject.Find("Board");
                PlayCard();
            }
            else
            {
                SetCurrentMode(GameMode.PickTarget);
            }
        } else
        {
            Debug.Log("The card: " + cardMan.GetCardData().GetName() + " was unplayable (Likely due to cost).");
        }
    }

    public void PlayCard()
    {
        //Pay Cost
        Debug.Log(selectedCard);
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.PayEnergy(selectedCard.GetCardData().GetCost());
        
        //Remove the card from the hand 
        selectedCard.SetEmpty();

        if (player.IsBlind())
        {
            CardData.playCardRandomTarget(selectedCard.GetCardData());
        }
        else
        {
            //Put the card on the stack
            StackManager playStack = GameObject.Find("StackHolder").GetComponent<StackManager>();
            playStack.Push(selectedCard.GetCardData(), StackUsage.PLAY);
        }

        //Removes highlight for the next card to appear
        selectedCard.GetComponent<CardUIUpdater>().ResetHighlight();

        //Recheck if any conditions are not met
        HandManager.Get().UpdateAllCardsInHand();

        currentMode = GameMode.Animation;
        requiredInput = Target.NONE;
    }

    public void clickCardOnStack(GameObject clicked)
    {
        
    }

    public void clickDeck(GameObject clicked)
    {
        Debug.Log("Clicked the Deck");
        DeckManager.Get().PrintDeck();
    }

    public void clickDiscard(GameObject clicked)
    {
        Debug.Log("Clicked the Discard Pile");
        DeckManager.Get().DisplayDiscardToPlayer();
    }

    public void autoEndTurn()
    {
        if(canEndTurn && actionsNeeded <= 0)
        {
            canEndTurn = false;
            endTurn();
            timeSinceAutoEndTurn = 0;
            canEndTurn = true;
        } 
    }

    public void clickEndTurn()
    {
        Debug.Log("Clicked End Turn");
        float manualEndTurnLockout = 1f;
        if(timeSinceAutoEndTurn >= manualEndTurnLockout)
        {
            endTurn();
        }


    }

    private void endTurn()
    {
        //During animation you should not be able to end turn
        if ((!GetCurrentMode().Equals(GameMode.Animation)
            || GetCurrentMode().Equals(GameMode.PickCardInHand)))
        {
            ResetSelection();

            GameObject.Find("Board").GetComponent<EncounterManager>().EndTurn();

            DeckManager decks = DeckManager.Get();
            decks.EndTurn(); //Discards hand

            Player player = GameObject.Find("Player").GetComponent<Player>();
            player.EndTurn(); //Resets energy

            decks.StartTurn(); //Draws hand
        }
    }


}

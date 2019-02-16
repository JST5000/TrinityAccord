using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum GameMode {SelectCard, PickTarget, Animation  }; 

public class UIManager : MonoBehaviour
{
    private static GameMode currentMode = GameMode.SelectCard;

    //Finding user input to cast card
    static Target requiredInput = Target.NONE;
    static CardManager selectedCard = null;
    static StackManager cardStack;

    public const bool ENABLE_CLICK_BORDERS = false;
    private enum Status { USED, UNUSED };

    private GameMode GetCurrentMode()
    {
        return currentMode;
    }

    private void SetCurrentMode(GameMode mode)
    {
        currentMode = mode;
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
    }

    private void updateHitboxWithStatus(Status s, GameObject obj) 
    {
        if (ENABLE_CLICK_BORDERS)
        {
            DebugBorder border = obj.GetComponentInChildren<DebugBorder>();
            if (border != null)
            {
                Color output = Color.black;
                if (s == Status.USED)
                {
                    output = Color.green;
                }
                else
                {
                    output = Color.red;
                }
                border.SetRGBColorTemporarily(output);
           
            }
            
        }
    }

    public void clickEnemy(GameObject clicked)
    {
        Debug.Log("Clicked an Enemy named: " + clicked.name);
        if (GetCurrentMode().Equals(GameMode.PickTarget) && requiredInput.Equals(Target.ENEMY))
        {
            PlayCard();
            //TODO add the effect to this call
            updateHitboxWithStatus(Status.USED, clicked);
        }
        else
        {
            updateHitboxWithStatus(Status.UNUSED, clicked);

        }
    }

    public void clickCardInHand(GameObject clicked)
    {
        Debug.Log(currentMode);
        Debug.Log(GetCurrentMode());
        if(GetCurrentMode().Equals(GameMode.SelectCard))
        {
            //TODO
            //If (cardManager attached to clicked isPlayable) {
            //  put card into selected;
            //  Change currentMode to PickTarget;
            //  Log the card selected
            //} else { Log that card was not playable. }
            CardManager cardMan = clicked.GetComponent<CardManager>();
            if (cardMan.IsPlayable())
            {
                selectedCard = cardMan;
                Debug.Log("Selected Card = " + selectedCard);
                requiredInput = selectedCard.GetTargets();
                SetCurrentMode(GameMode.PickTarget);

                updateHitboxWithStatus(Status.USED, clicked);
            } else
            {
                Debug.Log("The card: " + cardMan.GetCardData().CardName() + " was unplayable (Likely due to cost).");
            }
        } else if(currentMode.Equals(GameMode.PickTarget))
        {
            if(requiredInput.Equals(Target.CARD))
            {
                PlayCard();
                CardData[] card = { clicked.GetComponent<CardManager>().GetCardData() };
                selectedCard.Action(card);
            }
        }
        Debug.Log("Clicked a Card named: " + clicked.name);
        Debug.Log(currentMode);
    }

    public void PlayCard()
    {
        //Pay Cost
        Debug.Log(selectedCard);
        GameObject.Find("Player").GetComponent<Player>().PayEnergy(selectedCard.GetCardData().Cost());
        
        //Put the card on the stack
        StackManager playStack = GameObject.Find("StackHolder").GetComponent<StackManager>();
        playStack.Push(selectedCard);
        
        //Remove the card from the hand 
        selectedCard.SetEmpty();

        currentMode = GameMode.Animation;
        requiredInput = Target.NONE;
    }

    public void clickCardOnStack(GameObject clicked)
    {
        
    }

    public void clickDeck(GameObject clicked)
    {
        Debug.Log("Clicked the Deck");
        GameObject.Find("Deck").GetComponent<DeckManager>().PrintDeck();
        updateHitboxWithStatus(Status.UNUSED, clicked);
    }

    public void clickDiscard(GameObject clicked)
    {
        Debug.Log("Clicked the Discard Pile");
        GameObject.Find("Deck").GetComponent<DeckManager>().PrintDiscard();
        updateHitboxWithStatus(Status.UNUSED, clicked);
    }

    public void clickEndTurn(GameObject clicked)
    {
        Debug.Log("Clicked End Turn");

        //During animation you should not be able to end turn
        if (!GetCurrentMode().Equals(GameMode.Animation))
        {
            GameObject.Find("Board").GetComponent<EncounterManager>().EndTurn();

            DeckManager decks = GameObject.Find("Deck").GetComponent<DeckManager>();
            decks.EndTurn(); //Discards hand
            decks.StartTurn(); //Draws hand

            GameObject.Find("Player").GetComponent<Player>().EndTurn(); //Resets energy

            updateHitboxWithStatus(Status.USED, clicked);
        }
    }

    public void clickBoard(GameObject clicked)
    { 
        Debug.Log("Clicked the Board");
        if(GetCurrentMode().Equals(GameMode.PickTarget) && requiredInput.Equals(Target.ALL_ENEMIES))
        {
            PlayCard();
            //TODO add effect call to card
        }
        updateHitboxWithStatus(Status.UNUSED, clicked);
    }
}

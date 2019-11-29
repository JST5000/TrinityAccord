using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        UIManager uiMan = GameObject.Find("UIManagerWrapper").GetComponent<UIManager>();
        //TODO, Test
        if (GetComponent<CardManager>())
        {
            if (UIManager.currentMode.Equals(GameMode.PickTarget) && UIManager.requiredInput.Equals(Target.CARD))
            {
                uiMan.clickCardInHand(gameObject);
            }
        }
        else
        {
            uiMan.clickEnemy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

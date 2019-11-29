using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject itemBeingDragged;
    Camera mainCamera = null;
    UIManager uiMan = null;
    Vector3 startPosition;

    private Camera GetCamera()
    {
        if (mainCamera == null)
        {
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
        return mainCamera;
    }

    private UIManager GetUIManager()
    {
        if (uiMan == null)
        {
            uiMan = GameObject.Find("UIManagerWrapper").GetComponent<UIManager>();
        }
        return uiMan;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;

        //For click and drag only clicks first for targetted abilities. Otherwise does not.
        Target target = GetComponent<CardManager>().GetTargets();
        if (!(target.Equals(Target.ALL_ENEMIES) || target.Equals(Target.NONE) 
            || target.Equals(Target.CHOOSE3))) {
            GetUIManager().clickCardInHand(gameObject);
        }


        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = GetCamera().ScreenToWorldPoint(Input.mousePosition);
        //Keep it on the same plane as before
        mousePos.z = startPosition.z;
        transform.position = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        transform.position = startPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        //For click and drag clicks at the end to trigger the ability
        Target target = GetComponent<CardManager>().GetTargets();
        if (target.Equals(Target.ALL_ENEMIES) || target.Equals(Target.NONE)
            || target.Equals(Target.CHOOSE3))
        {
            GetUIManager().clickCardInHand(gameObject);
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

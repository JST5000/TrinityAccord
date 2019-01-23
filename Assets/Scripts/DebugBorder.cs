using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugBorder : MonoBehaviour
{
    private bool tryMatching = true;
    private int maxAttempts = 10;

    // Start is called before the first frame update
    void Start()
    {
        MatchParentDimensions();
        UpdateVisibility();        
    }

    void MatchParentDimensions()
    {
        RectTransform parentRect = transform.parent.transform as RectTransform;
        (transform as RectTransform).sizeDelta = new Vector2(parentRect.sizeDelta.x, parentRect.sizeDelta.y);
        Debug.Log("Changed to " + parentRect.sizeDelta.x + " w and " + parentRect.sizeDelta.y + " h");
        tryMatching = ((transform as RectTransform).sizeDelta.x == 0 && (transform as RectTransform).sizeDelta.y == 0);
    }

    //Visibility is determined at compile time. This is a debug only functionality
    public void UpdateVisibility() {
        Image border = gameObject.GetComponent(typeof(Image)) as Image;
        if (UIManager.ENABLE_CLICK_BORDERS) {
            //Makes it visible
            border.color = new Color(border.color.r, border.color.g, border.color.b, 1);
        }
        else {
            //Makes it invisible
            border.color = new Color(border.color.r, border.color.g, border.color.b, 0);
        }
    }

	void SetColor() {

	}

    // Update is called once per frame
    void Update()
    {
        //Deals with race condition between parent layout formatting and child accessing of width/length
        if (UIManager.ENABLE_CLICK_BORDERS && tryMatching)
        {
            MatchParentDimensions();
            maxAttempts--;
            if(maxAttempts <= 0)
            {
                tryMatching = false;
            }
        }
    }
}

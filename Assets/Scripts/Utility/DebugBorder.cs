﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugBorder : MonoBehaviour
{
    private bool tryMatching = true;
    private int maxAttempts = 10;
    private float maxColorChangedTimer = 1;
    private float colorChangedTimer = 0;
    private Color defaultColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        UpdateDimensions();
       // UpdateVisibility();        
    }

    void UpdateDimensions()
    {
        MatchTargetDimensions(transform.parent);
    }

    void MatchTargetDimensions(Transform obj)
    {
        RectTransform objRect = obj as RectTransform;
        (transform as RectTransform).sizeDelta = new Vector2(objRect.sizeDelta.x, objRect.sizeDelta.y);
    }

	public void SetRGBColorTemporarily(Color col) {
        SetRGBColor(col);
        colorChangedTimer = maxColorChangedTimer;
	}

    private void SetRGBColor(Color col)
    {
        Image border = GetComponent<Image>();
        border.color = new Color(col.r, col.g, col.b, border.color.a);
    }

    // Update is called once per frame
 /*   void Update()
    {
        //Deals with race condition between parent layout formatting and child accessing of width/length
        if (UIManager.ENABLE_CLICK_BORDERS && tryMatching)
        {
            UpdateDimensions();
            maxAttempts--;
            if(maxAttempts <= 0)
            {
                tryMatching = false;
            }
        }
        if(colorChangedTimer > 0)
        {
            colorChangedTimer -= Time.deltaTime;
            if(colorChangedTimer < 0)
            {
               SetRGBColor(defaultColor);
            }
        }
    } */
}

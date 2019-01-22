using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugBorder : MonoBehaviour
{
	public static bool initialEnableDebugBorders = false;
    private bool enableDebugBorders = initialEnableDebugBorders;

    // Start is called before the first frame update
    void Start()
    {
        MatchParentDimensions();
        UpdateVisibility();        
    }

    void MatchParentDimensions()
    {
        Vector3 parentRect = gameObject.GetComponentInParent<Renderer>().bounds.size;
    //TODO: Make the width and height of the image the same as the parent transform. That way it will make an outline.

    }

    void SetEnableDebugBorders(bool enable)
    {
        if (enableDebugBorders != enable)
        {
            enableDebugBorders = enable;
            UpdateVisibility();
        }
    }


    void UpdateVisibility() {
        Image border = gameObject.GetComponent(typeof(Image)) as Image;
        if (enableDebugBorders) {
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
        
    }
}

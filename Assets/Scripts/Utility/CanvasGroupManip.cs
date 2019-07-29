using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupManip : MonoBehaviour
{
    public static void Disable(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.blocksRaycasts = false;
        cg.interactable = false;
    }

    public static void Enable(CanvasGroup cg, bool blocksRaycasts = true)
    {
        cg.alpha = 1;
        cg.blocksRaycasts = blocksRaycasts;
        cg.interactable = true;
    }

    public static void SetVisibility(bool enableCondition, CanvasGroup cg, bool blocksRaycasts = true)
    {
        if(enableCondition)
        {
            Enable(cg, blocksRaycasts);
        } else
        {
            Disable(cg);
        }
    }

    public static void Refresh(CanvasGroup cg)
    {
        Disable(cg);
        Enable(cg);
    }
}

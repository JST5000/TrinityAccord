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

    public static void Enable(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.blocksRaycasts = true;
        cg.interactable = true;
    }
}

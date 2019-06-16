using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    private static string nameOfCanvasGroupHolder = "GameUI";

    public static void SetVisibilityOfGameUI(bool visible)
    {
        CanvasGroupManip.SetVisibility(visible, GameObject.Find(nameOfCanvasGroupHolder).GetComponent<CanvasGroup>());
    }
}

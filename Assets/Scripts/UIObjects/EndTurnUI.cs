using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnUI : MonoBehaviour
{

    HandManager handMan;
    StackManager stack;
    UIManager uiMan;

    Button bg;

    // Start is called before the first frame update
    void Start()
    {
        handMan = GameObject.Find("Hand").GetComponent<HandManager>();
        stack = GameObject.Find("StackHolder").GetComponent<StackManager>();

        bg = GetComponent<Button>();
        uiMan = GameObject.Find("UIManagerWrapper").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(handMan.HasPlayable() || !stack.IsDisplayEmpty())
        {
            SetColorToNotDone();
        } else
        {
            //SetColorToDone();
            uiMan.autoEndTurn();
        }
    }

    private void SetColorToDone()
    {
        var targetColor = Color.yellow;
        SetColors(targetColor);
    }

    private void SetColorToNotDone()
    {
        Color targetColor = new Color(1, 1, 1, 1);
        SetColors(targetColor);
    }

    private void SetColors(Color targetColor)
    {
        var colors = GetComponent<Button>().colors;
        colors.normalColor = targetColor;
        float highlightDarken = .9f;
        colors.highlightedColor = new Color(targetColor.r * highlightDarken, targetColor.g * highlightDarken, targetColor.b * highlightDarken, colors.highlightedColor.a);
        colors.pressedColor = new Color(targetColor.r * .5f, targetColor.g * .5f, targetColor.b * .5f, colors.pressedColor.a);
        GetComponent<Button>().colors = colors;
    }
}

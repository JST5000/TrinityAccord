using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeywordsToIcons : MonoBehaviour
{
    public TextMeshProUGUI effectText;
    public TextMeshProUGUI iconShorthandText;

    Dictionary<string, string> keywordsToIcons = InitKeywordsToIcons();

    static Dictionary<string, string> InitKeywordsToIcons()
    {
        Dictionary<string, string> keyToIcon = new Dictionary<string, string>();
        keyToIcon.Add("flip", InLineIcon.FLIP);
        keyToIcon.Add(InLineIcon.ON_DISARM, InLineIcon.ON_DISARM);
        keyToIcon.Add("summon", InLineIcon.SUMMON);
        keyToIcon.Add("blind", InLineIcon.BLIND);
        return keyToIcon;
    }
    
    private string ExtractKeyIcons(string text)
    {
        string output = "";
        char[] delimiters = { ' ', '\n', ':', ',' };
        string[] tokens = text.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);
        foreach(string token in tokens)
        {
            keywordsToIcons.TryGetValue(token.ToLower(), out string translated);
            if (translated != null && !output.Contains(translated))
            {
                output += translated;
            }
        }

        return output;
    }

    // Update is called once per frame
    void Update()
    {
        iconShorthandText.text = ExtractKeyIcons(effectText.text);
    }
}

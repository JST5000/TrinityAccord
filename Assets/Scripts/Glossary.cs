using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glossary : MonoBehaviour
{
    public static string[] glossary { get; set; } =
        {
            InLineIcon.DAMAGE + ": X - Deal X damage to the the player." ,
            InLineIcon.ON_DISARM + ": X - When a life is lost, change this effect to X." ,
            "Attack - Red cards. Usually do damage." ,
            "Blind X - All attacks have random targets for X turns." ,
            "Charge X - Every time this card is discarded, increase its attack by x. Resets on use.",
            "Drowsy - Put the enemy to Sleep in 1 turn. Damage wakes them up!",
            "Flip - Transform this into a new card or new effect.",
            "Grow X - Every time this card is played, increase its attack by X." ,
            "Spell - Blue cards. Usually are utility." ,
            "Preserve - Target card is not discarded at the end of the turn.",
            "Sleep - Stop the enemy's timer for up to 2 turns. Damage wakes them up!",
            "Disarm - Reset the enemie's attack timer to max and Stun them." ,
            "Summon - Creates a new enemy!",
            "Stun - Stop the enemy's timer for 1 turn.",
            "Vulnerable - Recieves double damage."
        };
    public static Dictionary<string, string> Keywords { get; set; } = InitializeKeywords();

    public static Dictionary<string, string> InitializeKeywords()
    {
        Dictionary<string, string> keywords = new Dictionary<string, string>();
        foreach (string helpLine in glossary)
        {
            int dividerIndex = helpLine.IndexOf('-');
            string keyword = helpLine.Substring(0, dividerIndex - 1).ToLower();
            string meaning = helpLine;

            //Get rid of " X" at the end of some keywords
            if (keyword[keyword.Length - 1] == 'x')
            {
                keyword = keyword.Substring(0, keyword.Length - 2);
            }

            keywords.Add(keyword, meaning);
        }

        return keywords;
    }
}

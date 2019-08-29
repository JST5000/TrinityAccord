using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelItem : ShopItem
{
    protected string SceneName;
    protected bool SkipLevel;
    protected bool IncreasedDifficulty;

    public TravelItem(string optionName, int cost, string imageName, string sceneName, bool skipLevel, bool increasedDifficulty) 
        : base(optionName, cost, imageName: imageName)
    {
        this.SceneName = sceneName;
        this.SkipLevel = skipLevel;
        this.IncreasedDifficulty = increasedDifficulty;
    }    

    override public void Effect()
    {
        if(SkipLevel)
        {
            PermanentState.Wins++;
        }
        PermanentState.ChooseNextFight(IncreasedDifficulty);
        SceneManager.LoadScene(SceneName);
    }
}

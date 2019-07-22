using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelItem : ShopItem
{
    protected string SceneName;
    protected bool SkipLevel;

    public TravelItem(string optionName, int cost, string imageName, string SceneName, bool SkipLevel) : base(optionName, cost, imageName: imageName)
    {
        this.SceneName = SceneName;
        this.SkipLevel = SkipLevel;
    }    

    override public void Effect()
    {
        if(SkipLevel)
        {
            PermanentState.wins++;
        }
        PermanentState.ChooseNextFight();
        SceneManager.LoadScene(SceneName);
    }
}

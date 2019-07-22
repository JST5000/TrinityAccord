using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeTravelItem : TravelItem
{
    private bool left;

    public RelativeTravelItem(bool left, int cost, string imageName, bool skipLevel, string optionName = null) :
        base(
        optionName: optionName != null ? optionName : GetSceneName(left), 
        cost: cost, 
        imageName: imageName, 
        SceneName: "", //Will be loaded right before travel 
        SkipLevel: skipLevel)
    {
        this.left = left;
    }

    private static string GetSceneName(bool left)
    {
        return PermanentState.worldMap.GetChildSceneName(left);
    }

    override public void Effect()
    {
        //Ensures that the travel is relative to the current town
        if (SkipLevel)
        {
            SceneName = GetSceneName(left);
        } else
        {
            SceneName = "Encounter";
        }
        PermanentState.MoveToNextTown(left);
        base.Effect();
    }
}

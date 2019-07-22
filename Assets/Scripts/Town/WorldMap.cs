using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap
{
    private MapNode currentTown = null;

    public WorldMap()
    {
        currentTown = GenerateWorldMap();
    }

    private MapNode GenerateWorldMap()
    {
        MapNode SallyAndStand = new MapNode("SallyAndStand");
        MapNode Cabin = new MapNode("Cabin", null, SallyAndStand);
        MapNode Vantage = new MapNode("Vantage", SallyAndStand, null);
        MapNode ArtistHill = new MapNode("ArtistHill", null, Cabin);
        MapNode Campfire = new MapNode("Campfire", Cabin, Vantage);
        MapNode Market = new MapNode("Market", Vantage, null);
        MapNode Harbor = new MapNode("SailboatHarbor", ArtistHill, Campfire);
        MapNode Oasis = new MapNode("Oasis", Campfire, Market);

        MapNode ExplorerTent = new MapNode("ExplorerTent", Harbor, Oasis);

        return ExplorerTent;
    }

    public void MoveToNextTown(bool left)
    {
        if (left)
        {
            currentTown = currentTown.left;
        }
        else
        {
            currentTown = currentTown.right;
        }
    }

    public string GetChildSceneName(bool left)
    {
        if(left)
        {
            return currentTown?.left?.sceneName;
        } else
        {
            return currentTown?.right?.sceneName;
        }
    }

    public MapNode GetCurrentTown()
    {
        return currentTown;
    }

    public void Reset()
    {
        currentTown = GenerateWorldMap();
    }
}

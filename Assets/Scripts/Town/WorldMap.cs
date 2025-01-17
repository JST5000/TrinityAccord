﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap
{
    private MapNode currentTown = null;

    public List<MapNode> PathTraveled { get; set; } = new List<MapNode>();

    public WorldMap()
    {
        currentTown = GenerateWorldMap();
    }

    private MapNode GenerateWorldMap()
    {
        PathTraveled = new List<MapNode>();

        MapNode LastStand = new MapNode("Last Stand");
        MapNode Cabin = new MapNode("Cabin", null, LastStand);
        MapNode Vantage = new MapNode("Vantage", LastStand, null);
        MapNode ArtistHill = new MapNode("Artist Hill", null, Cabin);
        MapNode Campfire = new MapNode("Campfire", Cabin, Vantage);
        MapNode Market = new MapNode("Market", Vantage, null);
        MapNode Harbor = new MapNode("Harbor", ArtistHill, Campfire);
        MapNode Oasis = new MapNode("Oasis", Campfire, Market);

        MapNode Tent = new MapNode("Tent", Harbor, Oasis);

        PathTraveled.Add(Tent);

        return Tent;
    }

    public void MoveToNextTown(bool left)
    {
        if (left)
        {
            currentTown = currentTown.Left;
        }
        else
        {
            currentTown = currentTown.Right;
        }

        PathTraveled.Add(currentTown);
    }

    public string GetChildSceneName(bool left)
    {
        if(left)
        {
            return currentTown?.Left?.SceneName;
        } else
        {
            return currentTown?.Right?.SceneName;
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

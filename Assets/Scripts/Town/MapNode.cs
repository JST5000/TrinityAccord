using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode
{
    public string sceneName;
    public MapNode left;
    public MapNode right;
    
    public MapNode(string sceneName, MapNode left = null, MapNode right = null) {
        this.sceneName = sceneName;
        this.left = left;
        this.right = right;
    }
}

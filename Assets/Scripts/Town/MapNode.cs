using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode
{
    public string SceneName { get; set; }
    public MapNode Left { get; set; }
    public MapNode Right { get; set; }
    private string PictureName { get; set; }
    
    public MapNode(string sceneName, MapNode left = null, MapNode right = null) {
        this.SceneName = sceneName;
        this.Left = left;
        this.Right = right;
        this.PictureName = sceneName;
    }

    public Sprite GetMapIcon()
    {
        string PictureFolderPath = "Map_Icons/";
        return Resources.Load<Sprite>(PictureFolderPath + PictureName);
    }
}

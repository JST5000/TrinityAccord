using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPage
{
    public string Title;
    public string TextContent;
    public Transform Prefab;


    public HelpPage(string Title, string TextContent, Transform Prefab)
    {
        this.Title = Title;
        this.TextContent = TextContent;
        this.Prefab = Prefab;
    }

    public HelpPage(string Title, string TextContent)
    {
        this.Title = Title;
        this.TextContent = TextContent;
    }
}

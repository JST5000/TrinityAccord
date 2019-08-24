using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathIconManager : MonoBehaviour
{
    public Image[] pathIcons;

    // Start is called before the first frame update
    void Start()
    {
        List<MapNode> path = PermanentState.worldMap?.PathTraveled;
        CanvasGroupManip.SetVisibility(path != null, GetComponent<CanvasGroup>());

        DisplayPathIcons(path);
    }

    private void DisplayPathIcons(List<MapNode> path)
    {
        int iconsToShow = (path == null) ? 0 : path.Count;
        int ignoreManagerImageOffset = 0;
        for (int i = ignoreManagerImageOffset; i < pathIcons.Length; ++i)
        {
            int pathIndex = i - ignoreManagerImageOffset;
            bool displayIcon = pathIndex < iconsToShow;
            if (displayIcon)
            {
                pathIcons[i].sprite = path[pathIndex].GetMapIcon();
            }
            CanvasGroupManip.SetVisibility(displayIcon, pathIcons[pathIndex].GetComponent<CanvasGroup>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

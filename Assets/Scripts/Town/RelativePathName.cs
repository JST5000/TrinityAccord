using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelativePathName : MonoBehaviour
{
    public bool left = true;
    public bool updateText = true;

    // Start is called before the first frame update
    void Start()
    {
        if (updateText)
        {
            GetComponentInChildren<Text>().text = PermanentState.worldMap.GetChildSceneName(left);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

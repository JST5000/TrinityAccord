using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTransform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Returns the vector that is used
    public static Vector3 SetScaleToMatch(RectTransform child, RectTransform parent)
    {
        float xScale = parent.rect.width / child.rect.width;
        float yScale = parent.rect.height / child.rect.height;
        Vector3 scale = new Vector3(xScale, yScale, 1);
        child.localScale = scale;
        return scale;
    }
}

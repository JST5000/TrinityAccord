using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotOverrideCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
     //   if(GetComponentInParent<Canvas>() != null)
     //   {
            canvas.overrideSorting = false;
     //   }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

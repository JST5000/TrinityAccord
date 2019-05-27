using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = PermanentState.GetFightTitle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (PermanentState.wins < 5)
        {
            GetComponent<TextMeshProUGUI>().text = "Level " + (1 + PermanentState.wins);
        } else
        {
            GetComponent<TextMeshProUGUI>().text = "Final Boss!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

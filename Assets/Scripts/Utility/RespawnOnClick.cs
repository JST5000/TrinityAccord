using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            GameObject perm = GameObject.Find("PermanentState");
            if (perm != null)
            {
                PermanentState.ResetStatics();
                Destroy(perm);
                SceneManager.LoadScene("Encounter");
            }
        }
    }
}

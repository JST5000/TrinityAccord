using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentSingleton : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        CreateSingleton();
    }

    private void CreateSingleton()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}

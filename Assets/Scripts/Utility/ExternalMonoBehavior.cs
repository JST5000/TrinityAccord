using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalMonoBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreateSingleton();
    }

    private void CreateSingleton()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MonoBehaviorUtil");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void UseStartCoroutine(IEnumerator func)
    {
        StartCoroutine(func);
    }
}

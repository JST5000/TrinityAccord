using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNestedPrefab : MonoBehaviour
{
    void Awake()
    {
        PrefabInstance.OnPostprocessScene();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedEnemyLoader : MonoBehaviour
{
    public string EnemyName;
    // Start is called before the first frame update
    void Awake()
    {
        EnemyManager man = GetComponent<EnemyManager>();
        man.Init(EncounterInterpreter.InterpretWord(EnemyName));
    }
}

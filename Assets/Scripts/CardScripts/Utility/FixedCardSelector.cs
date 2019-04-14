using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCardSelector : MonoBehaviour
{

    public string CardName;
    // Start is called before the first frame update
    void Start()
    {
        CardManager man = GetComponent<CardManager>();
        man.Init(CardDataUtil.InterpretWord(CardName));  
    }
}

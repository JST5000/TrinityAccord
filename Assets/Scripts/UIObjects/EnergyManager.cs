using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{

    private int currEnergy;
    public Text energyText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetEnergy(int energy)
    {
        currEnergy = energy;
        energyText.text = currEnergy + "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

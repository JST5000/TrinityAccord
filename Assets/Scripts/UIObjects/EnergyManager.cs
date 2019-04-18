using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{

    private int currEnergy;
    public Text energyText;

    private Color originalColor;
    private Color darkenedColor;
    private bool darkened = false;

    // Start is called before the first frame update
    void Start()
    {
        float dim = 0.5f;
        Image img = GetComponent<Image>();
        originalColor = new Color(img.color.r, img.color.g, img.color.b);
        darkenedColor = new Color(img.color.r * dim, img.color.g * dim, img.color.b * dim, 1);
    }

    public void SetEnergy(int energy)
    {
        currEnergy = energy;
        energyText.text = currEnergy + "";
        if (currEnergy == 0)
        {
            GetComponent<Image>().color = darkenedColor;
            darkened = true;
        }
        else if (darkened)
        {
            GetComponent<Image>().color = originalColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

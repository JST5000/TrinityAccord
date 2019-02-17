using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private int currHealth;
    public int maxHealth;
    public Image topLayer;
    public Text display;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetCurrHealth(int curr)
    {
        currHealth = curr;
        UpdateUI();
    }

    public void SetMaxHealth(int max)
    {
        maxHealth = max;
        UpdateUI();
    }

    private void UpdateUI()
    {
        string healthMessage = currHealth + " / " + maxHealth;
        display.text = healthMessage;
        //Scales the green overlay down to the correct %hp
        float scale = 1;
        if (maxHealth != 0)
        {
            if (currHealth <= 0)
            {
                scale = 0;
            }
            else
            {
                scale = (1f * currHealth) / maxHealth;
            }
        }
        topLayer.transform.localScale = new Vector3(scale, topLayer.transform.localScale.y, topLayer.transform.localScale.z);
    }
}

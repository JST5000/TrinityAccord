using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public GameObject life; //AKA Stagger

    //Invalid for negative lives.
    public void SetLives(int lives)
    {
        int deltaLives = lives - GetLives();
        if (deltaLives > 0)
        {
            for (int i = 0; i < deltaLives; ++i)
            {
                Instantiate<GameObject>(life, transform);
            }
        } else if (deltaLives < 0)
        {
            for (int i = 0; i < -deltaLives; ++i)
            { 
                Debug.Log("Removing a life.");
                Debug.Log(transform.GetChild(0).name);
                Destroy(transform.GetChild(0).gameObject);
            }
        }
    }

    public int GetLives()
    {
        return transform.childCount;
    }

    public void DecrementLives()
    {
        int lives = GetLives();
        if (lives > 0)
        {
            SetLives(lives - 1);
        }
    }
}

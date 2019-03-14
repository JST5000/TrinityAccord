using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to interpret CSV file

public class EncounterData 
{
    
    private int level;
    private int difficulty;
    private string damage;
    private string encounter;

    public int Level { get => level; set => level = value; }
    public int Difficulty { get => difficulty; set => difficulty = value; }
    public string Damage { get => damage; set => damage = value; }
    public string Encounter { get => encounter; set => encounter = value; }

    public void DebugAll()
    {
        Debug.Log(Level);
        Debug.Log(Difficulty);
        Debug.Log(Damage);
        Debug.Log(Encounter);
    }
}

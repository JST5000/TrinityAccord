using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to interpret CSV file

public class EncounterData 
{
    
    private int level;
    //Difficulty of 0 means unset
    private int difficulty;
    private string damage;
    private string encounter;

    public int Level { get => level; set => level = value; }
    //Difficulty of 0 means unset
    public int Difficulty { get => difficulty; set => difficulty = value; }
    public string Damage { get => damage; set => damage = value; }
    public string Encounter { get => encounter; set => encounter = value; }

    //Header for the CSV file, should correspond with the order used in WriteCSVLine()
    public static string GetCSVFieldNames()
    {
        return "Level,Difficulty,Damage,Encounter";
    }

    public void AppendDamage(int dmg)
    {
        if(damage != "")
        {
            damage += ",";
        }
        damage += dmg;
    }

    public string WriteCSVLine()
    {
        string line = "";
        line += Quote(level);
        line += ",";
        //Difficulty == 0 implies no difficulty was set
        if (difficulty != 0)
        {
            line += Quote(difficulty);
        }
        line += ",";
        line += Quote(damage);
        line += ",";
        line += Quote(encounter);
        return line;
    }

    public string Quote(object o)
    {
        return "\"" + o.ToString() + "\"";
    }

    public void DebugAll()
    {
        Debug.Log(Level);
        Debug.Log(Difficulty);
        Debug.Log(Damage);
        Debug.Log(Encounter);
    }
}

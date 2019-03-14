using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class CsvInterpreter : MonoBehaviour
{
    public void ReadToTemplate<T>(List<T> templateList, string path)
    {
        foreach (T template in templateList) {

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Debug.Log(property.Name);
                if (property.DeclaringType.Equals(typeof(int)))
                {
                    Debug.Log("Found an int! (Name: " + property.Name + ")");
                }
                else if (property.DeclaringType.Equals(typeof(string))) {
                    Debug.Log("Found a string! (Name: " + property.Name + ")");
                    property.SetValue(template, "10");
                } else
                {

                }
            }
        }
    }

    private void Start()
    {
        string csvFilePath = "Assets\\Resources\\Data\\Encounters.csv";
        EncounterData data = new EncounterData();
        List<EncounterData> dataList = new List<EncounterData>();
        dataList.Add(data);
        ReadToTemplate<EncounterData>(dataList, csvFilePath);
    }
}

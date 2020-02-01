using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOpener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PermanentState.Wins % 2 == 0 )//&& PermanentState.Wins != 0)
        {
            GameObject instance = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UpgradeUI"));
            instance.transform.SetParent(GameObject.Find("Canvas").transform, false);
            instance.transform.position = new Vector3(0, 0, 0);
            instance.transform.localScale = instance.transform.localScale * .8f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftClassCard : MonoBehaviour
{
    private static object lockObj = new object();

    public Transform DraftCardMenu;

    // Start is called before the first frame update
    void Start()
    {
        lock (lockObj)
        {
            if (!PermanentState.HasDraftedClassCard)
            {
                PermanentState.HasDraftedClassCard = true;
                GameUI.SetVisibilityOfGameUI(false);
                InstantiateChooseClass();
            }
        }
    }

    private void InstantiateChooseClass()
    {
        Transform instance = Instantiate(DraftCardMenu, GameObject.Find("Canvas").transform, false);
        CardData[] classCards = { new Berserk(), new Chaos(), new Sharpen(), new Juggle() };
        Choose3Manager choose3Manager = instance.GetComponent<Choose3Manager>();
        choose3Manager.SetTitle("Select your class!");
        choose3Manager.Init(classCards);
        choose3Manager.DisableCardBuffText();
        choose3Manager.reloadEncounterOnDraft = true;
        choose3Manager.PauseGameInteraction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

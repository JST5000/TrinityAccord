using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateEnemyUI : MonoBehaviour
{
    public Text nameDisplay;
    public HealthManager hpManager;
    public LifeManager livesHolder;
    public Image enemyPicture;
    public Text timerDisplay;
    public Text effectDisplay;

    // Start is called before the first frame update
    void Start()
    {
        //Test data
        UIEnemyData testEnemy = new UIEnemyData("Test Enemy", 7, 3, 2, 2, "Deal 2 damage.");
        PopulateUI(testEnemy);
    }

    public void PopulateUI(UIEnemyData data)
    {
        nameDisplay.text = data.enemyName;
        hpManager.SetMaxHealth(data.HP);
        hpManager.SetCurrHealth(data.HP);
        livesHolder.SetLives(data.staggers);
        timerDisplay.text = data.timer + "/" + data.timer;
        effectDisplay.text = data.effect;
        //TODO populate the image field
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

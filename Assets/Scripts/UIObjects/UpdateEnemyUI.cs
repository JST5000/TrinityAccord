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
    public Image timerBackground;
    public Text timerDisplay;
    public Text effectDisplay;

    private EnemyManager enemyHolder;

    private bool isDisabled = false;
    public CanvasGroup enemyCG;

    // Start is called before the first frame update
    void Start()
    {
        enemyHolder = GetComponent<EnemyManager>();
        //Test data
        UIEnemyData testEnemy = new UIEnemyData("Test Enemy", currHP: 7, maxHP: 7, staggers: 3, damage: 2, maxTimer: 2, currTimer: 2, effect: "Deal 2 damage.");
        PopulateUI(testEnemy);
    }

    public void PopulateUI(UIEnemyData data)
    {
        nameDisplay.text = data.EnemyName;
        hpManager.SetMaxHealth(data.MaxHP);
        hpManager.SetCurrHealth(data.CurrHP);
        livesHolder.SetLives(data.Staggers);
        timerDisplay.text = data.CurrTimer + "/" + data.MaxTimer;
        effectDisplay.text = data.Effect;
        //TODO populate the image field
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyHolder.IsEmpty())
        {
            UpdateUI(enemyHolder.GetUIData());
            EnableCard();
        }
        else
        {
            DisableCard();
        }
    }

    //Checks if change is needed, then updates
    public void DisableCard()
    {
        if (!isDisabled)
        {
            enemyCG.alpha = 0;
            enemyCG.blocksRaycasts = false;
            enemyCG.interactable = false;
            isDisabled = true;
        }
    }

    //Checks if change is needed, then updates
    public void EnableCard()
    {
        if (isDisabled)
        {
            enemyCG.alpha = 1;
            enemyCG.blocksRaycasts = true;
            enemyCG.interactable = true;
            isDisabled = false;
        }
    }

    public void UpdateUI(UIEnemyData data)
    {
        nameDisplay.text = data.EnemyName;

        hpManager.SetCurrHealth(data.CurrHP);
        hpManager.SetMaxHealth(data.MaxHP);

        livesHolder.SetLives(data.Staggers);

        //Enemy Background Image is unchanged

        SetTimerColor(data);
        timerDisplay.text = data.CurrTimer + " / " + data.MaxTimer;

        effectDisplay.text = data.Effect;
    }

    public void SetTimerColor(UIEnemyData data)
    {
        if (data.CurrTimer >= data.MaxTimer)
        {
            timerBackground.color = Color.green;
        }
        else if (data.CurrTimer > 1)
        {
            timerBackground.color = Color.yellow;
        }
        else
        {
            timerBackground.color = Color.red;
        }
    }
}

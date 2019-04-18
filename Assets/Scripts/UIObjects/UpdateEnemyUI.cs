using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateEnemyUI : MonoBehaviour
{
    public Text nameDisplay;
    public HealthManager hpManager;
    public LifeManager livesHolder;
    public Image enemyPicture;
    public Image timerBackground;
    public Text timerDisplay;
    public TextMeshProUGUI effectDisplay;
    public TextMeshProUGUI stunnedText;

    private EnemyManager enemyHolder;

    private bool isDisabled = false;
    public CanvasGroup enemyCG;

    // Start is called before the first frame update
    void Start()
    {
        enemyHolder = GetComponent<EnemyManager>();
        DisableEnemy();
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
            EnableEnemy();
        }
        else
        {
            DisableEnemy();
        }
    }

    //Checks if change is needed, then updates
    public void DisableEnemy()
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
    public void EnableEnemy()
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

        enemyPicture.sprite = data.Picture;

        SetTimerColor(data);
        timerDisplay.text = data.CurrTimer + " / " + data.MaxTimer;

        SetStunnedText(data);

        effectDisplay.text = data.Effect;
    }

    public void SetTimerColor(UIEnemyData data)
    {
        if(data.CurrTimer == 1)
        {
            timerBackground.color = Color.red;
        } else if (data.CurrTimer == 2 || data.CurrTimer < data.MaxTimer )
        {
            timerBackground.color = Color.yellow;
        } else if (data.CurrTimer >= data.MaxTimer)
        {
            timerBackground.color = Color.green;
        }

        if(data.Stunned)
        {
            Color c = timerBackground.color;
            float dim = .6f;
            timerBackground.color = new Color(c.r * dim, c.g * dim, c.b * dim, c.a);
        }
    }

    public void SetStunnedText(UIEnemyData data)
    {
        if(data.Stunned)
        {
            stunnedText.text = "Stunned!";
        } else
        {
            stunnedText.text = "";
        }
    }
}

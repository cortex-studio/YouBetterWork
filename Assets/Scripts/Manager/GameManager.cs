using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TapToStartHelper tapToStartHelper;
    [SerializeField] private Image playerBar;
    [SerializeField] private GameObject playerScoreBG;
    [SerializeField] private TextMeshProUGUI playerWinLevelText;
    [SerializeField] private TextMeshProUGUI enemyWinLevelText;
    [SerializeField] private TextMeshProUGUI playerLoseLevelText;
    [SerializeField] private TextMeshProUGUI enemyLoseLevelText;
    [SerializeField] private Color greenColor;
    [SerializeField] private Color redColor;
    [SerializeField] private Image timerBar;
    [SerializeField] private VoidEvent enemyScoreReset;
    [SerializeField] private VoidEvent playerScoreReset;
    [SerializeField] private List<string> promotionList = new List<string>();
    [SerializeField] private TextMeshProUGUI promotionTextPlayer;
    [SerializeField] private TextMeshProUGUI promotionTextEnemy;
    [SerializeField] private VoidEvent staminaResetEvent;
    [SerializeField] private VoidEvent promotionSystemEvent;
    [SerializeField] private GameObject moneyUI;
    [SerializeField] private GameObject promotionUi;
    [SerializeField] private VoidEvent enemySelect;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject matchUIPanel;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private IntEvent cameraChangeEvent;
    [SerializeField] private BoolEvent ınputManager;
    [SerializeField] private VoidEvent buttonClose;
    [SerializeField] private BoolEvent isMatchForEarnMoney;
    [SerializeField] private IntVariable enemyScore;
    [SerializeField] private IntVariable playerScore;
    private float matchUITimer;
    private float matchTimer = 30f;
    public bool isWorking;
    private bool isMatch;
    private bool isStartWar;
    public bool isUIController;

    private void Start()
    {
        RandomTimer();
    }

    private void Update()
    {
        
        if (isMatch)
        {
            if (Input.GetMouseButtonDown(0) && isUIController)
            {
                isWorking = true;
                tapToStartHelper.WarSceneTapToWar();
                isUIController = false;
            }
            if (!isStartWar)
                return;
            matchTimer -= Time.deltaTime;
            Timer();
            if (matchTimer <= 0)
            {
                playerBar.fillAmount = 0;
                playerScoreBG.SetActive(false);
                staminaResetEvent.Raise();
                ınputManager.Raise(false);
                isMatchForEarnMoney.Raise(false);
                cameraChangeEvent.Raise(1);
                if (enemyScore.Value > playerScore.Value)
                {
                    playerLoseLevelText.text = promotionList[playerData.PromotionLevel];
                    enemyLoseLevelText.text = promotionList[playerData.PromotionLevel + 1];
                    enemyManager.EnemyFinish(true);
                    losePanel.SetActive(true);
                    enemyManager.IsFight(false);
                }
                else
                {
                    enemyManager.EnemyFinish(false);
                    playerData.PromotionLevel += 1;
                    playerWinLevelText.text = promotionList[playerData.PromotionLevel];
                    enemyWinLevelText.text = promotionList[playerData.PromotionLevel - 1];
                    
                    promotionSystemEvent.Raise();
                    winPanel.SetActive(true);
                    enemyManager.IsFight(false);
                }

                isWorking = false;
                isStartWar = false;
                isMatch = false;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !isUIController && isWorking)
            {
                tapToStartHelper.WorkSceneTapToWork();
                isUIController = true;
            }
        }
    }

    public void VSScene()
    {
        playerBar.fillAmount = 0;
        enemyScoreReset.Raise();
        playerScoreReset.Raise();
        staminaResetEvent.Raise();
        moneyUI.SetActive(false);
        promotionUi.SetActive(false);
        enemySelect.Raise();
        ınputManager.Raise(false);
        cameraChangeEvent.Raise(1);
        RandomTimer();
        StartCoroutine(OpenMatchUI());
        buttonClose.Raise();
        isWorking = false;
    }

    public void IsStartWar(bool value)
    {
        isStartWar = value;
    }

    public void IsWorking(bool value)
    {
        isWorking = value;
    }

    public void IsMatch(bool value)
    {
        playerScore.Value = 0;
        isMatch = value;
    }

    private void RandomTimer()
    {
        matchUITimer = Random.Range(15, 20);
        matchTimer = 30;
    }

    private IEnumerator OpenMatchUI()
    {
        yield return new WaitForSeconds(0.6f);
        promotionTextEnemy.text = promotionList[playerData.PromotionLevel];
        promotionTextPlayer.text = promotionList[playerData.PromotionLevel];
        matchUIPanel.SetActive(true);
    }

    private void Timer()
    {
        float value = matchTimer / 30f;
        timerBar.fillAmount = Mathf.Lerp(timerBar.fillAmount, value, Time.deltaTime);
        timerBar.color = Color.Lerp(timerBar.color, redColor, Time.deltaTime / 25);
    }

    public void TimerReset()
    {
        timerBar.fillAmount = 1;
        timerBar.color = greenColor;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private TapToStartHelper tapToHelper;
    [SerializeField] private Transform fileFinishPose;
    [SerializeField] private Transform instanceFilePose;
    [SerializeField] private List<Transform> instanceFiles = new List<Transform>();
    [SerializeField] private Image screenEffectUi;
    [SerializeField] private Image screenEffectUiWarr;
    [SerializeField] private Image screenEffectUiRed;
    [SerializeField] private Image screenEffectUiWarrRed;
    [SerializeField] private ParticleSystem writingParticle;
    [SerializeField] private ParticleSystem writingParticleWar;
    [SerializeField] private GameObject particle1;
    [SerializeField] private GameObject particle2;
    [SerializeField] private TextMeshProUGUI promotionText;
    [SerializeField] private List<string> promotionList = new List<string>();
    [SerializeField] private Image perfectGreenBardEffect;
    [SerializeField] private Material characterMaterial;
    [SerializeField] private VoidEvent moneyFx;
    [SerializeField] private TextMeshProUGUI playerMoneyText;
    [SerializeField] private Color greenColor;
    [SerializeField] private Color redColor;
    [SerializeField] private Animator tpsPlayerAnim;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private VoidEvent perfectEvent;
    [SerializeField] private VoidEvent moneyAnimEvent;
    [SerializeField] private IntEvent moneyTextEvent;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private RectTransform greenArea;
    [SerializeField] private GameObject playerBar;
    [SerializeField] private Image playerBarImage;
    [SerializeField] private Image staminaBar;
    [SerializeField] private Image staminaBarVersus;
    [SerializeField] private VoidEvent buttonCheck;
    [SerializeField] private IntVariable playerScore;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    private float inputTime;
    private bool inputManager = true;
    private float fillValue;
    private float greenMin;
    private float greenMax;
    private bool staminaController = false;
    private float staminaValue = 1f;
    private bool isMatch;
    private bool colorChange;
    private float inputMultiplier = 1f;
    public bool isUIOverride { get; private set; }

    private void Start()
    {
        PromotionSystem();
        DOTween.To(() => playerMoneyText.text, x => playerMoneyText.text = x, "$" + playerData.PlayerMoney.ToString(),
            1);
    }

    private void Update()
    {
        isUIOverride = EventSystem.current.IsPointerOverGameObject();
        if (!inputManager)
        {
            playerBar.SetActive(false);
            writingParticle.Pause();
            return;
        }

        if (Input.GetMouseButtonDown(0) && !isUIOverride)
        {
            //tapToHelper.WorkSceneTapToWork();
            writingParticle.Play();
            writingParticleWar.Play();
            staminaController = true;
            playerBar.SetActive(true);
            tpsPlayerAnim.SetBool("Work", true);
            playerAnim.SetBool("Work", true);
            GreenMinMaxCheck();
        }

        if (Input.GetMouseButton(0))
        {
            inputTime += Time.deltaTime * inputMultiplier;
            BarFiller(inputTime);
            StaminaController();
        }

        if (Input.GetMouseButtonUp(0))
        {
            writingParticle.Pause();
            writingParticleWar.Pause();
            playerAnim.SetTrigger("Hit");
            tpsPlayerAnim.SetBool("Hit", true);
            playerAnim.SetBool("Work", false);

            StartCoroutine(AnimDelay());

            staminaValue -= ((1f - playerData.Stamina / 200f) / 4);
            fillValue = playerBarImage.fillAmount;
            GreenAreaController(fillValue);
            playerBar.SetActive(false);
            playerBarImage.fillAmount = 0;
            staminaController = false;
            inputTime = 0;
        }

        if (staminaBar.fillAmount < 0.3f)
        {
            GreenMinMaxCheck();
            inputMultiplier = staminaBar.fillAmount + 0.3f;
            writingParticle.Pause();
            writingParticleWar.Pause();
            tpsPlayerAnim.speed = inputMultiplier - 0.1f;
            playerAnim.speed = inputMultiplier - 0.1f;
            greenArea.sizeDelta = new Vector2(80,
                Mathf.Lerp(greenArea.sizeDelta.y, 400 * (staminaBar.fillAmount / 0.3f), Time.deltaTime * 2));
            staminaBar.color = Color.Lerp(staminaBar.color, redColor, Time.deltaTime * 2);
            particle1.SetActive(true);
            particle2.SetActive(true);
            if (!colorChange)
            {
                screenEffectUiRed.transform.DOScale(new Vector3(1f, 1f, 1.3f), 0.3f).SetEase(Ease.OutBounce)
                    .SetLoops(-1, LoopType.Yoyo).SetId("screeneffect");
                screenEffectUiRed.DOFade(1, 0.2f).SetId("screeneffectFade");
                screenEffectUiWarrRed.transform.DOScale(new Vector3(1f, 1f, 1.3f), 0.3f).SetEase(Ease.OutBounce)
                    .SetLoops(-1, LoopType.Yoyo).SetId("screeneffectwar");
                screenEffectUiWarrRed.DOFade(1, 0.2f).SetId("screeneffectwarFade");

                characterMaterial.DOColor(redColor, 1f);
                colorChange = true;
            }
        }
        else
        {
            inputMultiplier = 1;
            if (colorChange)
            {
                DOTween.Kill("screeneffect");
                DOTween.Kill("screeneffectFade");
                DOTween.Kill("screeneffectwar");
                DOTween.Kill("screeneffectwarFade");
                screenEffectUiRed.transform.DOScale(new Vector3(0.95f, 0.58f, 0), 0.2f);
                screenEffectUiRed.DOFade(0, 0.2f);
                screenEffectUiWarrRed.transform.DOScale(new Vector3(0.95f, 0.58f, 0), 0.2f);
                screenEffectUiWarrRed.DOFade(0, 0.2f);
                characterMaterial.DOColor(Color.white, 1f);
                colorChange = false;
            }

            tpsPlayerAnim.speed = inputMultiplier;
            playerAnim.speed = inputMultiplier;
            greenArea.sizeDelta = new Vector2(80, Mathf.Lerp(greenArea.sizeDelta.y, 400, Time.deltaTime * 5));
            staminaBar.color = Color.Lerp(staminaBar.color, greenColor, Time.deltaTime * 5);

            //characterMaterial.color = Color.Lerp(characterMaterial.color, Color.white, Time.deltaTime * 5);
            particle1.SetActive(false);
            particle2.SetActive(false);
        }

        if (!staminaController)
        {
            //staminaValue += Time.deltaTime / ((1 - playerData.Stamina / 250f) * (playerData.Stamina / 250));
            staminaValue += Time.deltaTime / (100f / playerData.Stamina);
            //Debug.Log(VAR);
        }

        if (staminaValue >= 1)
        {
            staminaValue = 1;
        }

        if (staminaValue <= 0.05f)
        {
            staminaValue = 0.05f;
        }

        StaminaBarFiller();
    }

    private IEnumerator AnimDelay()
    {
        yield return new WaitForSeconds(0.5f);
        tpsPlayerAnim.SetBool("Hit", false);
        tpsPlayerAnim.SetBool("Work", false);
    }

    private void BarFiller(float value)
    {
        playerBarImage.fillAmount = value;
        if (playerBarImage.fillAmount == 1)
        {
            playerBarImage.fillAmount = 0;
            inputTime = 0;
        }
    }

    public void InputManager(bool value)
    {
        inputManager = value;
    }

    private void GreenMinMaxCheck()
    {
        greenMin = (150 + greenArea.rect.height / 2) - (40 * greenArea.rect.height / 400);
        //Debug.Log("Green Min = " + greenMin);
        greenMax = (150 + greenArea.rect.height / 2) + (20 * greenArea.rect.height / 400);
        //Debug.Log("Green Max = " + greenMax);
    }


    private void GreenAreaController(float value)
    {
        float playerLastValue = 0;
        playerLastValue = 700 - 700 * value;
        if (greenMin < playerLastValue && playerLastValue < greenMax)
        {
            if (!isMatch)
            {
                playerData.PlayerMoney += playerData.Income * 2;
                moneyTextEvent.Raise(playerData.Income * 2);
                moneyFx.Raise();
                DOTween.To(() => playerMoneyText.text, x => playerMoneyText.text = x,
                    "$" + playerData.PlayerMoney.ToString(), 1);
                moneyAnimEvent.Raise();
                buttonCheck.Raise();
            }
            else
            {
                playerScore.Value += 1;
                playerScoreText.text = playerScore.Value.ToString();
                FileInstance(playerScore.Value);
            }

            PerfectFadeOutEffect();
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
            perfectEvent.Raise();
        }

        if (150 < playerLastValue && playerLastValue < greenMin ||
            greenMax < playerLastValue && playerLastValue < 150 + greenArea.rect.height)
        {
            int rnd = Random.Range(0, 100);
            if (rnd < playerData.Accuracy)
            {
                if (!isMatch)
                {
                    playerData.PlayerMoney += playerData.Income;
                    moneyTextEvent.Raise(playerData.Income);
                    DOTween.To(() => playerMoneyText.text, x => playerMoneyText.text = x,
                        "$" + playerData.PlayerMoney, 1);
                    moneyAnimEvent.Raise();
                    buttonCheck.Raise();
                    moneyFx.Raise();
                }
                else
                {
                    playerScore.Value += 1;
                    playerScoreText.text = playerScore.Value.ToString();
                    FileInstance(playerScore.Value);
                }

                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            }
        }
    }

    public void IsMatch(bool value)
    {
        isMatch = value;
    }

    private void StaminaController()
    {
        float value = 0;
        if (playerData.Stamina < 75)
        {
            value = 4.5f - ((playerData.Stamina - 50) * 0.02f);
        }
        else
        {
            value = 4;
        }

        staminaValue -= Time.deltaTime / (value - 150f / playerData.Stamina) / 5.5f;
    }

    private void StaminaBarFiller()
    {
        DOTween.To(() => staminaBar.fillAmount, x => staminaBar.fillAmount = x,
            staminaValue / 1f, 0.5f);
        DOTween.To(() => staminaBarVersus.fillAmount, x => staminaBarVersus.fillAmount = x,
            staminaValue / 1f, 0.5f);
        //staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, staminaValue / 1f,
           // Time.deltaTime * (3f - playerData.Stamina / 250f));
        //staminaBarVersus.fillAmount = Mathf.Lerp(staminaBarVersus.fillAmount, staminaValue / 1f,
           // Time.deltaTime * (3f - playerData.Stamina / 250f));
    }

    public void AddMoney(int value)
    {
        playerData.PlayerMoney += value;
        DOTween.To(() => playerMoneyText.text, x => playerMoneyText.text = x,
            "$" + playerData.PlayerMoney.ToString(), 1);
    }

    private void PerfectFadeOutEffect()
    {
        perfectGreenBardEffect.DOFade(1, 0.2f)
            .OnComplete(() => { perfectGreenBardEffect.DOFade(0, 0.5f); });
        screenEffectUi.transform.DOScale(new Vector3(1f, 1f, 1.3f), 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            screenEffectUi.transform.DOScale(new Vector3(0.95f, 0.58f, 0), 0.3f).SetDelay(0.2f);
        });

        screenEffectUi.DOFade(1, 0.2f)
            .OnComplete(() => { screenEffectUi.DOFade(0, 0.5f); });
        screenEffectUiWarr.transform.DOScale(new Vector3(1f, 1f, 1.3f), 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            screenEffectUiWarr.transform.DOScale(new Vector3(0.95f, 0.58f, 0), 0.3f).SetDelay(0.2f);
        });

        screenEffectUiWarr.DOFade(1, 0.2f)
            .OnComplete(() => { screenEffectUiWarr.DOFade(0, 0.5f); });
    }

    public void PromotionSystem()
    {
        if (playerData.PromotionLevel >= 10)
        {
            playerData.PromotionLevel = 10;
        }

        promotionText.text = promotionList[playerData.PromotionLevel];
    }

    public void StaminaReset()
    {
        staminaValue = 1;
        greenArea.sizeDelta = new Vector2(80, Mathf.Lerp(greenArea.sizeDelta.y, 400, 0));
        staminaBar.color = greenColor;
        characterMaterial.color = Color.white;
        playerBarImage.fillAmount = 0;
    }

    public void PlayerScoreReset()
    {
        playerScore.Value = 0;
        playerScoreText.text = playerScore.Value.ToString();
    }

    private void FileInstance(int value)
    {
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        instanceFiles[value - 1].transform.position = instanceFilePose.position;
        instanceFiles[value - 1].GetComponent<Rigidbody>().isKinematic = false;
    }

    public void FilePoseReset()
    {
        for (int i = 0; i < instanceFiles.Count; i++)
        {
            instanceFiles[i].GetComponent<Rigidbody>().isKinematic = true;
            instanceFiles[i].position = fileFinishPose.position;
        }
    }
}
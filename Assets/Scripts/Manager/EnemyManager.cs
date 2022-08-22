using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyParticle;
    [SerializeField] private IntEvent cameraChangeEvent;
    [SerializeField] private Color redColor;
    [SerializeField] private List<Material> enemyMaterials = new List<Material>();
    private Material enemyMat;
    [SerializeField] private Transform fileFinishPose;
    [SerializeField] private Transform instanceFilePose;
    [SerializeField] private List<Transform> instanceFiles = new List<Transform>();
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TextMeshProUGUI playerStamina;
    [SerializeField] private TextMeshProUGUI playerAccuracy;
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private TextMeshProUGUI enemyStaminaText;
    [SerializeField] private TextMeshProUGUI enemyAccuracyText;
    [SerializeField] private Animator playerMatchUıAnim;
    [SerializeField] private List<GameObject> matchUıEnemies = new List<GameObject>();
    [SerializeField] private List<GameObject> matchEnemies = new List<GameObject>();
    [SerializeField] private int enemyPerfectChange;
    [SerializeField] private int enemyAccuracy;
    [SerializeField] private IntVariable enemyScore;
    [SerializeField] private float workTime;
    [SerializeField] private TextMeshProUGUI enemyScoreText;
    private Animator enemyAnim;
    private float inGameTime;
    private bool isFight;
    private float rndTime=3f;

    private void Start()
    {
        inGameTime = workTime;
    }

    private void Update()
    {
        if (!isFight)
        {
            return;
        }

        rndTime -= Time.deltaTime;
        
        if (rndTime <=0)
        {
            StartCoroutine(EnemyColorChangeSystem());
            rndTime = Random.Range(3f , 5f);
        }

        inGameTime -= Time.deltaTime;

        if (inGameTime < 0)
        {
            int rnd = Random.Range(0, 100);
            if (rnd < enemyData.PerfectAccuracy)
            {
                enemyScore.Value += 1;
                enemyScoreText.text = enemyScore.Value.ToString();
                inGameTime = workTime;
                FileInstance(enemyScore.Value);
                
            }
            else
            {
                int rndAccuracy = Random.Range(0, 100);
                if (rndAccuracy <= enemyData.Accuracy)
                {
                    enemyScore.Value += 1;
                    enemyScoreText.text = enemyScore.Value.ToString();
                    inGameTime = workTime;
                    FileInstance(enemyScore.Value);
                }
                else
                {
                    inGameTime = workTime;
                }
            }
        }
    }

    public void EnemyScoreReset()
    {
        enemyScore.Value = 0;
        enemyScoreText.text = enemyScore.Value.ToString();
        for (int i = 0; i < enemyMaterials.Count; i++)
        {
            enemyMaterials[i].color = Color.white;
        }
    }

    public void ChoseEnemy()
    {
        enemyData.Accuracy = Random.Range(40, playerData.Accuracy);
        enemyData.Stamina = Random.Range(30, (int) playerData.Stamina);
        int hardValue = 0; 
        if (playerData.Accuracy > 100)
        {
            hardValue = 60;
        }
        else
        {
            hardValue = 40;
        }
        enemyData.PerfectAccuracy = Random.Range(20, hardValue);
        enemyStaminaText.text = enemyData.Stamina.ToString();
        enemyAccuracyText.text = enemyData.Accuracy.ToString();
        playerStamina.text = playerData.Stamina.ToString();
        playerAccuracy.text = playerData.Accuracy.ToString();
        int rndEnemy = Random.Range(0, matchEnemies.Count);
        int rndPlayer = 0;
        int rndAnim = 0;

        for (int i = 0; i < matchEnemies.Count; i++)
        {
            matchEnemies[i].SetActive(false);
            matchUıEnemies[i].SetActive(false);
            if (i == rndEnemy)
            {
                
                matchEnemies[rndEnemy].SetActive(true);
                matchUıEnemies[rndEnemy].SetActive(true);
                enemyMat = enemyMaterials[rndEnemy];
                rndAnim = Random.Range(1, 10);
                enemyAnim = matchUıEnemies[rndEnemy].GetComponent<Animator>();
                enemyAnim.SetBool("Back",false);
                enemyAnim.SetInteger("dance", rndAnim);
            }
        }

        for (int i = 0; i < 12; i++)
        {
            rndPlayer = Random.Range(1, 10);
            if (rndPlayer != rndAnim)
            {
                playerMatchUıAnim.SetBool("Back",false);
                playerMatchUıAnim.SetInteger("dance", rndPlayer);
                break;
            }
        }
    }


    public void IsFight(bool value)
    {
        isFight = value;
    }

    private void FileInstance(int value)
    {
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

    private IEnumerator EnemyColorChangeSystem()
    {
        enemyParticle.SetActive(true);
        enemyMat.DOColor(redColor, 0.7f);
        yield return new WaitForSeconds(0.7f);
        enemyMat.DOColor(Color.white, 0.5f).OnComplete(() =>
        {
            enemyParticle.SetActive(false);
        });
    }

    public void EnemyFinish(bool isWin)
    {
        enemyAnim.SetBool("Back",true);
        enemyAnim.SetInteger("dance",0);
        playerMatchUıAnim.SetBool("Back",true);
        playerMatchUıAnim.SetInteger("dance",0);
        if (isWin)
        {
          enemyAnim.SetTrigger("Win");  
          playerMatchUıAnim.SetTrigger("Lose");
        }
        else
        {
            enemyAnim.SetTrigger("Lose");  
            playerMatchUıAnim.SetTrigger("Win");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PromotionWarButtonHepler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private IntEvent cameraChange;
    [SerializeField] private BoolEvent inputmanagerEvent;
    [SerializeField] private BoolEvent isMatchEvent;
    [SerializeField] private BoolEvent isMatchForEarnMoneyEvent;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private GameObject playerScoreBG;
    [SerializeField] private PlayerInput playerınput;
    [SerializeField] private GameObject matchStartScene;


    public void ButtonInterract()
    {
        cameraChange.Raise(2);
        gameManager.TimerReset();
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        inputmanagerEvent.Raise(true);
        isMatchEvent.Raise(true);
        playerınput.IsMatch(true);
        matchStartScene.SetActive(true);
        isMatchForEarnMoneyEvent.Raise(true);
        playerScoreBG.SetActive(true);
        
    }
}

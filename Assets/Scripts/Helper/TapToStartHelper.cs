using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class TapToStartHelper : MonoBehaviour
{
    [SerializeField] private VoidEvent onLevelStart;

    [SerializeField] private GameObject income;
    [SerializeField] private GameObject stamina;
    [SerializeField] private GameObject accuracy;
    [SerializeField] private BoolEvent isWorkingEvent;
    [SerializeField] private GameObject warButton;

    [SerializeField] private GameManager isStartWar;
    [SerializeField] private EnemyManager isFight;
    [SerializeField] private GameObject warTaptoStart;




    public void WorkSceneTapToWork()
    {
        onLevelStart.Raise();
        warButton.SetActive(true);
        income.SetActive(true);
        stamina.SetActive(true);
        accuracy.SetActive(true);
        isWorkingEvent.Raise(true);
    }


    public void WarSceneTapToWar()
    {
        isStartWar.IsStartWar(true);
        isFight.IsFight(true);
        warTaptoStart.SetActive(false);
    }
}
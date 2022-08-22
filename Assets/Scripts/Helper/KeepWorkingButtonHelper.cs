using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class KeepWorkingButtonHelper : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private IntEvent cameraChange;
    [SerializeField] private BoolEvent inputmanagerEvent;
    [SerializeField] private BoolEvent isWorkingEvent;
    [SerializeField] private List<GameObject> buttons = new List<GameObject>();
    [SerializeField] private GameObject tapToWork;
    [SerializeField] private GameObject moneyUi;
    [SerializeField] private GameObject promotionUi;
    [SerializeField] private VoidEvent textReset;


    public void ButtonActive()
    {
        cameraChange.Raise(0);
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        textReset.Raise();
        moneyUi.SetActive(true);
        promotionUi.SetActive(true);
        inputmanagerEvent.Raise(true);
        isWorkingEvent.Raise(true);
        tapToWork.SetActive(true);
        gameManager.isUIController = false;
    }
}

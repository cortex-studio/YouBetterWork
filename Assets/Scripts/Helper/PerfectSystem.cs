using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PerfectSystem : MonoBehaviour
{
    [SerializeField] private Transform perfectText;
    [SerializeField] private Transform moneyTextTransform;
    [SerializeField] private TextMeshProUGUI moneyText;

    public void Perfect()
    {
        perfectText.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.5f).SetEase(Ease.OutBounce)
            .OnComplete(() => { perfectText.DOScale(new Vector3(0, 0, 0), 0.3f).SetDelay(0.2f); });
    }

    public void MoneyText()
    {
        moneyTextTransform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.5f).SetEase(Ease.OutBounce)
            .OnComplete(() => { moneyTextTransform.DOScale(new Vector3(0, 0, 0), 0.3f).SetDelay(0.2f); });
    }

    public void SetMoneyText(int value)
    {
        moneyText.text = "+$" + value;
    }
}
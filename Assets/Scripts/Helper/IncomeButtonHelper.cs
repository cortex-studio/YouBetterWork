using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class IncomeButtonHelper : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private int upgradeValue;
    [SerializeField] private int upgradeMoneyValue;
    [SerializeField] private Button button;
    [SerializeField] private VoidEvent buttonCheck;

    private void Start()
    {
        ButtonCheck();
        StartValues();
    }

    public void ButtonCheck()
    {
        if (playerData.PlayerMoney < playerData.incomeLevel * upgradeMoneyValue + 85)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    public void ButtonInteract()
    {
        playerData.PlayerMoney -= playerData.incomeLevel * upgradeMoneyValue + 85;
        playerData.incomeLevel += 1;
        playerData.Income += 1;
        moneyText.text = (playerData.incomeLevel * upgradeMoneyValue + 85).ToString();
        valueText.text = "$" + (playerData.Income).ToString();
        buttonCheck.Raise();
    }

    public void ButtonClose()
    {
        gameObject.SetActive(false);
    }


    private void StartValues()
    {
        moneyText.text = (playerData.incomeLevel * upgradeMoneyValue + 85).ToString();
        valueText.text = "$" + (playerData.Income).ToString();
    }
}
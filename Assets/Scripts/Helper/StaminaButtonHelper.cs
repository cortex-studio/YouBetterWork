using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class StaminaButtonHelper : MonoBehaviour
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
        if (playerData.PlayerMoney < playerData.staminaLevel * upgradeMoneyValue + 85)
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
        playerData.PlayerMoney -= playerData.staminaLevel * upgradeMoneyValue + 85;
        playerData.staminaLevel += 1;
        playerData.Stamina += 1;
        moneyText.text = (playerData.staminaLevel * upgradeMoneyValue + 85).ToString();
        double value = Math.Round(playerData.Stamina, 1);
        valueText.text = (value).ToString();
        buttonCheck.Raise();
    }

    public void ButtonClose()
    {
        gameObject.SetActive(false);
    }

    private void StartValues()
    {
        moneyText.text = (playerData.staminaLevel * upgradeMoneyValue + 85).ToString();
        double value = Math.Round(playerData.Stamina, 1);
        valueText.text = (value).ToString();
    }
}
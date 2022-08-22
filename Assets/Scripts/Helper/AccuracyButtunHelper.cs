using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class AccuracyButtunHelper : MonoBehaviour
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
        
        if (playerData.PlayerMoney < playerData.accuracyLevel * upgradeMoneyValue + 85)
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
       
        playerData.PlayerMoney -= playerData.accuracyLevel * upgradeMoneyValue + 85;
        playerData.accuracyLevel += 1;
        playerData.Accuracy += 1;
        moneyText.text = (playerData.accuracyLevel * upgradeMoneyValue + 85).ToString();
        valueText.text = (playerData.Accuracy).ToString();
        buttonCheck.Raise();
    }

    public void ButtonClose()
    {
        gameObject.SetActive(false);
    }

    private void StartValues()
    {
        valueText.text = (playerData.Accuracy).ToString();
        moneyText.text = (playerData.accuracyLevel * upgradeMoneyValue + 85).ToString();
    }
}
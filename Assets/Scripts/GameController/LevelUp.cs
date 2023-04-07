using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelUp : MonoBehaviour
{
    [SerializeField] private float upGradeMaxHealthAmount = 10;
    [SerializeField] private float upGradeDamageAmount = 10;
    [SerializeField] private float upGradeProjectileSpeedAmount = 20;
    [SerializeField] private float upGradeMoveSpeedAmount = 15;
    [SerializeField] private float upGradeCooldownAmount = 5;

    [Header("===== Get Component =====")]
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private PlayerStatus ps;
    
    [Header("===== UI Upgrade Slot =====")]
    [SerializeField] private GameObject LevelUpSlotUI;
    [SerializeField] private Text slot1ItemName;
    [SerializeField] private Text slot2ItemName;
    [SerializeField] private Text slot3ItemName;
    [SerializeField] private Text slot1DescriptionText;
    [SerializeField] private Text slot2DescriptionText;
    [SerializeField] private Text slot3DescriptionText;

    private List<string> statusUpgrade = new List<string>();
    private List<string> itemUpgrade = new List<string>();
    private List<char> upgradeGroup = new List<char>(3);
    private List<int> upgradeIndex = new List<int>(3);

    private int itemUpgradeCount;

    private void Start()
    {
        statusUpgrade.Add("Max health");
        statusUpgrade.Add("Damage");
        statusUpgrade.Add("Projectile speed");
        statusUpgrade.Add("Move speed");
        statusUpgrade.Add("Cooldown");
        itemUpgrade.Add("Bow");
        
        upgradeGroup.Add('0');
        upgradeGroup.Add('0');
        upgradeGroup.Add('0');
        
        upgradeIndex.Add(-1);
        upgradeIndex.Add(-1);
        upgradeIndex.Add(-1);
        
        OpenLevelUpSlotUI(false);
    }

    private void Update()
    {
        
    }

    public void PlayerLevelUp()
    {
        itemUpgradeCount = itemUpgrade.Count;   // Reset

        for (int i = 0; i < 3; i++)
        {
            if (itemUpgradeCount > 0)
            {
                upgradeGroup[i] = SetUpgradeGroup(Random.Range(0, 2));
            }
            else
            {
                upgradeGroup[i] = SetUpgradeGroup(0);
            }

            if (upgradeGroup[i] == 's')
            {
                upgradeIndex[i] = Random.Range(0, statusUpgrade.Count);
            }
            else if (upgradeGroup[i] == 'i')
            {
                upgradeIndex[i] = Random.Range(0, itemUpgrade.Count);
                itemUpgradeCount--;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            UpdateTextUI(i, upgradeGroup[i], upgradeIndex[i]);
        }
        
        UpdateLevelUpSlotUI();
        OpenLevelUpSlotUI(true);
    }

    private void UpdateTextUI(int i, char upgradeGroup,int index)
    {
        Debug.Log($"slot {i + 1} / Upgrade name {ReturnUpgradeName(upgradeGroup, index)}");
    }

    private char SetUpgradeGroup(int i)
    {
        if (i == 0)
        {
            return 's';
        }
        else if (i == 1)
        {
            return 'i';
        }
        else
        {
            return '0';
        }
    }

    private void UpdateLevelUpSlotUI()
    {
        slot1ItemName.text = ReturnUpgradeName(upgradeGroup[0], upgradeIndex[0]);
        slot1DescriptionText.text = ReturnUpgradeDescription(ReturnUpgradeName(upgradeGroup[0], upgradeIndex[0]));
        slot2ItemName.text = ReturnUpgradeName(upgradeGroup[1], upgradeIndex[1]);
        slot2DescriptionText.text = ReturnUpgradeDescription(ReturnUpgradeName(upgradeGroup[1], upgradeIndex[1]));
        slot3ItemName.text = ReturnUpgradeName(upgradeGroup[2], upgradeIndex[2]);
        slot3DescriptionText.text = ReturnUpgradeDescription(ReturnUpgradeName(upgradeGroup[2], upgradeIndex[2]));
    }

    private string ReturnUpgradeName(char upgradeGroup, int index)
    {
        if (upgradeGroup == 's')
        {
            return statusUpgrade[index];
        }
        else
        {
            return itemUpgrade[index];
        }
    }
    
    private string ReturnUpgradeDescription(string upgradeName)
    {
        switch (upgradeName)
        {
            case "Max health" :
                return "Max health + " + upGradeMaxHealthAmount;
            break;
            
            case "Damage" :
                return $"All damage + {upGradeDamageAmount} %";
            break;
            
            case "Projectile speed" :
                return $"Arrow projectile speed + {upGradeProjectileSpeedAmount}";
            break;
            
            case "Move speed" :
                return $"Player move speed + {upGradeMoveSpeedAmount}";
            break;
            
            case "Cooldown" :
                return $"Arrow shoot cooldown - {upGradeCooldownAmount} %";
            break;
            
            default:
                return "Upgrade name didn't match with Switch()";
        }

        return "-1";
    }

    public void OpenLevelUpSlotUI(bool open)
    {
        if (open)
        {
            Time.timeScale = 0;
            pm.SetPausePlayer(true);
            LevelUpSlotUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pm.SetPausePlayer(false);
            LevelUpSlotUI.SetActive(false);
            ps.ResetOpenLevelUpUI();
        }
    }
}

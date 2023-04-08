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
    [SerializeField] private Bow bow;

    [Header("===== UI Upgrade Slot =====")]
    [SerializeField] private GameObject LevelUpSlotUI;
    [SerializeField] private Text slot1ItemName;
    [SerializeField] private Text slot2ItemName;
    [SerializeField] private Text slot3ItemName;
    [SerializeField] private Text slot1DescriptionText;
    [SerializeField] private Text slot2DescriptionText;
    [SerializeField] private Text slot3DescriptionText;
    [SerializeField] private UIIconShowInUpgradeSlot slot1IconShow;
    [SerializeField] private UIIconShowInUpgradeSlot slot2IconShow;
    [SerializeField] private UIIconShowInUpgradeSlot slot3IconShow;

    [Header("===== UI Item Level Text =====")]
    [SerializeField] private Text bowLevelText;

    private List<string> statusUpgrade = new List<string>();
    private List<string> itemUpgrade = new List<string>();
    private List<char> upgradeGroup = new List<char>(3);
    private List<int> upgradeIndex = new List<int>(3);

    // ===== Level item here =====
    private int levelBow = 1;

    private int itemUpgradeCount;
    private bool cooldownUpgradeStatusIsInUpgradeList = true;

    private void Start()
    {
        statusUpgrade.Add("Increase player max health");
        statusUpgrade.Add("Increase player damage");
        statusUpgrade.Add("Increase player projectile speed");
        statusUpgrade.Add("Increase player move speed");
        statusUpgrade.Add("Reduce arrow cooldown");
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

        /*for (int i = 0; i < 3; i++)
        {
            UpdateTextUI(i, upgradeGroup[i], upgradeIndex[i]);
        }*/
        
        UpdateLevelUpSlotUI();
        OpenLevelUpSlotUI(true);
    }

    private void UpdateTextUI(int i, char upgradeGroup,int index)
    {
        Debug.Log($"slot {i + 1} / Upgrade name {ReturnUpgradeName(upgradeGroup, index, true)}");
    }   // This is for show in Console

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

    public void RemoveCooldownUpgradeStatus()
    {
        if (cooldownUpgradeStatusIsInUpgradeList)
        {
            cooldownUpgradeStatusIsInUpgradeList = false;
            statusUpgrade.Remove("Reduce arrow cooldown");
        }
    }

    private void UpdateLevelUpSlotUI()
    {
        slot1ItemName.text = ReturnUpgradeName(upgradeGroup[0], upgradeIndex[0], true);
        slot1DescriptionText.text = ReturnUpgradeDescription(ReturnUpgradeName(upgradeGroup[0], upgradeIndex[0], false));
        slot1IconShow.ShowIcon(ReturnUpgradeName(upgradeGroup[0], upgradeIndex[0], false));
        
        slot2ItemName.text = ReturnUpgradeName(upgradeGroup[1], upgradeIndex[1], true);
        slot2DescriptionText.text = ReturnUpgradeDescription(ReturnUpgradeName(upgradeGroup[1], upgradeIndex[1], false));
        slot2IconShow.ShowIcon(ReturnUpgradeName(upgradeGroup[1], upgradeIndex[1], false));
        
        slot3ItemName.text = ReturnUpgradeName(upgradeGroup[2], upgradeIndex[2], true);
        slot3DescriptionText.text = ReturnUpgradeDescription(ReturnUpgradeName(upgradeGroup[2], upgradeIndex[2], false));
        slot3IconShow.ShowIcon(ReturnUpgradeName(upgradeGroup[2], upgradeIndex[2], false));
    }

    private string ReturnUpgradeName(char upgradeGroup, int index, bool showItemLevel)
    {
        if (upgradeGroup == 's')
        {
            return statusUpgrade[index];
        }
        else
        {
            if (showItemLevel)
            {
                return $"{itemUpgrade[index]} LV. {ShowUpgradeItemToLevel(itemUpgrade[index])}";
            }
            else
            {
                return itemUpgrade[index];
            }
        }
    }
    
    private string ReturnUpgradeDescription(string upgradeName)
    {
        switch (upgradeName)
        {
            case "Increase player max health" :
                return "Max health + " + upGradeMaxHealthAmount;
            break;
            
            case "Increase player damage" :
                return $"All damage + {upGradeDamageAmount} %";
            break;
            
            case "Increase player projectile speed" :
                return $"Arrow projectile speed + {upGradeProjectileSpeedAmount}";
            break;
            
            case "Increase player move speed" :
                return $"Player move speed + {upGradeMoveSpeedAmount}";
            break;
            
            case "Reduce arrow cooldown" :
                return $"Arrow shoot cooldown - {upGradeCooldownAmount} %";
            break;
            
            // ============================= End - Status Description =============================

            case "Bow" :
                return UpGradeBowDescription(levelBow + 1);
                break;

            default:
                return "Upgrade name didn't match with Switch()";
            break;
        }
    }

    private void UpGrade(string upgradeName)
    {
        switch (upgradeName)
        {
            case "Increase player max health" :
                ps.UpgradeStatus(0, upGradeMaxHealthAmount);
                break;
            
            case "Increase player damage" :
                ps.UpgradeStatus(1, upGradeDamageAmount);
                ps.UpdateStatus();
                break;
            
            case "Increase player projectile speed" :
                ps.UpgradeStatus(2, upGradeProjectileSpeedAmount);
                ps.UpdateStatus();
                break;
            
            case "Increase player move speed" :
                ps.UpgradeStatus(3, upGradeMoveSpeedAmount);
                ps.UpdateStatus();
                break;
            
            case "Reduce arrow cooldown" :
                ps.UpgradeStatus(4, upGradeCooldownAmount);
                ps.UpdateStatus();
                break;
            
            // ============================= End - Status Upgrade =============================
            
            case "Bow" :
                UpgradeBow(levelBow + 1);
                break;
                
            
            default:
                Debug.Log("Upgrade name didn't match with Switch()");
                break;
        }
        
        //slot1IconShow.DisableAll();
        //slot2IconShow.DisableAll();
        //slot3IconShow.DisableAll();
    }

    public void PressUpgradeSlot1()
    {
        UpGrade(ReturnUpgradeName(upgradeGroup[0], upgradeIndex[0], false));
        OpenLevelUpSlotUI(false);
    }
    
    public void PressUpgradeSlot2()
    {
        UpGrade(ReturnUpgradeName(upgradeGroup[1], upgradeIndex[1], false));
        OpenLevelUpSlotUI(false);
    }
    
    public void PressUpgradeSlot3()
    {
        UpGrade(ReturnUpgradeName(upgradeGroup[2], upgradeIndex[2], false));
        OpenLevelUpSlotUI(false);
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

    private int ShowUpgradeItemToLevel(string itemName)
    {
        switch (itemName)
        {
            case "Bow" :
                return levelBow + 1;
            break;
            
            default:
                return -1;
            // ShowUpgradeItemToLevel() - No match item name in switch()
        }
    }
    
    // =============================== Item - Upgrade() ===============================

    private void UpgradeBow(int lv)
    {
        switch (lv)
        {
            case 2 :
                bow.UpgradeBow(3, 0, 0, 0);
                levelBow++;
                break;
            
            case 3 :
                bow.UpgradeBow(0, 1, 0, 0);
                levelBow++;
                break;
            
            case 4 :
                bow.UpgradeBow(3, 0, 0, 0);
                levelBow++;
                break;
            
            case 5 :
                bow.UpgradeBow(0, 1, 0, 0);
                levelBow++;
                break;
            
            case 6 :
                bow.UpgradeBow(0, 0, 0.2f, 0);
                levelBow++;
                break;
            
            case 7 :
                bow.UpgradeBow(0, 2, 0, 0);
                levelBow++;
                break;
            
            case 8 :
                bow.UpgradeBow(4, 0, 0, 0);
                levelBow++;
                break;
            
            case 9 :
                bow.UpgradeBow(0, 0, 0.3f, 0);
                levelBow++;
                break;
            
            case 10 :
                bow.UpgradeBow(0, 0, 0, 1);
                levelBow++;
                itemUpgrade.Remove("Bow");
                break;
        }
        
        UpdateBowLevelText();
    }
    
    // =============================== Item - UpgradeDescription() ===============================

    private string UpGradeBowDescription(int lv)
    {
        switch (lv)
        {
            case 2 :
                return "Base damage +3";
            break;
            
            case 3 :
                return "Projectile speed +1";
            break;
            
            case 4 :
                return "Base damage +3";
            break;
            
            case 5 :
                return "Projectile speed +1";
                break;

            case 6 :
                return "Cooldown -0.2 second";
                break;

            case 7 :
                return "Projectile speed +2";
                break;

            case 8 :
                return "Base damage +4";
                break;

            case 9 :
                return "Cooldown -0.3 second";
                break;

            case 10 :
                return "Piercing +1";
                break;

            default:
                return "UpGradeBowDescription() - int lv didn't match in switch";
        }
    }
    
    // =============================== Item - Level Text ===============================

    private void UpdateBowLevelText()
    {
        if (levelBow == 10)
        {
            bowLevelText.text = "LV. max";
        }
        else
        {
            bowLevelText.text = $"LV. {levelBow}";
        }
    }
}

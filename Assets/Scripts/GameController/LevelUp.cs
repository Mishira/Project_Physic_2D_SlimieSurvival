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
    [SerializeField] private ItemController itemController;

    [Header("===== UI Upgrade Slot =====")]
    [SerializeField] private GameObject levelUpSlotUI;
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
    private int levelCrucifix = 1;
    private int levelHealthOrb = 1;
    private int levelShield = 1;
    private int levelSyringe = 1;
    private int levelKnowledge = 1;
    private int levelGoldenSword = 1;
    private int levelBoot = 1;
    private int levelGoldenClock = 1;
    private int levelGoldenHeart = 1;
    private int levelMarkOfCalamity = 1;

    private int itemUpgradeCount;
    private bool cooldownUpgradeStatusIsInUpgradeList = true;
    private bool projectileSpeedUpgradeStatusIsInUpgradeList = true;

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

    public void RemoveProjectileSpeedUpgradeStatus()
    {
        if (projectileSpeedUpgradeStatusIsInUpgradeList)
        {
            projectileSpeedUpgradeStatusIsInUpgradeList = false;
            statusUpgrade.Remove("Increase player projectile speed");
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

    public void AddItemUpgradeList(string newItem)
    {
        itemUpgrade.Add(newItem);
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
    
    // =================================================================================================================
    
    private string ReturnUpgradeDescription(string upgradeName)
    {
        switch (upgradeName)
        {
            case "Increase player max health" :
                return $"Max health + {upGradeMaxHealthAmount} then heal 20% max HP";

            case "Increase player damage" :
                return $"All damage + {upGradeDamageAmount} %";

            case "Increase player projectile speed" :
                return $"Arrow projectile speed + {upGradeProjectileSpeedAmount} %";

            case "Increase player move speed" :
                return $"Player move speed + {upGradeMoveSpeedAmount} %";

            case "Reduce arrow cooldown" :
                return $"Arrow shoot cooldown - {upGradeCooldownAmount} %";

            // ============================= End - Status Description =============================

            case "Bow" :
                return UpGradeBowDescription(levelBow + 1);

            case "Crucifix" :
                return UpGradeCrucifixDescription(levelCrucifix + 1);
            
            case "Health orb" :
                return UpGradeHealthOrbDescription(levelHealthOrb + 1);
            
            case "Shield" :
                return UpGradeShieldDescription(levelShield + 1);
            
            case "Syringe" :
                return UpGradeSyringeDescription(levelSyringe + 1);
            
            case "Knowledge" :
                return UpGradeKnowledgeDescription(levelKnowledge + 1);
            
            case "Golden sword" :
                return UpGradeGoldenSwordDescription(levelGoldenSword + 1);
            
            case "Boot" :
                return UpGradeBootDescription(levelBoot + 1);
            
            case "Golden clock" :
                return UpGradeGoldenClockDescription(levelGoldenClock + 1);
            
            case "Golden heart" :
                return UpGradeGoldenHeartDescription(levelGoldenHeart + 1);
            
            case "Mark of Calamity" :
                return UpGradeMarkOfCalamityDescription(levelMarkOfCalamity + 1);
            
            
            default:
                return "Upgrade name didn't match with Switch()";
        }
    }

    private void UpGrade(string upgradeName)
    {
        switch (upgradeName)
        {
            case "Increase player max health" :
                ps.UpgradeStatus(0, upGradeMaxHealthAmount);
                ps.HealPlayer(ps._maxHealth * 0.2f);
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
            
            case "Crucifix" :
                UpgradeCrucifix(levelCrucifix + 1);
                break;
                
            case "Health orb" :
                UpgradeHealthOrb(levelHealthOrb + 1);
                break;
            
            case "Shield" :
                UpgradeShield(levelShield + 1);
                break;
            
            case "Syringe" :
                UpgradeSyringe(levelSyringe + 1);
                break;
            
            case "Knowledge" :
                UpgradeKnowledge(levelKnowledge + 1);
                break;
            
            case "Golden sword" :
                UpgradeGoldenSword(levelGoldenSword + 1);
                break;
            
            case "Boot" :
                UpgradeBoot(levelBoot + 1);
                break;
            
            case "Golden clock" :
                UpgradeGoldenClock(levelGoldenClock + 1);
                break;
            
            case "Golden heart" :
                UpgradeGoldenHeart(levelGoldenHeart + 1);
                break;
            
            case "Mark of Calamity" :
                UpgradeMarkOfCalamity(levelMarkOfCalamity + 1);
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
            SetTimeScale(0);
            pm.SetPausePlayer(true);
            levelUpSlotUI.SetActive(true);
        }
        else
        {
            SetTimeScale(1);
            pm.SetPausePlayer(false);
            levelUpSlotUI.SetActive(false);
            ps.ResetOpenLevelUpUI();
        }
    }

    public void SetTimeScale(float change)
    {
        Time.timeScale = change;
    }

    private int ShowUpgradeItemToLevel(string itemName)
    {
        switch (itemName)
        {
            case "Bow" :
                return levelBow + 1;

            case "Crucifix" :
                return levelCrucifix + 1;

            case "Health orb" :
                return levelHealthOrb + 1;
            
            case "Shield" :
                return levelShield + 1;
            
            case "Syringe" :
                return levelSyringe + 1;
            
            case "Knowledge" :
                return levelKnowledge + 1;
            
            case "Golden sword" :
                return levelGoldenSword + 1;
            
            case "Boot" :
                return levelBoot + 1;
            
            case "Golden clock" :
                return levelGoldenClock + 1;
            
            case "Golden heart" :
                return levelGoldenHeart + 1;
            
            case "Mark of Calamity" :
                return levelMarkOfCalamity + 1;
            
            
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

    private void UpgradeCrucifix(int lv)
    {
        switch (lv)
        {
            case 2 :
                ps.ChangeCrucifixInvincibleTime(2.5f);
                itemController.UpdateCrucifixUpGrade(75, 105);
                levelCrucifix++;
                break;
            
            case 3 :
                ps.ChangeCrucifixInvincibleTime(3f);
                itemController.UpdateCrucifixUpGrade(90, 90);
                levelCrucifix++;
                break;

            case 4 :
                ps.ChangeCrucifixInvincibleTime(3.5f);
                itemController.UpdateCrucifixUpGrade(105, 75);
                levelCrucifix++;
                break;

            case 5 :
                ps.ChangeCrucifixInvincibleTime(4f);
                itemController.UpdateCrucifixUpGrade(120, 60);
                itemUpgrade.Remove("Crucifix");
                break;
        }
    }

    private void UpgradeHealthOrb(int lv)
    {
        switch (lv)
        {
            case 2 :
                ps.UpgradeStatus(0, 10);
                itemController.UpgradeHealthOrbHealPercent(1.25f);
                levelHealthOrb++;
                break;
            
            case 3 :
                ps.UpgradeStatus(0, 10);
                itemController.UpgradeHealthOrbHealPercent(1.5f);
                levelHealthOrb++;
                break;

            case 4 :
                ps.UpgradeStatus(0, 10);
                itemController.UpgradeHealthOrbHealPercent(1.7f);
                levelHealthOrb++;
                break;

            case 5 :
                ps.UpgradeStatus(0, 10);
                itemController.UpgradeHealthOrbHealPercent(2f);
                itemUpgrade.Remove("Health orb");
                break;
        }
    }

    private void UpgradeShield(int lv)
    {
        switch (lv)
        {
            case 2 :
                itemController.UpdateShieldUpgrade(1.5f, 9.5f);
                levelShield++;
                break;
            
            case 3 :
                itemController.UpdateShieldUpgrade(2f, 9f);
                levelShield++;
                break;
            
            case 4 :
                itemController.UpdateShieldUpgrade(2.5f, 8.5f);
                levelShield++;
                break;
            
            case 5 :
                itemController.UpdateShieldUpgrade(3f, 8f);
                itemUpgrade.Remove("Shield");
                break;
        }
    }

    private void UpgradeSyringe(int lv)
    {
        switch (lv)
        {
            case 2 :
                itemController.UpgradeSyringe(15, 30, 20, 7, 27.5f);
                levelSyringe++;
                break;
            
            case 3 :
                itemController.UpgradeSyringe(20, 40, 25, 8, 25f);
                levelSyringe++;
                break;
            
            case 4 :
                itemController.UpgradeSyringe(25, 50, 30, 9, 22.5f);
                levelSyringe++;
                break;
            
            case 5 :
                itemController.UpgradeSyringe(30, 60, 40, 10, 20f);
                itemUpgrade.Remove("Syringe");
                break;
        }
    }

    private void UpgradeKnowledge(int lv)
    {
        switch (lv)
        {
            case 2 :
                itemController.UpgradeKnowledge(10);     // Add 10 ---> 20%
                levelKnowledge++;
                break;
            
            case 3 :
                itemController.UpgradeKnowledge(10);     // Add 10 ---> 30%
                levelKnowledge++;
                break;
            
            case 4 :
                itemController.UpgradeKnowledge(10);     // Add 10 ---> 40%
                levelKnowledge++;
                break;
            
            case 5 :
                itemController.UpgradeKnowledge(10);     // Add 10 ---> 50%
                itemUpgrade.Remove("Knowledge");
                break;
        }
    }

    private void UpgradeGoldenSword(int lv)
    {
        switch (lv)
        {
            case 2 :
                itemController.UpgradeGoldenSword(15);      // Add 15 --> 45%
                levelGoldenSword++;
                break;
            
            case 3 :
                itemController.UpgradeGoldenSword(15);      // Add 15 --> 60%
                levelGoldenSword++;
                break;

            case 4 :
                itemController.UpgradeGoldenSword(20);      // Add 20 --> 80%
                levelGoldenSword++;
                break;

            case 5 :
                itemController.UpgradeGoldenSword(20);      // Add 20 --> 100%
                itemUpgrade.Remove("Golden sword");
                break;

        }
    }

    private void UpgradeBoot(int lv)
    {
        switch (lv)
        {
            case 2 :
                itemController.UpgradeBoot(30);     // Add 30 --> 70%
                levelBoot++;
                break;
            
            case 3 :
                itemController.UpgradeBoot(30);     // Add 30 --> 100%
                itemUpgrade.Remove("Boot");
                break;
        }
    }

    private void UpgradeGoldenClock(int lv)
    {
        switch (lv)
        {
            case 2 :
                itemController.UpgradeGoldenClock(7.5f);        // Add 7.5 --> -17.5%
                levelGoldenClock++;
                break;
            
            case 3 :
                itemController.UpgradeGoldenClock(7.5f);        // Add 7.5 --> -25%
                levelGoldenClock++;
                break;

            case 4 :
                itemController.UpgradeGoldenClock(7.5f);        // Add 7.5 --> -32.5%
                levelGoldenClock++;
                break;

            case 5 :
                itemController.UpgradeGoldenClock(7.5f);        // Add 7.5 --> -40%
                itemUpgrade.Remove("Golden clock");
                break;

        }
    }

    private void UpgradeGoldenHeart(int lv)
    {
        switch (lv)
        {
            case 2 :
                itemController.UpgradeGoldenHeart(5, 2, 4, 16);
                levelGoldenHeart++;
                break;
            
            case 3 :
                itemController.UpgradeGoldenHeart(6, 3, 6, 15);
                levelGoldenHeart++;
                break;

            case 4 :
                itemController.UpgradeGoldenHeart(7, 3, 8, 15);
                levelGoldenHeart++;
                break;

            case 5 :
                itemController.UpgradeGoldenHeart(8, 4, 10, 14);
                itemUpgrade.Remove("Golden heart");
                break;

        }
    }

    private void UpgradeMarkOfCalamity(int lv)
    {
        switch (lv)
        {
            case 2 :
                itemController.UpgradeMarkOfCalamity(12, 3);
                levelMarkOfCalamity++;
                break;
            
            case 3 :
                itemController.UpgradeMarkOfCalamity(16, 4);
                levelMarkOfCalamity++;
                break;

            case 4 :
                itemController.UpgradeMarkOfCalamity(20, 5);
                levelMarkOfCalamity++;
                break;

            case 5 :
                itemController.UpgradeMarkOfCalamity(24, 6);
                levelMarkOfCalamity++;
                //itemUpgrade.Remove("Mark of Calamity");
                break;
            
            case 6 :
                itemController.UpgradeMarkOfCalamity(27, 6);
                levelMarkOfCalamity++;
                break;
            
            case 7 :
                itemController.UpgradeMarkOfCalamity(30, 6);
                levelMarkOfCalamity++;
                break;

            case 8 :
                itemController.UpgradeMarkOfCalamity(34, 6);
                levelMarkOfCalamity++;
                break;

            case 9 :
                itemController.UpgradeMarkOfCalamity(37, 6);
                levelMarkOfCalamity++;
                break;

            case 10 :
                itemController.UpgradeMarkOfCalamity(38, 8);
                levelMarkOfCalamity++;
                break;

            case 11 :
                itemController.UpgradeMarkOfCalamity(40, 10);
                levelMarkOfCalamity++;
                break;

            case 12 :
                itemController.MaxUpgradeMarkOfCalamity(150);
                levelMarkOfCalamity++;
                itemUpgrade.Remove("Mark of Calamity");
                break;

        }
    }
    
    // =============================== Item - UpgradeDescription() ===============================

    private string UpGradeBowDescription(int lv)
    {
        switch (lv)
        {
            case 2 :
                return "Base damage +3";

            case 3 :
                return "Projectile speed +1";

            case 4 :
                return "Base damage +3";

            case 5 :
                return "Projectile speed +1";

            case 6 :
                return "Cooldown -0.2 second";

            case 7 :
                return "Projectile speed +2";

            case 8 :
                return "Base damage +4";

            case 9 :
                return "Cooldown -0.3 second";

            case 10 :
                return "Piercing +1";

            default:
                return "UpGradeBowDescription() - int lv didn't match in switch";
        }
    }

    private string UpGradeCrucifixDescription(int lv)
    {
        switch (lv)
        {
            case 2 :
                return "Protect player HP can't go below 1 then give Invincible for 2.5 second and regen 75 HP in 10 second. Cooldown 105 second";

            case 3 :
                return "Protect player HP can't go below 1 then give Invincible for 3 second and regen 90 HP in 10 second. Cooldown 90 second";

            case 4 :
                return "Protect player HP can't go below 1 then give Invincible for 3.5 second and regen 105 HP in 10 second. Cooldown 75 second";

            case 5 :
                return "Protect player HP can't go below 1 then give Invincible for 4 second and regen 120 HP in 10 second. Cooldown 60 second";

            default:
                return "UpGradeCrucifixDescription(int lv) - int lv didn't match in switch";
        }
    }

    private string UpGradeHealthOrbDescription(int lv)
    {
        switch (lv)
        {
            case 2 :
                return "Max health +30 and Heal 1.25% max HP every second.";
            
            case 3 :
                return "Max health +40 and Heal 1.5% max HP every second.";

            case 4 :
                return "Max health +50 and Heal 1.75% max HP every second.";

            case 5 :
                return "Max health +60 and Heal 2% max HP every second.";
            
            default:
                return "UpGradeHealthOrbDescription(int lv) - didn't match in switch";

        }
    }

    private string UpGradeShieldDescription(int lv)
    {
        switch (lv)
        {
            case 2 :
                return "When player take damage gain Invincible for 1.5 second. Cooldown 9.5 second";
            
            case 3 :
                return "When player take damage gain Invincible for 2 second. Cooldown 9 second";

            case 4 :
                return "When player take damage gain Invincible for 2.5 second. Cooldown 8.5 second";

            case 5 :
                return "When player take damage gain Invincible for 3 second. Cooldown 8 second";
            
            default:
                return "UpGradeShieldDescription(int lv) - didn't match in switch";

        }
    }

    private string UpGradeSyringeDescription(int lv)
    {
        switch (lv)
        {
            case 2 :
                return "When player take damage heal 15 HP then gain Move speed +30 %, Damage +20 %, for 7 second. cooldown 27.5 second";
            
            case 3 :
                return "When player take damage heal 20 HP then gain Move speed +40 %, Damage +25 %, for 8 second. cooldown 25 second";
            
            case 4 :
                return "When player take damage heal 25 HP then gain Move speed +50 %, Damage +30 %, for 9 second. cooldown 22.5 second";
            
            case 5 :
                return "When player take damage heal 30 HP then gain Move speed +60 %, Damage +40 %, for 10 second. cooldown 20 second";
            
            default:
                return "UpGradeSyringeDescription(int lv) - didn't match in switch";
        }
    }

    private string UpGradeKnowledgeDescription(int lv)
    {
        switch (lv)
        {
            case 2 :
                return "Slime drop 20% more EXP.";
            
            case 3 :
                return "Slime drop 30% more EXP.";

            case 4 :
                return "Slime drop 40% more EXP.";

            case 5 :
                return "Slime drop 50% more EXP.";
            
            default:
                return "UpGradeKnowledgeDescription(int lv) - didn't match in switch";

        }
    }

    private string UpGradeGoldenSwordDescription(int lv)
    {
        switch (lv)
        {
            case 2 :
                return "All player damage +45%";
            
            case 3 :
                return "All player damage +60%";

            case 4 :
                return "All player damage +80%";

            case 5 :
                return "All player damage +100%";
            
            default:
                return "UpGradeGoldenSwordDescription(int lv) - didn't match in switch";

        }
    }

    private string UpGradeBootDescription(int lv)
    {
        switch (lv)
        {
            case 2 :
                return "Player move speed +70%";
            
            case 3 :
                return "Player move speed +100%";
            
            default:
                return "UpGradeBoot(int lv) - didn't match in switch";

        }
    }

    private string UpGradeGoldenClockDescription(int lv)
    {
        switch (lv)
        {
            case 2 :
                return "Player attack cooldown -17.5%";
            
            case 3 :
                return "Player attack cooldown -25%";

            case 4 :
                return "Player attack cooldown -32.5%";

            case 5 :
                return "Player attack cooldown -40%";
            
            default:
                return "UpGradeGoldenClock(int lv) - didn't match in switch";

        }
    }

    private string UpGradeGoldenHeartDescription(int lv)
    {
        switch (lv)
        {
            case 2 :
                return "Negate next damage than gain +5 max HP, +2% Damage, +4% projectile speed. Cooldown 16 second";
            
            case 3 :
                return "Negate next damage than gain +6 max HP, +3% Damage, +6% projectile speed. Cooldown 15 second";

            case 4 :
                return "Negate next damage than gain +7 max HP, +3% Damage, +8% projectile speed. Cooldown 15 second";

            case 5 :
                return "Negate next damage than gain +8 max HP, +4% Damage, +10% projectile speed. Cooldown 14 second";

            default:
                return "UpGradeGoldenHeartDescription(int lv) - didn't match in switch";
        }
    }

    private string UpGradeMarkOfCalamityDescription(int lv)
    {
        switch (lv)
        {
            case 2 :
                return "Blue and Red Slime will spawn more...";
            
            case 3 :
                return "Blue and Red Slime will spawn more...  Are you sure about that?";

            case 4 :
                return "This upgrade is very danger!  Just stop upgrade this thing...  Trust me";

            case 5 :
                return "OK... If you what this... Good luck and don't say I didn't tell about this thing.";
            
            case 6 :
                return "...";
            
            case 7 :
                return "......";
            
            case 8 :
                return "..........";

            case 9 :
                return "Hey... Why are you still Upgrade this thing?";

            case 10 :
                return "Hey why are you doing this... You want to challenge yourself?...   WHY?";

            case 11 :
                return "Warning if you continue upgrade this thing it will destroy yourself!!!	";

            case 12 :
                return "This is last warning... You need to stop upgrade this thing NOW.";

            default:
                return "UpGradeMarkOfCalamityDescription(int lv) - didn't match in switch";

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

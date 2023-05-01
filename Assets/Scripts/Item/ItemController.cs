using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ItemController : MonoBehaviour
{
    [Header("===== Icon UI in Slot =====")]
    [SerializeField] private UIIconItemInPlayerSlot uiIconItemInPlayerSlot1;
    [SerializeField] private UIIconItemInPlayerSlot uiIconItemInPlayerSlot2;
    [SerializeField] private UIIconItemInPlayerSlot uiIconItemInPlayerSlot3;
    [SerializeField] private UIIconItemInPlayerSlot uiIconItemInPlayerSlot4;
    [SerializeField] private UIIconItemInPlayerSlot uiIconItemInPlayerSlot5;
    [SerializeField] private UIIconItemInPlayerSlot uiIconItemInPlayerSlot6;
    
    [Header("===== Cooldown UI =====")]
    [SerializeField] private ItemEffect itemEffectArtifact1;
    [SerializeField] private ItemEffect itemEffectArtifact2;
    [SerializeField] private ItemEffect itemEffectArtifact3;
    [SerializeField] private ItemEffect itemEffectArtifact4;
    [SerializeField] private ItemEffect itemEffectArtifact5;
    [SerializeField] private ItemEffect itemEffectArtifact6;

    [Header("===== Crucifix =====")]
    [SerializeField] private float crucifixHeal = 30;
    [SerializeField] private int crucifixHealDuration = 10;
    [SerializeField] private float crucifixCooldown = 120;
    
    [Header("===== Health orb =====")]
    [SerializeField] private float healthOrbHealPercent = 1;

    [Header("===== Shield =====")] 
    [SerializeField] private float shieldInvincibleTime = 1;
    [SerializeField] private float shieldCooldown = 10;

    [Header("===== Syringe =====")]
    [SerializeField] private float syringeHeal = 10;
    [SerializeField] private float syringeBuffMoveSpeed = 20;
    [SerializeField] private float syringeBuffDamage = 15;
    [SerializeField] private float syringeBuffDuration = 6;
    [SerializeField] private float syringeCooldown = 30;

    [Header("===== Knowledge =====")]
    [SerializeField] private float knowledgeEXPMultiply = 35;

    [Header("===== Golden sword =====")]
    [SerializeField] private float goldenSwordBuffDamage = 30;

    [Header("===== Boot =====")]
    [SerializeField] private float bootBuffMoveSpeed = 40;

    [Header("===== Golden clock =====")]
    [SerializeField] private float goldenClockBuffCooldown = 10;

    [Header("===== Golden Heart =====")]
    [SerializeField] private float goldenHeartAddMaxHP = 4;
    [SerializeField] private float goldenHeartAddDamage = 2;
    [SerializeField] private float goldenHeartAddProjectileSpeed = 2;
    [SerializeField] private float goldenHeartCooldown = 16;

    [Header("===== Mark of Calamity =====")]
    [SerializeField] private int blueSlimeSpawnRate = 8;
    [SerializeField] private int redSlimeSpawnRate = 2;

    [Header("===== Get Component =====")]
    [SerializeField] private PlayerStatus ps;
    [SerializeField] private UIController uiC;
    [SerializeField] private LevelUp levelUp;
    [SerializeField] private GameManager gameManager;

    // List player item
    private List<string> itemInRandomBox = new List<string>();

    public List<string> _itemInRandomBox => itemInRandomBox;
    
    
    private string itemWaitingToPutInToList;
    private bool readyToAdd = false;
    private int nextEmptySlotItem = 1;

    public int _nextEmptySlotItem => nextEmptySlotItem;

    // Crucifix
    private int crucifixHealTime;
    private bool crucifixReadyToHeal = true;
    private int crucifixSlot = -1;
    private bool crucifixPickUp = false;
    
    // Health orb
    private bool healthReadyToHeal = true;
    private int healthOrbSlot = -1;
    private bool healthOrbPickUp = false;
    
    // Shield
    private int shieldSlot = -1;
    private bool shieldPickUp = false;
    
    // Syringe
    private int syringeSlot = -1;
    private bool syringePickUp = false;
    private float lastSyringeBuffMoveSpeed;
    private float lastSyringeBuffDamage;
    
    // Knowledge
    private int knowledgeSlot = -1;
    private bool knowledgePickUp = false;
    
    // Golden sword
    private int goldenSwordSlot = -1;
    private bool goldenSwordPickUp = false;
    
    // Boot
    private int bootSlot = -1;
    private bool bootPickUp = false;
    
    // Golden clock
    private int goldenClockSlot = -1;
    private bool goldenClockPickUp = false;
    
    // Golden heart
    private int goldenHeartSlot = -1;
    private bool goldenHeartPickUp = false;
    private bool goldenHeartReady = false;
    
    // Mark of Calamity
    private int markOfCalamitySlot = -1;
    private bool markOfCalamityPickUp = false;

    private void Start()
    {
        itemInRandomBox.Add("Crucifix");
        itemInRandomBox.Add("Health orb");
        itemInRandomBox.Add("Shield");
        itemInRandomBox.Add("Syringe");
        itemInRandomBox.Add("Knowledge");
        itemInRandomBox.Add("Golden sword");
        itemInRandomBox.Add("Boot");
        itemInRandomBox.Add("Golden clock");
        itemInRandomBox.Add("Golden heart");
        itemInRandomBox.Add("Mark of Calamity");
    }

    private void Update()
    {
        CrucifixHeal();
        HealthOrbHeal();
    }
    public void PlayerPickUpItem(string itemNameInput)
    {
        itemWaitingToPutInToList = itemNameInput;       // Change item name input
        readyToAdd = true;
        uiC.UpdatePickUpItemUI(itemNameInput);      // Update UI
        uiC.OpenPickUpItemUI(true);     // Open UI
    }

    public bool CheckSameItem(string itemPickUpName)
    {
        if (nextEmptySlotItem > 6)
        {
            return true;
        }

        switch (itemPickUpName)
        {
            case "Crucifix" :
                if (crucifixPickUp)
                {
                    return true;
                }
                break;
            
            case "Health orb" :
                if (healthOrbPickUp)
                {
                    return true;
                }
                break;
            
            case "Shield" :
                if (shieldPickUp)
                {
                    return true;
                }
                break;
            
            case "Syringe" :
                if (syringePickUp)
                {
                    return true;
                }
                break;
            
            case "Knowledge" :
                if (knowledgePickUp)
                {
                    return true;
                }
                break;
            
            case "Golden sword" :
                if (goldenSwordPickUp)
                {
                    return true;
                }
                break;
            
            case "Boot" :
                if (bootPickUp)
                {
                    return true;
                }
                break;
            
            case "Golden clock" :
                if (goldenClockPickUp)
                {
                    return true;
                }
                break;
            
            case "Golden heart" :
                if (goldenHeartPickUp)
                {
                    return true;
                }
                break;
            
            case "Mark of Calamity" :
                if (markOfCalamityPickUp)
                {
                    return true;
                }
                break;
            
            default:
                return true;
        }

        return false;
    }

    public void AddItemInToList()       // Click pick up button
    {
        switch (itemWaitingToPutInToList)
        {
            case "Crucifix":
                readyToAdd = false;
                crucifixSlot = nextEmptySlotItem;
                crucifixPickUp = true;
                itemInRandomBox.Remove("Crucifix");
                ps.ResetCrucifixDeadProtection();
                PutItemIconInEmptySlot(nextEmptySlotItem);
                uiC.OpenPickUpItemUI(false);
                levelUp.AddItemUpgradeList(itemWaitingToPutInToList);
                break;

            case "Health orb":
                readyToAdd = false;
                healthOrbSlot = nextEmptySlotItem;
                healthOrbPickUp = true;
                itemInRandomBox.Remove("Health orb");
                PickUpHealthOrb();
                PutItemIconInEmptySlot(nextEmptySlotItem);
                uiC.OpenPickUpItemUI(false);
                levelUp.AddItemUpgradeList(itemWaitingToPutInToList);
                break;
            
            
            case "Shield" :
                readyToAdd = false;
                shieldSlot = nextEmptySlotItem;
                shieldPickUp = true;
                itemInRandomBox.Remove("Shield");
                ResetShield();
                PutItemIconInEmptySlot(nextEmptySlotItem);
                uiC.OpenPickUpItemUI(false);
                levelUp.AddItemUpgradeList(itemWaitingToPutInToList);
                break;
            
            case "Syringe" :
                readyToAdd = false;
                syringeSlot = nextEmptySlotItem;
                syringePickUp = true;
                itemInRandomBox.Remove("Syringe");
                ResetSyringe();
                PutItemIconInEmptySlot(nextEmptySlotItem);
                uiC.OpenPickUpItemUI(false);
                levelUp.AddItemUpgradeList(itemWaitingToPutInToList);
                break;
            
            case "Knowledge" :
                readyToAdd = false;
                knowledgeSlot = nextEmptySlotItem;
                knowledgePickUp = true;
                itemInRandomBox.Remove("Knowledge");
                PickUpKnowledge();
                PutItemIconInEmptySlot(nextEmptySlotItem);
                uiC.OpenPickUpItemUI(false);
                levelUp.AddItemUpgradeList(itemWaitingToPutInToList);
                break;
            
            case "Golden sword" :
                readyToAdd = false;
                goldenSwordSlot = nextEmptySlotItem;
                goldenSwordPickUp = true;
                itemInRandomBox.Remove("Golden sword");
                PickUpGoldenSword();
                PutItemIconInEmptySlot(nextEmptySlotItem);
                uiC.OpenPickUpItemUI(false);
                levelUp.AddItemUpgradeList(itemWaitingToPutInToList);
                break;
            
            case "Boot" :
                readyToAdd = false;
                bootSlot = nextEmptySlotItem;
                bootPickUp = true;
                itemInRandomBox.Remove("Boot");
                PickUpBoot();
                PutItemIconInEmptySlot(nextEmptySlotItem);
                uiC.OpenPickUpItemUI(false);
                levelUp.AddItemUpgradeList(itemWaitingToPutInToList);
                break;
            
            case "Golden clock" :
                readyToAdd = false;
                goldenClockSlot = nextEmptySlotItem;
                goldenClockPickUp = true;
                itemInRandomBox.Remove("Golden clock");
                PickUpGoldenClock();
                PutItemIconInEmptySlot(nextEmptySlotItem);
                uiC.OpenPickUpItemUI(false);
                levelUp.AddItemUpgradeList(itemWaitingToPutInToList);
                break;
            
            case "Golden heart" :
                readyToAdd = false;
                goldenHeartSlot = nextEmptySlotItem;
                goldenClockPickUp = true;
                itemInRandomBox.Remove("Golden heart");
                PickUpGoldenHeart();
                PutItemIconInEmptySlot(nextEmptySlotItem);
                uiC.OpenPickUpItemUI(false);
                levelUp.AddItemUpgradeList(itemWaitingToPutInToList);
                break;
            
            case "Mark of Calamity" :
                readyToAdd = false;
                markOfCalamitySlot = nextEmptySlotItem;
                markOfCalamityPickUp = true;
                itemInRandomBox.Remove("Mark of Calamity");
                PickUpMarkOfCalamity();
                PutItemIconInEmptySlot(nextEmptySlotItem);
                uiC.OpenPickUpItemUI(false);
                levelUp.AddItemUpgradeList(itemWaitingToPutInToList);
                break;


            default:
                Debug.Log("PlayerPickUpItem(string itemName) Input didn't match with switch()");
                break;
        }
    }

    private void PutItemIconInEmptySlot(int slot)
    {
        nextEmptySlotItem++;
        switch (slot)
        {
            case 1 :
                uiIconItemInPlayerSlot1.ShowItemIcon(itemWaitingToPutInToList);
                break;
            
            case 2 :
                uiIconItemInPlayerSlot2.ShowItemIcon(itemWaitingToPutInToList);
                break;
            
            case 3 :
                uiIconItemInPlayerSlot3.ShowItemIcon(itemWaitingToPutInToList);
                break;
            
            case 4 :
                uiIconItemInPlayerSlot4.ShowItemIcon(itemWaitingToPutInToList);
                break;
            
            case 5 :
                uiIconItemInPlayerSlot5.ShowItemIcon(itemWaitingToPutInToList);
                break;
            
            case 6 :
                uiIconItemInPlayerSlot6.ShowItemIcon(itemWaitingToPutInToList);
                break;
        }
    }

    private void SetActivateCooldownItemSlot(int slotNumber, float cooldown)
    {
        switch (slotNumber)
        {
            case 1 :
                itemEffectArtifact1.StartCountDown(cooldown);
                break;
            
            case 2 :
                itemEffectArtifact2.StartCountDown(cooldown);
                break;
            
            case 3 :
                itemEffectArtifact3.StartCountDown(cooldown);
                break;
            
            case 4 :
                itemEffectArtifact4.StartCountDown(cooldown);
                break;
            
            case 5 :
                itemEffectArtifact5.StartCountDown(cooldown);
                break;
            
            case 6 :
                itemEffectArtifact6.StartCountDown(cooldown);
                break;
            
            default:
                Debug.Log("SetActivateCooldownItemSlot(int slotNumber, float cooldown) didn't match with switch()");
                break;
        }
    }

    // =============================== Weapon - Method Tools ===============================
    
    
    
    // =============================== Weapon - Pistol ===============================

    
    
    // =============================== Item - Crucifix ===============================

    private void CrucifixHeal()
    {
        if (crucifixHealTime > 0 && crucifixReadyToHeal)
        {
            crucifixHealTime--;
            crucifixReadyToHeal = false;
            ps.HealPlayer(crucifixHeal / crucifixHealDuration);
            Invoke(nameof(ResetCrucifixReadyToHeal), 1);
        }
    }

    public void ActivateCrucifix()
    {
        crucifixHealTime = crucifixHealDuration;
        Invoke(nameof(CrucifixFinishCooldown), crucifixCooldown);
        SetActivateCooldownItemSlot(crucifixSlot, crucifixCooldown);
    }

    public void UpdateCrucifixUpGrade(float regen, float cooldown)
    {
        crucifixHeal = regen;
        crucifixCooldown = cooldown;
    }

    private void ResetCrucifixReadyToHeal()
    {
        crucifixReadyToHeal = true;
    }

    private void CrucifixFinishCooldown()
    {
        ps.ResetCrucifixDeadProtection();
    }
    
    // =============================== Item - Health orb ===============================

    private void HealthOrbHeal()
    {
        if (healthOrbPickUp && healthReadyToHeal)
        {
            healthReadyToHeal = false;
            ps.HealPlayer((healthOrbHealPercent / 100) * ps._maxHealth);
            Invoke(nameof(ResetHealthOrbReadyToHeal), 1);
        }
    }

    private void ResetHealthOrbReadyToHeal()
    {
        healthReadyToHeal = true;
    }

    public void UpgradeHealthOrbHealPercent(float newPercent)
    {
        healthOrbHealPercent = newPercent;
    }

    public void PickUpHealthOrb()
    {
        ps.UpgradeStatus(0, 20);
    }
    
    // =============================== Item - Shield ===============================

    private void ResetShield()
    {
        ps.ResetShieldProtection();
    }

    public void ActivateShield()
    {
        SetActivateCooldownItemSlot(shieldSlot, shieldCooldown);
        Invoke(nameof(ResetShield), shieldCooldown);
    }

    public void UpdateShieldUpgrade(float invincibleDuration, float cooldown)
    {
        shieldInvincibleTime = invincibleDuration;
        ps.ChangeShieldInvincibleTime(invincibleDuration);
        shieldCooldown = cooldown;
    }
    
    // =============================== Item - Syringe ===============================

    private void ResetSyringe()
    {
        ps.ResetSyringeBuff();
    }

    public void ActivateSyringeBuff()
    {
        ps.HealPlayer(syringeHeal);
        ps.UpgradeStatus(3, syringeBuffMoveSpeed);
        ps.UpgradeStatus(1, syringeBuffDamage);
        lastSyringeBuffMoveSpeed = syringeBuffMoveSpeed;
        lastSyringeBuffDamage = syringeBuffDamage;
        SetActivateCooldownItemSlot(syringeSlot, syringeCooldown);
        
        Invoke(nameof(DisableSyringeBuff), syringeBuffDuration);
        Invoke(nameof(ResetSyringe), syringeCooldown);
    }

    private void DisableSyringeBuff()
    {
        ps.UpgradeStatus(3, -lastSyringeBuffMoveSpeed);
        ps.UpgradeStatus(1, -lastSyringeBuffDamage);
    }

    public void UpgradeSyringe(float heal, float moveSpeed, float damage, float duration, float cooldown)
    {
        syringeHeal = heal;
        syringeBuffMoveSpeed = moveSpeed;
        syringeBuffDamage = damage;
        syringeBuffDuration = duration;
        syringeCooldown = cooldown;
    }
    
    // =============================== Item - Knowledge ===============================

    private void PickUpKnowledge()
    {
        ps.AddExperienceGainMultiply(knowledgeEXPMultiply);
    }
    public void UpgradeKnowledge(float addMultiply)
    {
        knowledgeEXPMultiply += addMultiply;
        ps.AddExperienceGainMultiply(addMultiply);
    }
    
    // =============================== Item - Golden sword ===============================

    private void PickUpGoldenSword()
    {
        ps.UpgradeStatus(1, goldenSwordBuffDamage);
    }

    public void UpgradeGoldenSword(float addMultiply)
    {
        goldenSwordBuffDamage += addMultiply;
        ps.UpgradeStatus(1, addMultiply);
    }
    
    // =============================== Item - Boot ===============================

    private void PickUpBoot()
    {
        ps.UpgradeStatus(3, bootBuffMoveSpeed);
    }

    public void UpgradeBoot(float addMultiply)
    {
        bootBuffMoveSpeed += addMultiply;
        ps.UpgradeStatus(3, addMultiply);
    }
    
    // =============================== Item - Golden clock ===============================

    private void PickUpGoldenClock()
    {
        ps.UpgradeStatus(4, goldenClockBuffCooldown);
    }

    public void UpgradeGoldenClock(float addMultiply)
    {
        goldenClockBuffCooldown += addMultiply;
        ps.UpgradeStatus(4, addMultiply);
    }
    
    // =============================== Item - Golden heart ===============================

    private void PickUpGoldenHeart()
    {
        goldenHeartReady = true;
        ps.PickUpGoldenHeart();
    }

    public void ActivateGoldenHeart()
    {
        if (goldenHeartReady)
        {
            goldenHeartReady = false;
            ps.UpgradeStatus(0, goldenHeartAddMaxHP);
            ps.UpgradeStatus(1, goldenHeartAddDamage);
            ps.UpgradeStatus(2, goldenHeartAddProjectileSpeed);
            SetActivateCooldownItemSlot(goldenHeartSlot, goldenHeartCooldown);
            Invoke(nameof(ResetGoldenHeartReady), goldenHeartCooldown);
        }
    }

    public void UpgradeGoldenHeart(float addHP, float addDam, float addProSpeed, float cooldown)
    {
        goldenHeartAddMaxHP = addHP;
        goldenHeartAddDamage = addDam;
        goldenHeartAddProjectileSpeed = addProSpeed;
        goldenHeartCooldown = cooldown;
    }

    private void ResetGoldenHeartReady()
    {
        goldenHeartReady = true;
        ps.ResetGoldenHeartNegateDamage();
    }
    
    // =============================== Item - Mark of Calamity ===============================

    private void PickUpMarkOfCalamity()
    {
        gameManager.SetSpawnRate(blueSlimeSpawnRate, redSlimeSpawnRate);
    }

    public void UpgradeMarkOfCalamity(int blueSlimeRate, int redSlimeRate)
    {
        blueSlimeSpawnRate = blueSlimeRate;
        redSlimeSpawnRate = redSlimeRate;
        gameManager.SetSpawnRate(blueSlimeRate, redSlimeRate);
    }
}

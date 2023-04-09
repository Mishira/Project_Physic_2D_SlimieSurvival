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
    [SerializeField] private ItemEffect itemEffectSecondaryWeapon;
    [SerializeField] private ItemEffect itemEffectArtifact1;
    [SerializeField] private ItemEffect itemEffectArtifact2;
    [SerializeField] private ItemEffect itemEffectArtifact3;
    [SerializeField] private ItemEffect itemEffectArtifact4;
    [SerializeField] private ItemEffect itemEffectArtifact5;
    [SerializeField] private ItemEffect itemEffectArtifact6;
    //[SerializeField] private ItemEffect itemEffectItemSlot1;
    
    [Header("===== Med Kit =====")]
    [SerializeField] private float medKitHealPercent = 25;
    [SerializeField] private int medKitHealDuration = 5;
    [SerializeField] private float medKitCooldown = 10;

    [Header("===== Crucifix =====")]
    [SerializeField] private float crucifixHeal = 30;
    [SerializeField] private int crucifixHealDuration = 10;
    [SerializeField] private float crucifixCooldown = 120;

    [Header("===== Key Bind =====")]
    [SerializeField] private KeyCode weapon2Key = KeyCode.Mouse1;
    [SerializeField] private KeyCode weapon2SecondKey = KeyCode.Space;

    [SerializeField] private PlayerStatus ps;
    [SerializeField] private UIController uiC;
    [SerializeField] private LevelUp levelUp;

    // List player item
    //private List<string> playerItemName = new List<string>();
    private string itemWaitingToPutInToList;
    private bool readyToAdd = false;
    private int nextEmptySlotItem = 1;

    // Player Secondary weapon
    private string itemName;
    private bool weaponReadyToUse = true;

    // Med kit
    private int healTime;
    private bool readyToHeal = true;

    // Crucifix
    private int crucifixHealTime;
    private bool crucifixReadyToHeal = true;
    private int crucifixSlot = -1;
    private bool crucifixPickUp = false;

    private void Start()
    {
        itemName = "Med kit";   // Use only for prototype version.
    }

    private void Update()
    {
        if ((Input.GetKeyDown(weapon2Key) || Input.GetKeyDown(weapon2SecondKey)) && weaponReadyToUse)
        {
            UseSecondaryWeapon(itemName);
            weaponReadyToUse = false;
        }
        
        MedKitHeal();
        CrucifixHeal();
    }

    private void ResetWeaponReadyToUse()
    {
        weaponReadyToUse = true;
    }

    public void ChangeSecondaryWeapon(string weaponName)
    {
        itemName = weaponName;
    }

    private void UseSecondaryWeapon(string itemName)
    {
        switch (itemName)
        {
            case "Med kit" :
                healTime = medKitHealDuration;
                Invoke(nameof(ResetWeaponReadyToUse), medKitCooldown);
                itemEffectSecondaryWeapon.StartCountDown(medKitCooldown);
                break;
            
            default:
                Debug.Log("UseSecondaryWeapon(string itemName) input didn't match with switch()");
                break;
        }
    }

    public void PlayerPickUpItem(string itemNameInput)
    {
        itemWaitingToPutInToList = itemNameInput;
        readyToAdd = true;
        uiC.UpdatePickUpItemUI(itemNameInput);
        uiC.OpenPickUpItemUI(true);
    }

    public void AddItemInToList()       // Click pick up button
    {
        switch (itemWaitingToPutInToList)
        {
            case "Crucifix":
                //playerItemName.Add("Crucifix");
                readyToAdd = false;
                ps.ResetCrucifixDeadProtection();
                crucifixSlot = nextEmptySlotItem;
                crucifixPickUp = true;
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

    // =============================== Weapon - Med kit ===============================

    private void MedKitHeal()
    {
        if (healTime > 0 && readyToHeal)
        {
            healTime--;
            readyToHeal = false;
            ps.HealPlayer(((medKitHealPercent / medKitHealDuration) / 100) * ps._maxHealth);
            Invoke(nameof(ResetReadyToHeal), 1);
        }
    }

    private void ResetReadyToHeal()
    {
        readyToHeal = true;
    }
    
    // =============================== Item - Crucifix ===============================

    private void CrucifixHeal()
    {
        if (crucifixHealTime > 0 && crucifixReadyToHeal)
        {
            crucifixHealTime--;
            crucifixReadyToHeal = false;
            ps.HealPlayer(crucifixHeal / crucifixHealDuration);
            Invoke(nameof(ResetcrucifixReadyToHeal), 1);
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

    private void ResetcrucifixReadyToHeal()
    {
        crucifixReadyToHeal = true;
    }

    private void CrucifixFinishCooldown()
    {
        ps.ResetCrucifixDeadProtection();
    }
}

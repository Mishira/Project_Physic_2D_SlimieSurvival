using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ItemController : MonoBehaviour
{
    [Header("===== Cooldown UI =====")]
    [SerializeField] private ItemEffect itemEffectSecondaryWeapon;
    //[SerializeField] private ItemEffect itemEffectItemSlot1;
    
    [Header("===== Med Kit =====")]
    [SerializeField] private float medKitHealPercent = 25;
    [SerializeField] private int medKitHealDuration = 5;
    [SerializeField] private float medKitCooldown = 10;

    [Header("===== Key Bind =====")]
    [SerializeField] private KeyCode weapon2Key = KeyCode.Mouse1;
    [SerializeField] private KeyCode weapon2SecondKey = KeyCode.Space;

    [SerializeField] private PlayerStatus ps;

    private string itemName;
    private bool weaponReadyToUse = true;

    private int healTime;
    private bool readyToHeal = true;

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

    // =============================== Weapon - Med kit ===============================

    private void MedKitHeal()
    {
        if (healTime > 0 && readyToHeal)
        {
            healTime--;
            readyToHeal = false;
            ps.HealPlayer(medKitHealPercent / medKitHealDuration);
            Invoke(nameof(ResetReadyToHeal), 1);
        }
    }

    private void ResetReadyToHeal()
    {
        readyToHeal = true;
    }
}

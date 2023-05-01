using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("===== Basic UI =====")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider experienceBar;
    [SerializeField] private Text playerLevelText;
    [SerializeField] private PlayerStatus ps;
    [SerializeField] private Text timer;

    [Header("===== Item Pick up UI =====")]
    [SerializeField] private GameObject itemPickUpUI;
    [SerializeField] private Text headerText;
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private GameObject iconCrucifix;
    [SerializeField] private GameObject iconHealthOrb;
    [SerializeField] private GameObject iconShield;
    [SerializeField] private GameObject iconSyringe;
    [SerializeField] private GameObject iconKnowledge;
    [SerializeField] private GameObject iconGoldenSword;
    [SerializeField] private GameObject iconBoot;
    [SerializeField] private GameObject iconGoldenClock;
    [SerializeField] private GameObject iconGoldenHeart;
    [SerializeField] private GameObject iconMarkOfCalamity;

    [Header("===== Get Component =====")]
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private LevelUp levelUp;

    private int time;
    private int timeMinute;
    private int timeSecone;
    private int startPlayTime;

    private void Start()
    {
        startPlayTime = (int)Time.time;
        OpenPickUpItemUI(false);
    }

    private void Update()
    {
        TimerNormal();
    }

    private void UpdatePlayerStatus()
    {
        
    }

    public void UpdateHealthBar()
    {
        healthBar.maxValue = ps._maxHealth;
        healthBar.value = ps._health;
    }

    public void UpdateExperienceBar()
    {
        experienceBar.minValue = ps._lastLevelExperienceRequire;
        experienceBar.maxValue = ps._nextLevelExperienceRequire;
        experienceBar.value = ps._playerExperience;
        playerLevelText.text = $"LV : {ps._playerLevel}";
    }

    private void TimerNormal()
    {
        time = (int)Time.time - startPlayTime;

        timeMinute = time / 60;
        timeSecone = time % 60;

        if (timeSecone < 10)
        {
            timer.text = $"{timeMinute} : 0{timeSecone}";
        }
        else
        {
            timer.text = $"{timeMinute} : {timeSecone}";
        }
    }

    public void OpenPickUpItemUI(bool open)
    {
        if (open)
        {
            levelUp.SetTimeScale(0);
            pm.SetPausePlayer(true);
            itemPickUpUI.SetActive(true);
        }
        else
        {
            levelUp.SetTimeScale(1);
            pm.SetPausePlayer(false);
            itemPickUpUI.SetActive(false);
        }
    }

    public void UpdatePickUpItemUI(string pickUpItemName)       // Show UI Pick Up Item
    {
        DisableAllIcon();
        
        switch (pickUpItemName)
        {
            case "Crucifix" :
                iconCrucifix.SetActive(true);
                headerText.text = "Item Found!";
                itemNameText.text = "Crucifix";
                descriptionText.text = "Protect player HP can't go below 1 then give Invincible for 2 second and regen " +
                                       "60 HP in 10 second. Cooldown 120 second";
                break;
            
            case "Health orb" :
                iconHealthOrb.SetActive(true);
                headerText.text = "Item Found!";
                itemNameText.text = "Health orb";
                descriptionText.text = "Max health +20 and Heal 1% max HP every second.";
                break;
            
            case "Shield" :
                iconShield.SetActive(true);
                headerText.text = "Item Found!";
                itemNameText.text = "Shield";
                descriptionText.text = "When player take damage gain Invincible for 1 second. Cooldown 10 second";
                break;
            
            case "Syringe" :
                iconSyringe.SetActive(true);
                headerText.text = "Item Found!";
                itemNameText.text = "Syringe";
                descriptionText.text = "When player take damage heal 10 HP then gain Move speed +20 %, Damage +15 %, " +
                                       "for 5 second. cooldown 30 second";
                break;
            
            case "Knowledge" :
                iconKnowledge.SetActive(true);
                headerText.text = "Item Found!";
                itemNameText.text = "Knowledge";
                descriptionText.text = "Slime drop 35% more EXP.";
                break;
            
            case "Golden sword" :
                iconGoldenSword.SetActive(true);
                headerText.text = "Item Found!";
                itemNameText.text = "Golden sword";
                descriptionText.text = "All player damage +30%";
                break;
            
            case "Boot" :
                iconBoot.SetActive(true);
                headerText.text = "Item Found!";
                itemNameText.text = "Boot";
                descriptionText.text = "Player move speed +40%";
                break;
            
            case "Golden clock" :
                iconGoldenClock.SetActive(true);
                headerText.text = "Item Found!";
                itemNameText.text = "Golden clock";
                descriptionText.text = "Player attack cooldown -10%";
                break;
            
            case "Golden heart" :
                iconGoldenHeart.SetActive(true);
                headerText.text = "Item Found!";
                itemNameText.text = "Golden heart";
                descriptionText.text = "Negate next damage than gain +4 max HP, +2% Damage, +2% projectile speed. Cooldown 16 second";
                break;
            
            case "Mark of Calamity" :
                iconMarkOfCalamity.SetActive(true);
                headerText.text = "Item Found...";
                itemNameText.text = "Mark of Calamity";
                descriptionText.text = "Blue and Red Slime will spawn more...";
                break;
        }
    }

    private void DisableAllIcon()
    {
        iconCrucifix.SetActive(false);
        iconHealthOrb.SetActive(false);
        iconShield.SetActive(false);
        iconSyringe.SetActive(false);
        iconKnowledge.SetActive(false);
        iconGoldenSword.SetActive(false);
        iconBoot.SetActive(false);
        iconGoldenClock.SetActive(false);
        iconGoldenHeart.SetActive(false);
        iconMarkOfCalamity.SetActive(false);
    }
}

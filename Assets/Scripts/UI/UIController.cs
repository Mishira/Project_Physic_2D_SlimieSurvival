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
        }
    }

    private void DisableAllIcon()
    {
        iconCrucifix.SetActive(false);
        iconHealthOrb.SetActive(false);
        iconShield.SetActive(false);
    }
}

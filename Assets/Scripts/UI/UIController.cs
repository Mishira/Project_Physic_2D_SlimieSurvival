using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider experienceBar;
    [SerializeField] private Text playerLevelText;
    [SerializeField] private PlayerStatus ps;
    [SerializeField] private Text timer;

    private int time;
    private int timeMinute;
    private int timeSecone;
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
        time = (int)Time.time;

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
}

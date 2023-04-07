using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("===== Status Setting =====")]
    [SerializeField] private float health = 100;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float damageMultiply = 0;
    [SerializeField] private float attackCooldownMultiply = 0;
    [SerializeField] private float projectileSpeedMultiply = 0;
    [SerializeField] private float moveSpeedMultiply = 0;
    [SerializeField] private float experienceGainMultiply = 0;

    [Header("===== Experience Setting =====")]
    [SerializeField] private int playerLevel = 1;
    [SerializeField] private float playerExperience = 0;
    [SerializeField] private float nextLevelExperienceRequire = 100;
    [SerializeField] private float expRequirementGrowForEachLevel = 20;

    [Header("===== Get Component =====")]
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private UIController uiController;
    [SerializeField] private LevelUp levelUp;

    private float nextLevelEXPBuff;
    private float lastLevelExperienceRequire = 0;
    private bool openLevelUpUI = false;

    public float _health => health;
    public float _maxHealth => maxHealth;
    public int _playerLevel => playerLevel;
    public float _playerExperience => playerExperience;
    public float _nextLevelExperienceRequire => nextLevelExperienceRequire;
    public float _lastLevelExperienceRequire => lastLevelExperienceRequire;

    private void Start()
    {
        health = maxHealth;
        nextLevelEXPBuff = nextLevelExperienceRequire + expRequirementGrowForEachLevel;
        
        CheckLevelUp();
    }

    private void Update()
    {
        CheckLevelUp();
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPickUpExperienceOrb(nextLevelExperienceRequire - playerExperience);
        }
    }

    public void PlayerTakeDamage(float damageTake)
    {
        health = health - damageTake;
        if (health <= 0)
        {
            health = 0;
            pm.SetPlayerDie();
        }
        uiController.UpdateHealthBar();
    }

    private void CheckLevelUp()
    {
        if (playerExperience >= nextLevelExperienceRequire && !openLevelUpUI)
        {
            openLevelUpUI = true;
            playerLevel++;
            lastLevelExperienceRequire = nextLevelExperienceRequire;
            nextLevelExperienceRequire = lastLevelExperienceRequire + nextLevelEXPBuff;
            nextLevelEXPBuff = nextLevelEXPBuff + expRequirementGrowForEachLevel;
            levelUp.PlayerLevelUp();
        }
        uiController.UpdateExperienceBar();
    }

    public void PlayerPickUpExperienceOrb(float exp)
    {
        playerExperience = playerExperience + exp;
        //CheckLevelUp();
    }

    public void ResetOpenLevelUpUI()
    {
        openLevelUpUI = false;
    }
}

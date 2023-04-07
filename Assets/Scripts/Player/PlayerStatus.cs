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
    [SerializeField] private float maxCooldownMultiplyLimit = 70;
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
    [SerializeField] private Bow bow;

    private float nextLevelEXPBuff;
    private float lastLevelExperienceRequire = 0;
    private bool openLevelUpUI = false;
    private bool firstTimeHitCooldownLimit = true;

    public float _health => health;
    public float _maxHealth => maxHealth;
    public int _playerLevel => playerLevel;
    public float _playerExperience => playerExperience;
    public float _nextLevelExperienceRequire => nextLevelExperienceRequire;
    public float _lastLevelExperienceRequire => lastLevelExperienceRequire;
    public float _damageMultiply => damageMultiply;
    public float _attackCooldownMultiply => attackCooldownMultiply;
    public float _projectileSpeedMultiply => projectileSpeedMultiply;
    public float _moveSpeedMultiply => moveSpeedMultiply;

    private void Start()
    {
        health = maxHealth;
        nextLevelEXPBuff = nextLevelExperienceRequire + expRequirementGrowForEachLevel;
        
        CheckLevelUp();
    }

    private void Update()
    {
        CheckLevelUp();
        uiController.UpdateHealthBar();
        
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

    public void UpdateStatus()
    {
        bow.UpdateStatusChange();
        pm.UpdateStatusChange();
    }

    public void UpgradeStatus(int index, float change)
    {
        switch (index)
        {
            case 0 :    // Max health
                maxHealth += change;
                health += change;
                break;
            
            case 1 :    // Damage
                damageMultiply += change;
                break;
            
            case 2 :    // Projectile Speed
                projectileSpeedMultiply += change;
                break;
            
            case 3 :    // Move Speed
                moveSpeedMultiply += change;
                break;
            
            case 4 :    // Cooldown
                attackCooldownMultiply += change;
                if (attackCooldownMultiply >= maxCooldownMultiplyLimit && firstTimeHitCooldownLimit)
                {
                    firstTimeHitCooldownLimit = false;
                    attackCooldownMultiply = maxCooldownMultiplyLimit;
                    levelUp.RemoveCooldownUpgradeStatus();
                }
                else if (attackCooldownMultiply > maxCooldownMultiplyLimit)
                {
                    attackCooldownMultiply = maxCooldownMultiplyLimit;
                }
                break;
            
            default:
                Debug.Log("Upgrade name didn't match with Switch()");
                break;
        }
    }
}

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
    [SerializeField] private ItemController itemController;

    private float nextLevelEXPBuff;
    private float lastLevelExperienceRequire = 0;
    private bool openLevelUpUI = false;
    private bool firstTimeHitCooldownLimit = true;
    private bool crucifixDeadProtection = false;
    private bool playerInvincible = false;
    private float crucifixInvincibleTime = 2;
    private bool shieldDamageProtection = false;
    private float shieldInvincibleTime = 1;

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
        if (!playerInvincible)
        {
            health = health - damageTake;
            if (shieldDamageProtection && health > 0)
            {
                shieldDamageProtection = false;
                playerInvincible = true;
                itemController.ActivateShield();
                Invoke(nameof(DisablePlayerInvincible), shieldInvincibleTime);
            }
        }
        
        if (health <= 0 && crucifixDeadProtection)
        {
            health = 1;
            crucifixDeadProtection = false;
            playerInvincible = true;
            itemController.ActivateCrucifix();
            Invoke(nameof(DisablePlayerInvincible), crucifixInvincibleTime);
        }
        else if (health <= 0)
        {
            health = 0;
            pm.SetPlayerDie();
        }
    }

    public void HealPlayer(float healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
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
    
    public void ResetCrucifixDeadProtection()
    {
        crucifixDeadProtection = true;
    }

    public void ChangeCrucifixInvincibleTime(float duration)
    {
        crucifixInvincibleTime = duration;
    }

    public void DisablePlayerInvincible()
    {
        playerInvincible = false;
    }

    public void ChangeShieldInvincibleTime(float duration)
    {
        shieldInvincibleTime = duration;
    }

    public void ResetShieldProtection()
    {
        shieldDamageProtection = true;
    }
}

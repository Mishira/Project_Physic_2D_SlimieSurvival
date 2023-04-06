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
    [SerializeField] private float nextLevelExperienceRequire = 4;
    [SerializeField] private float expRequirementGrowForEachLevel = 2;

    [Header("===== Get Component =====")]
    [SerializeField] private PlayerMovement pm;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        
    }

    public void PlayerTakeDamage(float damageTake)
    {
        health = health - damageTake;
        if (health < 0)
        {
            health = 0;
            pm.SetPlayerDie();
        }
    }
}

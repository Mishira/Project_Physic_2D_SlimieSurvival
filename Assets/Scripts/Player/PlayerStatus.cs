using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("===== Basic Setting =====")]
    [SerializeField] private float health = 100;
    [SerializeField] private float maxHealth = 100;

    [Header("===== Get Component =====")]
    [SerializeField] private PlayerMovement pm;

    private void Start()
    {
        health = maxHealth;
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

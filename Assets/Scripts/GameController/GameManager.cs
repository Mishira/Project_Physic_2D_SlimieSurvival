using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("===== Enemy Spawn Setting =====")]
    [SerializeField] private bool enemySpawn = true;
    [SerializeField] private int minSpawnCooldown = 1;
    [SerializeField] private int maxSpawnCooldown = 100;
    [SerializeField] private float growEverySecond = 30;
    [SerializeField] private int growSpawnCooldownReduce = 5;
    [SerializeField] private int maxGrowSpawnCooldownReduce = 10;

    [Header("===== Green Slime Setting =====")]
    [SerializeField] private float gsGrowHealth = 2.5f;
    [SerializeField] private float gsGrowAttackDamage = 1.5f;
    [SerializeField] private float gsGrowMoveSpeed = 0.2f;
    [SerializeField] private float gsMaxGrowMoveSpeed = 5;
    
    [SerializeField] private float greenSlimeExperienceDrop = 25;

    //[Header("===== Get Component =====")]
    //[SerializeField] private UIController uiController;
    
    private float greenSlimeGrowHealth = 0;
    private float greenSlimeGrowAttack = 0;
    private float greenSlimeGrowMoveSpeed = 0;

    public bool _enemyspawn => enemySpawn;
    public int _minSpawnCooldown => minSpawnCooldown;
    public int _maxSpawnCooldown => maxSpawnCooldown;
    
    public float _greenSlimeGrowHealth => greenSlimeGrowHealth;
    public float _greenSlimeGrowAttack => greenSlimeGrowAttack;
    public float _greenSlimeGrowMoveSpeed => greenSlimeGrowMoveSpeed;
    public float _greenSlimeExperienceDrop => greenSlimeExperienceDrop;


    private void Start()
    {
        Invoke(nameof(EnemyGrow), growEverySecond);
    }

    private void Update()
    {
        
    }

    private void EnemyGrow()
    {
        maxSpawnCooldown -= growSpawnCooldownReduce;
        if (maxSpawnCooldown < maxGrowSpawnCooldownReduce)
        {
            maxSpawnCooldown = maxGrowSpawnCooldownReduce;
        }
        
        greenSlimeGrowHealth += gsGrowHealth;
        greenSlimeGrowAttack += gsGrowAttackDamage;
        greenSlimeGrowMoveSpeed += gsGrowMoveSpeed;
        if (greenSlimeGrowMoveSpeed > gsMaxGrowMoveSpeed)
        {
            greenSlimeGrowMoveSpeed = gsMaxGrowMoveSpeed;
        }
        
        Invoke(nameof(EnemyGrow), growEverySecond);
    }
    
}

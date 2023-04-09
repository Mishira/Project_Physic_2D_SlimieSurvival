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
    
    [Header("===== Blue Slime Setting =====")]
    [SerializeField] private float bsGrowHealth = 7.5f;
    [SerializeField] private float bsGrowAttackDamage = 1.5f;
    [SerializeField] private float bsGrowMoveSpeed = 0.2f;
    [SerializeField] private float bsMaxGrowMoveSpeed = 5;
    [SerializeField] private float blueSlimeExperienceDrop = 100;
    [SerializeField] private int blueSlimeSpawnChange = 4;
    
    [Header("===== Red Slime Setting =====")]
    [SerializeField] private float rsGrowHealth = 5f;
    [SerializeField] private float rsGrowAttackDamage = 2.25f;
    [SerializeField] private float rsGrowMoveSpeed = 0.2f;
    [SerializeField] private float rsMaxGrowMoveSpeed = 5;
    [SerializeField] private float redSlimeExperienceDrop = 200;
    [SerializeField] private int redSlimeSpawnChange = 1;

    //[Header("===== Get Component =====")]
    //[SerializeField] private UIController uiController;
    
    private float greenSlimeGrowHealth = 0;
    private float greenSlimeGrowAttack = 0;
    private float greenSlimeGrowMoveSpeed = 0;
    private float blueSlimeGrowHealth = 0;
    private float blueSlimeGrowAttack = 0;
    private float blueSlimeGrowMoveSpeed = 0;
    private float redSlimeGrowHealth = 0;
    private float redSlimeGrowAttack = 0;
    private float redSlimeGrowMoveSpeed = 0;

    public bool _enemyspawn => enemySpawn;
    public int _minSpawnCooldown => minSpawnCooldown;
    public int _maxSpawnCooldown => maxSpawnCooldown;
    public int _blueSlimeSpawnChange => blueSlimeSpawnChange;
    public int _redSlimeSpawnChange => redSlimeSpawnChange;
    
    public float _greenSlimeGrowHealth => greenSlimeGrowHealth;
    public float _greenSlimeGrowAttack => greenSlimeGrowAttack;
    public float _greenSlimeGrowMoveSpeed => greenSlimeGrowMoveSpeed;
    public float _greenSlimeExperienceDrop => greenSlimeExperienceDrop;
    
    public float _blueSlimeGrowHealth => blueSlimeGrowHealth;
    public float _blueSlimeGrowAttack => blueSlimeGrowAttack;
    public float _blueSlimeGrowMoveSpeed => blueSlimeGrowMoveSpeed;
    public float _blueSlimeExperienceDrop => blueSlimeExperienceDrop;
    
    public float _redSlimeGrowHealth => redSlimeGrowHealth;
    public float _redSlimeGrowAttack => redSlimeGrowAttack;
    public float _redSlimeGrowMoveSpeed => redSlimeGrowMoveSpeed;
    public float _redSlimeExperienceDrop => redSlimeExperienceDrop;


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
        
        blueSlimeGrowHealth += bsGrowHealth;
        blueSlimeGrowAttack += bsGrowAttackDamage;
        blueSlimeGrowMoveSpeed += bsGrowMoveSpeed;
        if (blueSlimeGrowMoveSpeed > bsMaxGrowMoveSpeed)
        {
            blueSlimeGrowMoveSpeed = bsMaxGrowMoveSpeed;
        }
        
        redSlimeGrowHealth += rsGrowHealth;
        redSlimeGrowAttack += rsGrowAttackDamage;
        redSlimeGrowMoveSpeed += rsGrowMoveSpeed;
        if (redSlimeGrowMoveSpeed > rsMaxGrowMoveSpeed)
        {
            redSlimeGrowMoveSpeed = rsMaxGrowMoveSpeed;
        }
        
        Invoke(nameof(EnemyGrow), growEverySecond);
    }
    
}

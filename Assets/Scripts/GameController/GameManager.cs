using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool enemySpawn = true;
    [SerializeField] private int minSpawnCooldown = 1;
    [SerializeField] private int maxSpawnCooldown = 100;
    
    /*[Header("===== Set Spawn point =====")]
    [SerializeField] private SpawnSlime leftSpawnPoint1;
    [SerializeField] private SpawnSlime leftSpawnPoint2;
    [SerializeField] private SpawnSlime leftSpawnPoint3;
    [SerializeField] private SpawnSlime rightSpawnPoint1;
    [SerializeField] private SpawnSlime rightSpawnPoint2;
    [SerializeField] private SpawnSlime rightSpawnPoint3;*/

    /*[Header("===== Prefab =====")]
    [SerializeField] private GameObject greenSlime;*/
    
    [SerializeField] private float greenSlimeExperienceDrop = 25;

    [Header("===== Get Component =====")]
    //[SerializeField] private UIController uiController;

    private int time;

    public bool _enemyspawn => enemySpawn;
    public int _minSpawnCooldown => minSpawnCooldown;
    public int _maxSpawnCooldown => maxSpawnCooldown;

    public float _greenSlimeExperienceDrop => greenSlimeExperienceDrop;

    private void Update()
    {
        time = (int)Time.time;
    }
    
}

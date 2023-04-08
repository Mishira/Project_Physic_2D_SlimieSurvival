using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSlime : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject greenSlime;
    [SerializeField] private GameManager gm;

    private int minSpawnCooldownTime;
    private int maxSpawnCooldownTime;
    private bool enemySpawn;
    private bool readyToSpawn;
    private int cooldown;

    private void Start()
    {
        readyToSpawn = false;
        UpdateSpawnCooldown();
        cooldown = Random.Range(minSpawnCooldownTime, maxSpawnCooldownTime) / 10;
        Invoke(nameof(ResetReadyToSpawn), cooldown);
    }

    private void Update()
    {
        UpdateSpawnCooldown();
        Spawn();
    }

    private void UpdateSpawnCooldown()
    {
        enemySpawn = gm._enemyspawn;
        minSpawnCooldownTime = gm._minSpawnCooldown;
        maxSpawnCooldownTime = gm._maxSpawnCooldown;
    }

    
    private void Spawn()
    {
        if (readyToSpawn && enemySpawn)
        {
            readyToSpawn = false;
            cooldown = Random.Range(minSpawnCooldownTime, maxSpawnCooldownTime) / 10;
            Instantiate(greenSlime, spawnPoint.position, spawnPoint.rotation);
            Invoke(nameof(ResetReadyToSpawn), cooldown);
        }
    }

    private void ResetReadyToSpawn()
    {
        readyToSpawn = true;
    }
}

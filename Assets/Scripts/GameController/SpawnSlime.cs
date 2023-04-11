using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSlime : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject greenSlime;
    [SerializeField] private GameObject blueSlime;
    [SerializeField] private GameObject redSlime;
    [SerializeField] private GameManager gm;

    private int minSpawnCooldownTime;
    private int maxSpawnCooldownTime;
    private bool enemySpawn;
    private bool readyToSpawn;
    private int cooldown;
    private int spawnCode;
    private int blueSlimeSpawnChange;
    private int redSlimeSpawnChange;

    private void Start()
    {
        readyToSpawn = false;
        UpdateSpawnCooldown();
        cooldown = Random.Range(minSpawnCooldownTime, maxSpawnCooldownTime) / 10;
        Invoke(nameof(ResetReadyToSpawn), cooldown);
    }

    private void Update()
    {
        blueSlimeSpawnChange = gm._blueSlimeSpawnChange;
        redSlimeSpawnChange = gm._redSlimeSpawnChange;
        
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
            spawnCode = Random.Range(1, 101);
            if (100 - (redSlimeSpawnChange + blueSlimeSpawnChange) >= spawnCode)
            {
                Instantiate(greenSlime, spawnPoint.position, spawnPoint.rotation);
            }
            else if (100 - redSlimeSpawnChange >= spawnCode)
            {
                Instantiate(blueSlime, spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                Instantiate(redSlime, spawnPoint.position, spawnPoint.rotation);
            }
            
            Invoke(nameof(ResetReadyToSpawn), cooldown);
        }
    }

    private void ResetReadyToSpawn()
    {
        readyToSpawn = true;
    }
}

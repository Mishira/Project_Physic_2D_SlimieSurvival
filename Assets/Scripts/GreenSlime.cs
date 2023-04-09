using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlime : MonoBehaviour
{
    [Header("===== Basic Setting =====")]
    [SerializeField] private float health;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackCooldown;

    [Header("===== Slime Type Setting =====")]
    [SerializeField] private bool greenSlime;
    [SerializeField] private bool blueSlime;
    [SerializeField] private bool redSlime;

    [Header("===== Get Component =====")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject experienceOrb;
    [SerializeField] private Transform experienceDropPoint;

    private GameObject player;
    private PlayerStatus ps;
    private GameManager gm;
    
    private float playerXDistance;
    private bool readyToAttack = true;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            player = go;
            ps = go.GetComponent<PlayerStatus>();
        }

        if (GameObject.FindGameObjectWithTag("GameController") != null)
        {
            GameObject go1 = GameObject.FindGameObjectWithTag("GameController");
            gm = go1.GetComponent<GameManager>();
        }
        
        SlimeGrowFromGameManager();
    }

    private void Update()
    {
        moveToPlayer();
    }

    private void moveToPlayer()
    {
        playerXDistance = player.transform.position.x - this.transform.position.x;

        if (playerXDistance > 0)
        {
            rb.velocity = new Vector2(1 * moveSpeed, rb.velocity.y);
        }
        else if (playerXDistance < 0)
        {
            rb.velocity = new Vector2(-1 * moveSpeed, rb.velocity.y);
        }
    }

    public void TakeDamage(float damageTake)
    {
        health = health - damageTake;
        if (health <= 0)
        {
            Instantiate(experienceOrb, experienceDropPoint.position, experienceDropPoint.rotation);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && readyToAttack)
        {
            readyToAttack = false;
            ps.PlayerTakeDamage(attackDamage);
            Invoke(nameof(ResetReadyToAttack), attackCooldown);
        }
    }

    private void ResetReadyToAttack()
    {
        readyToAttack = true;
    }

    private void SlimeGrowFromGameManager()
    {
        if (greenSlime)
        {
            health += gm._greenSlimeGrowHealth;
            moveSpeed += gm._greenSlimeGrowMoveSpeed;
            attackDamage += gm._greenSlimeGrowAttack;
        }
        else if (blueSlime)
        {
            health += gm._blueSlimeGrowHealth;
            moveSpeed += gm._blueSlimeGrowMoveSpeed;
            attackDamage += gm._blueSlimeGrowAttack;
        }
        else if (redSlime)
        {
            health += gm._redSlimeGrowHealth;
            moveSpeed += gm._redSlimeGrowMoveSpeed;
            attackDamage += gm._redSlimeGrowAttack;
        }
        
    }
}

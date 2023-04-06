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

    [Header("===== Get Component =====")]
    [SerializeField] private Rigidbody2D rb;

    private GameObject player;
    private PlayerStatus ps;
    
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
}

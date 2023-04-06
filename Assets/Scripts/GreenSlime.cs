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

    [Header("===== Get Component =====")]
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;

    private float playerXDistance;
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
}

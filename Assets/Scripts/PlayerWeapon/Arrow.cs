using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float timeToDestroyOnGround = 2;

    private float arrowDamage;
    private int arrowPiercing;
    
    private Bow bow;
    private Rigidbody2D rb;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Bow") != null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Bow");
            bow = go.GetComponent<Bow>();
            
            arrowDamage = bow._arrowDamage;
            arrowPiercing = bow._piercing;
        }

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            rb.simulated = false;
            Invoke(nameof(DestroyArrow), timeToDestroyOnGround);
        }
        else if (col.CompareTag("Enemy"))
        {
            col.GetComponent<GreenSlime>().TakeDamage(arrowDamage);
            if (arrowPiercing == 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                arrowPiercing--;
            }
        }
        else if (col.CompareTag("Box"))
        {
            col.GetComponent<Box>().ShootBox();
            Destroy(this.gameObject);
        }
    }

    private void DestroyArrow() // Only for Invoke()
    {
        Destroy(this.gameObject);
    }
}

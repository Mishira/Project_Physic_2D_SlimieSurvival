using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("===== Basic Setting =====")]
    [SerializeField] private float launchForce;
    
    [Header("===== Prefab =====")]
    [SerializeField] private GameObject arrow;

    [SerializeField] private KeyCode shootKey = KeyCode.Mouse0;
    
    [Header("===== Get Component =====")]
    [SerializeField] private Transform shotPoint;
    [SerializeField] private PlayerMovement pm;

    private void Update()
    {
        Vector2 bowPosition = transform.position;

        if (Input.GetKeyDown(shootKey))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (pm._isFaceingRight)
        {
            GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
            newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
        }
        else
        {
            GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
            newArrow.GetComponent<Rigidbody2D>().velocity = -transform.right * launchForce;
        }
        
    }
}

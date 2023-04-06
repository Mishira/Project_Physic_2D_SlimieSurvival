using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("===== Basic Setting =====")] 
    [SerializeField] private float arrowDamage;
    [SerializeField] private float launchForce;
    [SerializeField] private int piercing = 0;
    [SerializeField] private float shootCoolDown;
    
    [Header("===== Prefab =====")]
    [SerializeField] private GameObject arrow;

    [Header("===== Key Bind =====")]
    [SerializeField] private KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode autoFireKey = KeyCode.C;

    [Header("===== Get Component =====")]
    [SerializeField] private Transform shotPoint;
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private Animator ani;

    private bool readyToShoot = true;
    private bool autoFire = false;

    public float _arrowDamage => arrowDamage;
    public int _piercing => piercing;

    private void Update()
    {
        if (Input.GetKeyDown(autoFireKey))
        {
            autoFire = !autoFire;
        }

        if ((Input.GetKey(shootKey) || autoFire) && readyToShoot)
        {
            Shoot();
            Invoke(nameof(ResetReadyToShoot), shootCoolDown);
        }
    }

    private void Shoot()
    {
        if (pm._isFaceingRight)
        {
            GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
            newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
            readyToShoot = false;
        }
        else
        {
            GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation);
            newArrow.GetComponent<Rigidbody2D>().velocity = -transform.right * launchForce;
            readyToShoot = false;
        }
    }

    private void ResetReadyToShoot()
    {
        readyToShoot = true;
    }
}

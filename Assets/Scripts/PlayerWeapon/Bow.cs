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
    [SerializeField] private bool autoFire = true;
    
    [Header("===== Prefab =====")]
    [SerializeField] private GameObject arrow;

    [Header("===== Key Bind =====")]
    [SerializeField] private KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode autoFireKey = KeyCode.C;

    [Header("===== Get Component =====")]
    [SerializeField] private Transform shotPoint;
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private PlayerStatus ps;
    //[SerializeField] private Animator ani;

    private float defaultArrowDamage;
    private float defaultLaunchForce;
    private float defaultShootCooldown;
    
    private bool readyToShoot = false;

    public float _arrowDamage => arrowDamage;
    public int _piercing => piercing;

    private void Start()
    {
        defaultArrowDamage = arrowDamage;
        defaultLaunchForce = launchForce;
        defaultShootCooldown = shootCoolDown;
        Invoke(nameof(ResetReadyToShoot), 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(autoFireKey))
        {
            autoFire = !autoFire;
        }

        if ((Input.GetKey(shootKey) || autoFire) && readyToShoot && !pm._pausePlayer)
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

    public void UpdateStatusChange()
    {
        arrowDamage = defaultArrowDamage * ((ps._damageMultiply + 100) / 100);
        launchForce = defaultLaunchForce * ((ps._projectileSpeedMultiply + 100) / 100);
        shootCoolDown = defaultShootCooldown * ((100 - ps._attackCooldownMultiply) / 100);
    }

    public void UpgradeBow(float damage, float speed, float cooldown, int pierce)
    {
        defaultArrowDamage += damage;
        defaultLaunchForce += speed;
        defaultShootCooldown -= cooldown;
        piercing += pierce;
        
        UpdateStatusChange();
    }
}

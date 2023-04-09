using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIconShowInUpgradeSlot : MonoBehaviour
{
    [SerializeField] private GameObject damage;
    [SerializeField] private GameObject projectileSpeed;
    [SerializeField] private GameObject cooldown;
    [SerializeField] private GameObject moveSpeed;
    [SerializeField] private GameObject health;

    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject crucifix;

    public void ShowIcon(string itemName)
    {
        switch (itemName)
        {
            case "Increase player damage" :
                DisableAll();
                damage.SetActive(true);
                break;
            
            case "Increase player projectile speed" :
                DisableAll();
                projectileSpeed.SetActive(true);
                break;

            case "Reduce arrow cooldown" :
                DisableAll();
                cooldown.SetActive(true);
                break;

            case "Increase player move speed" :
                DisableAll();
                moveSpeed.SetActive(true);
                break;

            case "Increase player max health" :
                DisableAll();
                health.SetActive(true);
                break;

            case "Bow" :
                DisableAll();
                bow.SetActive(true);
                break;
            
            case "Crucifix" :
                DisableAll();
                crucifix.SetActive(true);
                break;

            
            default:
                Debug.Log("ShowIcon(string itemName) Input didn't match with switch");
                break;
        }
    }

    public void DisableAll()
    {
        damage.SetActive(false);
        projectileSpeed.SetActive(false);
        cooldown.SetActive(false);
        moveSpeed.SetActive(false);
        health.SetActive(false);
        bow.SetActive(false);
        crucifix.SetActive(false);
    }
}

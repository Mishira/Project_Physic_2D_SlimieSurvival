using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Box : MonoBehaviour
{
    [Header("===== Prefab =====")]
    [SerializeField] private GameObject crucifix;
    [SerializeField] private GameObject healthOrb;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject syringe;
    [SerializeField] private GameObject knowledge;
    [SerializeField] private GameObject goldenSword;
    [SerializeField] private GameObject boot;
    [SerializeField] private GameObject goldenClock;
    [SerializeField] private GameObject goldenHeart;
    [SerializeField] private GameObject markOfCalamity;

    [Header("===== Drop Item Position =====")]
    [SerializeField] private Transform dropPosition;
    
    private ItemController itemController;

    private int number;
    private string spawnItem;
    
    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("ItemController");
        itemController = go.GetComponent<ItemController>();
    }

    public void ShootBox()
    {
        RandomSpawnItem();
        Destroy(this.gameObject);
    }

    private void RandomSpawnItem()
    {
        if (itemController._itemInRandomBox.Count == 0)
        {
            return;
        }
        
        number = Random.Range(0, itemController._itemInRandomBox.Count);
        spawnItem = itemController._itemInRandomBox[number];

        switch (spawnItem)
        {
            case "Crucifix" :
                Instantiate(crucifix, dropPosition.position, dropPosition.rotation);
                break;
            
            case "Health orb" :
                Instantiate(healthOrb, dropPosition.position, dropPosition.rotation);
                break;
            
            case "Shield" :
                Instantiate(shield, dropPosition.position, dropPosition.rotation);
                break;

            case "Syringe" :
                Instantiate(syringe, dropPosition.position, dropPosition.rotation);
                break;

            case "Knowledge" :
                Instantiate(knowledge, dropPosition.position, dropPosition.rotation);
                break;

            case "Golden sword" :
                Instantiate(goldenSword, dropPosition.position, dropPosition.rotation);
                break;
            
            case "Boot" :
                Instantiate(boot, dropPosition.position, dropPosition.rotation);
                break;
            
            case "Golden clock" :
                Instantiate(goldenClock, dropPosition.position, dropPosition.rotation);
                break;
            
            case "Golden heart" :
                Instantiate(goldenHeart, dropPosition.position, dropPosition.rotation);
                break;
            
            case "Mark of Calamity" :
                Instantiate(markOfCalamity, dropPosition.position, dropPosition.rotation);
                break;

        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Box : MonoBehaviour
{
    [Header("===== Item Prefab =====")]
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

    [SerializeField] private GameObject expOrb;

    [Header("===== Drop Item Position =====")]
    [SerializeField] private Transform dropPosition;
    
    private ItemController itemController;

    private int itemIndex;
    private int spawnCode;
    private string spawnItemName;
    
    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("ItemController");
        itemController = go.GetComponent<ItemController>();
    }

    public void ShootBox()
    {
        SpawnItem();
        Destroy(this.gameObject);
    }
    
    private void RandomSpawnItem()
    {
        if (itemController._nextEmptySlotItem < 7)
        {
            
        }
    }

    private void SpawnItem()
    {
        if (itemController._itemInRandomBox.Count == 0 || itemController._nextEmptySlotItem > 6)
        {
            Instantiate(expOrb, dropPosition.position, dropPosition.rotation);
            return;
        }
        
        itemIndex = Random.Range(0, itemController._itemInRandomBox.Count);
        spawnItemName = itemController._itemInRandomBox[itemIndex];

        switch (spawnItemName)
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

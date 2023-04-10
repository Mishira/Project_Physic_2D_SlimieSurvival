using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private float despawnTime = 30;

    private ItemController ic;

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("ItemController");
        ic = go.GetComponent<ItemController>();
        
        Invoke(nameof(DespawnItem), despawnTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (!ic.CheckSameItem(itemName))
            {
                ic.PlayerPickUpItem(itemName);
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void DespawnItem()
    {
        Destroy(this.gameObject);
    }
    
}

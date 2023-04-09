using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private string itemName;

    private ItemController ic;

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("ItemController");
        ic = go.GetComponent<ItemController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            ic.PlayerPickUpItem(itemName);
            Destroy(this.gameObject);
        }
    }
    
}

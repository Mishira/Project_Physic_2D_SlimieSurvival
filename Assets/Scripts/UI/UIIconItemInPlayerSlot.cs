using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIconItemInPlayerSlot : MonoBehaviour
{
    [SerializeField] private GameObject crucifix;
    [SerializeField] private GameObject healthOrb;
    [SerializeField] private GameObject shield;

    public void ShowItemIcon(string itemShowName)
    {
        switch (itemShowName)
        {
            case "Crucifix" :
                crucifix.SetActive(true);
                break;
            
            case "Health orb" :
                healthOrb.SetActive(true);
                break;
            
            case "Shield" :
                shield.SetActive(true);
                break;
            
            
            default:
                Debug.Log("ShowItemIcon(string itemShowName) Input didn't match with switch");
                break;
        }
    }

}

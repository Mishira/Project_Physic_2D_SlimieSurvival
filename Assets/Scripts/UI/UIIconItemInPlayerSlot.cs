using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIconItemInPlayerSlot : MonoBehaviour
{
    [SerializeField] private GameObject crucifix;
    [SerializeField] private GameObject healthOrb;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject syringe;
    [SerializeField] private GameObject knowledge;
    [SerializeField] private GameObject goldenSword;

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
            
            case "Syringe" :
                syringe.SetActive(true);
                break;
            
            case "Knowledge" :
                knowledge.SetActive(true);
                break;
            
            case "Golden sword" :
                goldenSword.SetActive(true);
                break;
            
            
            default:
                Debug.Log("ShowItemIcon(string itemShowName) Input didn't match with switch");
                break;
        }
    }

}

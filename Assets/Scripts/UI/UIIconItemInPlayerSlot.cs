using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIconItemInPlayerSlot : MonoBehaviour
{
    [SerializeField] private GameObject crucifix;

    public void ShowItemIcon(string itemShowName)
    {
        switch (itemShowName)
        {
            case "Crucifix" :
                crucifix.SetActive(true);
                break;
            
            default:
                Debug.Log("ShowItemIcon(string itemShowName) Input didn't match with switch");
                break;
        }
    }

}

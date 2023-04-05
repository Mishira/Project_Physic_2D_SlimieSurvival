using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPlayer : MonoBehaviour
{
    [SerializeField] private KeyCode warpKey = KeyCode.P;
    [SerializeField] private GameObject player;

    private void Update()
    {
        if (Input.GetKeyDown(warpKey))
        {
            player.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 
                this.transform.position.z);
        }
    }
}

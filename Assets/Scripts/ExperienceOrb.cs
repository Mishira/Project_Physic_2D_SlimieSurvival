using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    [SerializeField] private float experience = 1;
    [SerializeField] private bool greenSlime = false;
    [SerializeField] private bool blueSlime = false;
    [SerializeField] private bool redSlime = false;
    
    private PlayerStatus ps;
    private GameManager gm;
    
    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            ps = go.GetComponent<PlayerStatus>();
        }

        if (GameObject.FindGameObjectWithTag("GameController") != null)
        {
            GameObject go1 = GameObject.FindGameObjectWithTag("GameController");
            gm = go1.GetComponent<GameManager>();

            if (greenSlime)
            {
                experience = gm._greenSlimeExperienceDrop;
            }
            else if (blueSlime)
            {
                experience = gm._blueSlimeExperienceDrop;
            }
            else if (redSlime)
            {
                experience = gm._redSlimeExperienceDrop;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            ps.PlayerPickUpExperienceOrb(experience);
            Destroy(this.gameObject);
        }
    }
}

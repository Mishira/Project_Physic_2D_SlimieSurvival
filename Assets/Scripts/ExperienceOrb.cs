using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    [SerializeField] private float experience = 1;
    
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

            experience = gm._greenSlimeExperienceDrop;
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

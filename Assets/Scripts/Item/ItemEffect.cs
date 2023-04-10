using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEffect : MonoBehaviour
{
    //[SerializeField] private GameObject handler;
    [SerializeField] private Image image;
    
    //[SerializeField] private UIEffectController uiEC;
    
    private float durationEffect;
    private float timeEnd;
    private float timeLeft;
    private bool nowCountdown = false;

    private void Update()
    {
        if (nowCountdown)
        {
            timeLeft = timeEnd - Time.time;
            image.fillAmount = timeLeft / durationEffect;
            if (timeLeft <= 0)
            {
                nowCountdown = false;
                image.fillAmount = 0;
                timeLeft = 0;
            }
        }
    }

    public void StartCountDown(float durationEffect)
    {
        nowCountdown = true;
        this.durationEffect = durationEffect;
        timeEnd = Time.time + durationEffect;
    }
}

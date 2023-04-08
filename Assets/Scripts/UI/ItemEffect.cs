using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEffect : MonoBehaviour
{
    [SerializeField] private Image image;
    //[SerializeField] private UIEffectController uiEC;

    private string effectName;
    private float durationEffect;
    private float timeEnd;
    private float timeLeft;
    private bool nowCountdown = false;

    private void Update()
    {
        if (nowCountdown)
        {
            timeLeft = timeEnd - Time.time;
            image.fillAmount = timeLeft - timeEnd;
            if (timeLeft <= 0)
            {
                nowCountdown = false;
                image.fillAmount = 1;
                //uiEC.EndEffect(effectName);
            }
        }
    }

    public void StartCountDown(string effectName, float durationEffect)
    {
        this.effectName = effectName;
        nowCountdown = true;
        timeEnd = Time.time + durationEffect;
        //uiEC.ActiveEffect(this.effectName);
    }
}

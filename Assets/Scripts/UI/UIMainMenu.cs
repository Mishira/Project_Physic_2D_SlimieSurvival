using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject uiMainMenu;
    [SerializeField] private GameObject mainMenuObj;

    [SerializeField] private GameObject uiHowToPlay;
    [SerializeField] private GameObject uiCredit;

    private void Start()
    {
        uiMainMenu.SetActive(true);
        mainMenuObj.SetActive(true);
        
        uiHowToPlay.SetActive(false);
        uiCredit.SetActive(false);
    }

    public void HowToPlayButton()
    {
        mainMenuObj.SetActive(false);
        uiMainMenu.SetActive(false);
        
        uiHowToPlay.SetActive(true);
    }

    public void CreditButton()
    {
        mainMenuObj.SetActive(false);
        uiMainMenu.SetActive(false);
        
        uiCredit.SetActive(true);
    }

    public void BackToMainMenu()
    {
        uiHowToPlay.SetActive(false);
        uiCredit.SetActive(false);
        
        mainMenuObj.SetActive(true);
        uiMainMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

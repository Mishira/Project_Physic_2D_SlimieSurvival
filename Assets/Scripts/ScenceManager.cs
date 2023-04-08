using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceManager : MonoBehaviour
{
    public void GoToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GotoMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}

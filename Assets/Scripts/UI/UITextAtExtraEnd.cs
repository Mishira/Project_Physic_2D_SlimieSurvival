using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextAtExtraEnd : MonoBehaviour
{
    [SerializeField] private GameObject textBox;
    [SerializeField] private Text showText;
    [SerializeField] private ScenceManager _scenceManager;
    [SerializeField] private KeyCode nextTextKey = KeyCode.Mouse0;
    private bool readyToContinue = false;
    private int textIndex = 0;

    [SerializeField] private List<string> chatLog = new List<string>();

    private void Start()
    {
        Invoke(nameof(OpenChatLog), 3);
    }

    private void Update()
    {
        if (Input.GetKeyDown(nextTextKey) && readyToContinue)
        {
            textIndex++;
            if (textIndex < chatLog.Count)
            {
                showText.text = chatLog[textIndex];
                readyToContinue = false;
                Invoke(nameof(ResetReadyToContinue), 0.35f);
            }
            else if (textIndex >= chatLog.Count)
            {
                _scenceManager.GoToGameOverScene();
            }
        }
    }

    private void OpenChatLog()
    {
        textBox.SetActive(true);
        showText.text = chatLog[textIndex];
        readyToContinue = true;
    }

    private void ResetReadyToContinue()
    {
        readyToContinue = true;
    }
}

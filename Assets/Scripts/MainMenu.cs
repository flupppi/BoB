using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu
{
    public Button startGameButton;
    public Button exitGameButton;
    public Button optionsButton;
    public Button controlsButton;

    public void Awake()
    {
        base.Awake();
        startGameButton.onClick.AddListener(OnStartButtonClicked);
        exitGameButton.onClick.AddListener(OnExitButtonClicked);
        optionsButton.onClick.AddListener(OnOptionsButtonClicked);
        controlsButton.onClick.AddListener(OnControlsButtonClicked);

    }

    private void OnControlsButtonClicked()
    {
        MenuManager.ShowMenu("Description");
    }

    public void Start()
    {
        base.Start();
    }

    private void OnStartButtonClicked()
    {
        GameManager.StartGame();   
    }

    private void OnExitButtonClicked()
    {
        GameManager.EndGame();
    }
    private void OnOptionsButtonClicked()
    {
        MenuManager.ShowMenu("Options");
    }
}

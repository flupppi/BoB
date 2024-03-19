using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : Menu
{

    public Button backToMainMenuButton;
    

    public void Awake()
    {
        base.Awake();

        backToMainMenuButton.onClick.AddListener(BackToMainMenuButtonClicked);
    }

    public void Start()
    {
        base.Start();
    }

    private void BackToMainMenuButtonClicked()
    {
        MenuManager.ShowMenu("Main Menu");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameButtonClicked()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void OptionsButtonClicked()
    {
        ShowMenu("Options");
    }

    private void ShowMenu(string v)
    {
        Debug.Log("Show Menu not yet implemented");
    }

    public void ExitButtonClicked()
    {
        Application.Quit();

    }

    public void ContinueButtonClicked()
    {
        Debug.Log("Continue not yet implemented");
    }

    public void ToMainMenuButtonClicked()
    {
        SceneManager.LoadScene("Menu Scene");

    }
}

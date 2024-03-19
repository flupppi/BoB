using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private static List<Menu> menus = new();

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        foreach (Menu menu in FindObjectsOfType<Menu>())
        {
            menus.Add(menu);
        }

    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        menus.Clear();
        foreach (Menu menu in FindObjectsOfType<Menu>())
        {
            menus.Add(menu);
        }
    }



    void SetUIActive(bool active)
    {
        if (!active)
        {
            foreach (var otherMenu in menus)
            {
                if (otherMenu.gameObject.activeInHierarchy)
                {
                    otherMenu.menuWillDisappear.Invoke();
                }
                otherMenu.gameObject.SetActive(false);
            }
        }
        else
        {
            ShowMenu("Home");
        }
    }


    public static void ShowMenu(string menuName)
    {
        Menu menu = SearchMenu(menuName);
        if (menu != null)
            ShowMenu(menu);
        else
            Debug.LogError("Menu: " + menuName + " is not a Menu!");
    }
    private static Menu SearchMenu(string menuName)
    {
        foreach (Menu menu in menus)
            if (menu.menuName == menuName)
                return menu;
        Debug.LogError("Menu: " + menuName + " is not a Menu!");
        return null;
    }

    public static void ShowMenu(Menu menuToShow)
    {
        if (menus.Contains(menuToShow) == false)
        {
            Debug.LogErrorFormat("{0} is not in the list of menus", menuToShow.name);
            return;
        }
        foreach (var otherMenu in menus)
        {
            if (otherMenu == menuToShow)
            {
                otherMenu.gameObject.SetActive(true);
                otherMenu.menuDidAppear.Invoke();
            }
            else
            {
                if (otherMenu.gameObject.activeInHierarchy)
                {
                    otherMenu.menuWillDisappear.Invoke();
                }
                otherMenu.gameObject.SetActive(false);
            }
        }
    }

    public void ToggleMenuAdditively(string menuName)
    {
        Menu menu = SearchMenu(menuName);
        if (menu != null)
            ToggleMenuAdditively(menu);
        else
            Debug.LogError("Menu: " + menuName + " is not a Menu!");
    }
    public void ToggleMenuAdditively(Menu menuToShow)
    {
        if (menus.Contains(menuToShow) == false)
        {
            Debug.LogErrorFormat("{0} is not in the list of menus", menuToShow.name);
            return;
        }
        if (menuToShow.gameObject.activeSelf)
        {
            menuToShow.menuWillDisappear.Invoke();
            menuToShow.gameObject.SetActive(false);
        }
        else
        {
            foreach (var otherMenu in menus)
            {
                if (otherMenu == menuToShow)
                {
                    otherMenu.gameObject.SetActive(true);
                    otherMenu.menuDidAppear.Invoke();
                }
                else
                {
                    if (otherMenu.gameObject.activeInHierarchy)
                    {
                        if (otherMenu.gameObject.TryGetComponent<Menu>(out _))
                        {
                            otherMenu.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
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

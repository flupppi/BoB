using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    public string menuName;
    public Vector2 menuPosition;
    // Invoked when a menu appears onscreen.
    public UnityEvent menuDidAppear = new();
    // Invoked when a menu is removed from the screen.
    public UnityEvent menuWillDisappear = new();
    public  RectTransform rectTransform;

    public void Awake()
    {
        menuDidAppear.AddListener(OnMenuDidAppear);
        menuWillDisappear.AddListener(OnMenuWillDisappear);

    }

    public void OnMenuWillDisappear()
    {
        rectTransform.anchoredPosition = new Vector2(3000, 3000);
    }

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();

    }

    public void OnMenuDidAppear()
    {

        rectTransform.anchoredPosition = menuPosition;
    }

    
}

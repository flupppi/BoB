using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDMenu : Menu
{
    [Range(0, 1)]
    public float healthFill = 1.0f;

    [Range(0, 1)]
    public float dashFill = 1.0f;

    [Range(0, 1)]
    public float cheerFill = 1.0f;

    public Slider healthBar;
    public Slider cheerBar;
    public Slider dashBar;
    public TMP_Text dashCounter;


    [Header("Skills")]

    public Slider fillIndicatorContinous;
    public Slider fillIndicatorCharged;
    public Slider fillIndicatorAerial;
    public Slider fillIndicatorClose;

    public Image skillIconConinous;
    public Image skillIconCharged;
    public Image skillIconAerial;
    public Image skillIconClose;

    [Range(0, 1)]
    public float cooldownContinous;
    [Range(0, 1)]
    public float cooldownCharged;
    [Range(0, 1)]
    public float cooldownAerial;
    [Range(0, 1)] 
    public float cooldownClose;


    public void Awake()
    {
        base.Awake();


    }


    public void Start()
    {
        base.Start();
    }
    public void Update()
    {
        healthBar.value = healthFill;
        dashBar.value = dashFill;
        cheerBar.value = cheerFill;

        fillIndicatorAerial.value = cooldownAerial;
        fillIndicatorCharged.value = cooldownCharged;
        fillIndicatorContinous.value = cooldownContinous;
        fillIndicatorClose.value = cooldownClose;

    }
}

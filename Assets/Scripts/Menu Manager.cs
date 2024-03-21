using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private static List<Menu> menus = new();
    [SerializeField] private UpgradeSystem upgradeSystem;
    [SerializeField] private RoundSystem roundSystem;
    [SerializeField] private GameObject player;

    private HealthComponent healthComponent;
    private AbilityHolder abilityHolder;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        foreach (Menu menu in FindObjectsOfType<Menu>())
        {
            menus.Add(menu);
        }

    }

    void Start() {
        HUDMenu hud = (HUDMenu)SearchMenu("Game HUD");
        UpgradeMenu upgradeMenu = (UpgradeMenu)SearchMenu("Upgrade");

        healthComponent = player.GetComponent<HealthComponent>();
        SetHealth(hud);
        abilityHolder = player.GetComponent<AbilityHolder>();
        SetAbilitiesIcons(hud);

        healthComponent.OnHealthChange += () => {
            SetHealth(hud);
        };

        upgradeSystem.OnOpenUpgradeWindow += (upgrades) => {
            upgradeMenu.gameObject.SetActive(false);
            ToggleMenuAdditively("Upgrade");
            

            for (int i = 0; i < upgradeMenu.slots.Length; i++) {
                upgradeMenu.slots[i].icon = upgrades[i]?.abilityIcon;
                // upgradeMenu.slots[i].title.text = upgrades[i]?.abilityName;
                upgradeMenu.slots[i].description.text = upgrades[i]?.description;
            }
        };

        upgradeSystem.OnCloseWindow += () => {
            ToggleMenuAdditively("Upgrade");
            ShowMenu("Game HUD");
        };

        upgradeSystem.OnUpgrade += () => { SetAbilitiesIcons(hud); };

        abilityHolder.OnCooldownUpdate += (cooldowns) => {
            hud.cooldownNormal = GetAbilityCooldown(0, cooldowns);
            hud.cooldownHeavy = GetAbilityCooldown(1, cooldowns);
            hud.cooldownAOE = GetAbilityCooldown(2, cooldowns);
            hud.cooldownShortDistance = GetAbilityCooldown(3, cooldowns);
            hud.dashFill = 1 - GetAbilityCooldown(4, cooldowns);
        };

        upgradeMenu.slots[0].GetComponent<Button>().onClick = GenerateSelectButtonEvent(upgradeSystem, 0);
        upgradeMenu.slots[1].GetComponent<Button>().onClick = GenerateSelectButtonEvent(upgradeSystem, 1);
        upgradeMenu.slots[2].GetComponent<Button>().onClick = GenerateSelectButtonEvent(upgradeSystem, 2);

        // roundSystem.OnFinish += (() => ShowMenu("EndScreen"));
    }

    private Button.ButtonClickedEvent GenerateSelectButtonEvent(UpgradeSystem upgradeSystem, int number) {
        Button.ButtonClickedEvent buttonEvent = new Button.ButtonClickedEvent();
        buttonEvent.AddListener((() => upgradeSystem.SelectAbility(number)));

        return buttonEvent;
    }

    private void SetAbilitiesIcons(HUDMenu hud) {
        hud.skillIconNormal = abilityHolder.Abilities[0]?.abilityIcon;
        hud.skillIconHeavy = abilityHolder.Abilities[1]?.abilityIcon;
        hud.skillIconAOE = abilityHolder.Abilities[2]?.abilityIcon;
        hud.skillIconShortDistance = abilityHolder.Abilities[3]?.abilityIcon;
    }

    private float GetAbilityCooldown(int ability, in float[] currentCooldown) {
        if (abilityHolder.Abilities[ability]) {
            return Get01Value(currentCooldown[ability], abilityHolder.Abilities[ability].cooldown);
        }

        return 1.0f;
    }

    private void SetHealth(HUDMenu hud) {
        hud.healthFill = Get01Value(healthComponent.Health, healthComponent.MaxHealth);
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

    private float Get01Value(float value, float max) {
        return value / max;
    }
}

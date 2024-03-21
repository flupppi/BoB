using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class UpgradeSystem : MonoBehaviour {
    [SerializeField] private AbilityBase[] m_startAbilities;
    private bool m_status = false;
    private AbilityBase[] m_upgradableAbilites = new AbilityBase[3];
    private AbilityHolder m_abilityHolder;

    public event Action<AbilityBase[]> OnOpenUpgradeWindow;
    public event Action OnCloseWindow;
    public event Action OnUpgrade;

    public void Enable() {
        m_status = true;
    }

    public void Disable() {
        m_status = false;
    }

    public AbilityBase[] GetRandomUpgrades() {
        Array.Clear(m_upgradableAbilites, 0, m_upgradableAbilites.Length);
        List<AbilityBase> abilityPool = new ();

        for (int i = 0; i < m_startAbilities.Length; i++) {
            if(m_startAbilities[i])
                abilityPool.Add(m_startAbilities[i]);
        }

        if (m_abilityHolder != null) {
            foreach (AbilityBase currentAbility in m_abilityHolder.Abilities) {
                if (currentAbility) {
                    // Debug.LogError($"Upgrades: {currentAbility.Upgrades.Length}");
                    foreach (AbilityBase currentAbilityUpgrade in currentAbility.Upgrades)
                    {
                        if (currentAbilityUpgrade) {
                            abilityPool.Add(currentAbilityUpgrade);
                            Debug.LogError($"{currentAbilityUpgrade.abilityName}");
                        }
                    }
                }
            }
        }

        // Debug.LogError($"AbilityPool: {abilityPool.Count}" );

        List<int> randomNumbers = GetNewRandom(abilityPool.Count);

        for (int i = 0; i < m_upgradableAbilites.Length; i++) {
            // Eventuell Checken ob kein Index doppelt vorhanden ist
            //int random =  Random.Range(0, abilityPool.Count);
            
            m_upgradableAbilites[i] = abilityPool[randomNumbers[i]];
        }

        return m_upgradableAbilites;
    }

    private List<int> GetNewRandom(int max) {
        List<int> randomNumbers = new List<int>();

        if (max < 3) {
            Debug.LogError("Max zu klein");
            return new List<int>();
        }

        while (randomNumbers.Count < 3)
        {
            int random = Random.Range(0, max);
            if (!(HasNumber(randomNumbers, random)))
            {
                randomNumbers.Add(random);
            }
        }

        return randomNumbers;
    }

    private bool HasNumber(List<int> list, int number) {
        foreach (int currNumber in list) {
            if (currNumber == number) {
                return true;
            }
        }

        return false;
    }

    public void SelectAbility(int ability) {
        int index = Array.IndexOf(m_startAbilities, m_upgradableAbilites[ability]);
        if (index != -1) {
            m_startAbilities[index] = null;
        }
        UpgradeAbility(ability);
        Disable();
        OnCloseWindow?.Invoke();
        m_abilityHolder.gameObject.GetComponent<PlayerInput>().enabled = true;
    }

    private void UpgradeAbility(int ability) {
        m_abilityHolder.UpgradeAbility(m_upgradableAbilites[ability]);
        OnUpgrade?.Invoke();
    }

    void OnTriggerEnter(Collider triggerCollider) {
        if (m_status && triggerCollider.gameObject.tag == "Player") {
            m_abilityHolder = triggerCollider.gameObject.GetComponent<AbilityHolder>();
            PrintRandomAbilities();
            // GetRandomUpgrades();
            OnOpenUpgradeWindow?.Invoke(m_upgradableAbilites);
            m_abilityHolder.gameObject.GetComponent<PlayerInput>().enabled = false;
        }
    }

    [ContextMenu("PrintRandomAbilites")]
    private void PrintRandomAbilities() {
        GetRandomUpgrades();

        foreach (var ability in m_upgradableAbilites) {
            Debug.LogError(ability);
        }
    }

    [ContextMenu("Select 0")]
    private void Select0() {
        SelectAbility(0);
    }
    [ContextMenu("Select 1")]
    private void Select1()
    {
        SelectAbility(1);
    }
    [ContextMenu("Select 2")]
    private void Select2()
    {
        SelectAbility(2);
    }

}

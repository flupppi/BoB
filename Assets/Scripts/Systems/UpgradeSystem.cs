using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeSystem : MonoBehaviour {
    [SerializeField] private AbilityBase[] m_startAbilities;
    private bool m_status = true;
    private AbilityBase[] m_upgradableAbilites = new AbilityBase[3];
    private AbilityHolder m_abilityHolder;

    public event Action<AbilityBase[]> OnOpenUpgradeWindow;
    public event Action OnCloseWindow;

    public void Enable() {
        m_status = true;
    }

    public void Disable() {
        m_status = false;
    }

    public AbilityBase[] GetRandomUpgrades() {
        Array.Clear(m_upgradableAbilites, 0, m_upgradableAbilites.Length);
        List<AbilityBase> abilityPool = new ();

        for(int i = 0; i < m_startAbilities.Length; i++) {
            if(m_startAbilities[i])
                abilityPool.Add(m_startAbilities[i]);
        }

        if (m_abilityHolder != null) {
            foreach (AbilityBase currentAbility in m_abilityHolder.Abilities) {
                if (currentAbility) {
                    foreach (AbilityBase currentAbilityUpgrade in currentAbility.Upgrades)
                    {
                        if (currentAbilityUpgrade) {
                            abilityPool.Add(currentAbilityUpgrade);
                        }
                    }
                }
            }
        }

        Debug.Log($"AbilityPool: {abilityPool.Count}" );

        for (int i = 0; i < m_upgradableAbilites.Length; i++) {
            // Eventuell Checken ob kein Index doppelt vorhanden ist
            int random = Random.Range(0, abilityPool.Count-1);
            m_upgradableAbilites[i] = abilityPool[random];
        }

        return m_upgradableAbilites;
    }

    public void SelectAbility(int ability) {
        UpgradeAbility(ability);
        Disable();
        OnCloseWindow?.Invoke();
    }

    private void UpgradeAbility(int ability) {
        m_abilityHolder.UpgradeAbility(m_upgradableAbilites[ability]);
    }

    void OnTriggerEnter(Collider triggerCollider) {
        if (m_status && triggerCollider.gameObject.tag == "Player") {
            m_abilityHolder = triggerCollider.gameObject.GetComponent<AbilityHolder>();
            PrintRandomAbilities();
            // GetRandomUpgrades();
            OnOpenUpgradeWindow?.Invoke(m_upgradableAbilites);
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
        SelectAbility(0);
    }
    [ContextMenu("Select 2")]
    private void Select2()
    {
        SelectAbility(0);
    }

}

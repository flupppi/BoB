using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour {
    [SerializeField] private AbilityBase[] m_abilities = new AbilityBase[5];
    private float[] m_cooldowns;
    private AbilityState m_currentState = AbilityState.Waiting;

    private float activeTime = 0.0f;



    void Update() {
        switch (m_currentState) {
            case AbilityState.Waiting:
                break;
            case AbilityState.Active:
                break;
            default:
                Debug.Log("AbilityHolder State machine Error");
                break;
        }
    }





    void OnValidate() {
        if (m_abilities.Length != 5) {
            Debug.LogError("Don't change the ability slot size");
            AbilityBase[] newAbilities = new AbilityBase[5];

            for (int i = 0; i < m_abilities.Length; i++) {
                if (i < newAbilities.Length) {
                    newAbilities[i] = m_abilities[i];
                }
            }

            m_abilities = newAbilities;
        }
    }
}

enum AbilityState {
    Waiting,
    Active
}

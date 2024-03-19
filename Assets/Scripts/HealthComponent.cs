using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField, Range(0, 10000)] private float m_MaxHealth = 100.0f;
    private float m_currentHealth;
    private bool m_isInvincible = false;

    public float Health {
        get => m_currentHealth;
        set => m_currentHealth = Mathf.Clamp(value, 0, m_MaxHealth);
    }
    public bool Invincible {
        get => m_isInvincible;
        set => m_isInvincible = value;
    }

    void Awake() {
        m_currentHealth = m_MaxHealth;
    }

    public void TakeDamage(float damage) {
        if(!m_isInvincible)
            Health -= damage;
    }

    public bool IsAlive() {
        return m_currentHealth > 0;
    }
}
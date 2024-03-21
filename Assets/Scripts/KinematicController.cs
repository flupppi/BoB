using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KinematicController : MonoBehaviour {
    [SerializeField] private bool m_autoEnable = true;
    [SerializeField] private float m_autoEnableTime = 0.5f;
    private float m_timer;
    private Rigidbody m_rb;
    void Start() {
        m_rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (m_autoEnable && m_timer <= 0.0f && !m_rb.isKinematic && m_rb.velocity.magnitude < 1.0f) {
            EnableKinematic();
        }

        if (m_timer > 0.0f) {
            m_timer -= Time.deltaTime;
        }
    }

    public void EnableKinematic() {
        m_rb.isKinematic = true;
    }

    public void DisableKinematic() {
        m_timer = m_autoEnableTime;
        m_rb.isKinematic = false;
    }
}
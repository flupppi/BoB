using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Collider))]
public class ExplodeOnContact : MonoBehaviour {
    [SerializeField] private ParticleSystem m_explosion;
    [SerializeField] private float m_damage;
    [SerializeField] private float m_explosionRange;
    [SerializeField] private float m_explosionForce;
    [SerializeField] private LayerMask m_layer;

    private Collider m_collider;
    private bool m_triggered;
    void Start() {
        m_collider = GetComponent<Collider>();
        m_collider.isTrigger = true;
    }

    void OnTriggerEnter() {
        if (m_triggered) return;
        m_triggered = true;

        Explode();
    }

    private void Explode() {
        if (m_explosion != null)
        {
            m_explosion.Play();
        }

        Collider[] enemies = Physics.OverlapSphere(transform.position, m_explosionRange, m_layer);

        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i].GetComponent<HealthComponent>())
                enemies[i].GetComponent<HealthComponent>().TakeDamage(m_damage);

            Rigidbody enemyRB = enemies[i].GetComponent<Rigidbody>();
            KinematicController kc = enemies[i].GetComponent<KinematicController>();

            if (enemyRB && kc)
            {
                kc.DisableKinematic();
                enemyRB.AddExplosionForce(m_explosionForce, transform.position, m_explosionRange);
            }

        }

        Debug.Log("EXPLOSION");
        Destroy(gameObject);
    }
}

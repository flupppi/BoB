using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float m_speed;

    [SerializeField] private float m_damage;

    public Vector3 Direction { get; set; } = Vector3.forward;

    void Start() {
        GetComponent<Rigidbody>().AddForce(Direction * m_speed, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider triggerCollider) {
        if (triggerCollider.gameObject.tag == "Enemy") {
            
        }
        triggerCollider.gameObject.GetComponent<HealthComponent>()?.TakeDamage(m_damage);

        Destroy(gameObject);
    }

}
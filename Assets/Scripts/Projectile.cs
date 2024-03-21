using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Projectile : MonoBehaviour {
    [SerializeField] private float m_speed;
    [SerializeField] private float m_damage;
    [SerializeField] private float m_maxLifeTime;
    
    [SerializeField] private bool m_isBouncy;
    [SerializeField, Range(0.0f, 1.0f)] private float m_bounciness;
    [SerializeField] private bool m_useGravity;
    [SerializeField] private int m_maxCollisions = 1;

    [SerializeField] private ParticleSystem m_explosion;
    [SerializeField] private bool m_explosive;
    [SerializeField] private bool m_explodeOnTouch;
    [SerializeField] private float m_explosionForce;
    [SerializeField] private float m_explosionRange;
    [SerializeField] private LayerMask m_layer;

    private Rigidbody m_rb;
    private CapsuleCollider m_collider;
    private int m_collisions;
    private PhysicMaterial m_physicsMaterial;

    public Vector3 Direction { get; set; } = Vector3.forward;

    void Start() {
        m_rb = GetComponent<Rigidbody>();
        m_collider = GetComponent<CapsuleCollider>();

        m_physicsMaterial = new PhysicMaterial {
            bounciness = m_bounciness,
            frictionCombine = PhysicMaterialCombine.Minimum,
            bounceCombine = PhysicMaterialCombine.Maximum
        };
        m_collider.material = m_physicsMaterial;

        m_rb.useGravity = m_useGravity;
        m_rb.collisionDetectionMode = CollisionDetectionMode.Continuous;


        m_rb.AddForce(Direction * m_speed, ForceMode.Impulse);
    }

    void Update() {
        m_maxLifeTime -= Time.deltaTime;

        if (m_collisions >= m_maxCollisions) {
            Explode();
        }

        if (m_maxLifeTime <= 0.0f && m_explosive) {
            Explode();
        } else if (m_maxLifeTime <= 0.0f) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider triggerCollider) {

        if (triggerCollider.gameObject.tag == "Enemy" && m_explodeOnTouch) {
            Explode();
        }

        if (triggerCollider.gameObject.tag == "Enemy" && !m_explosive) {
            triggerCollider.gameObject.GetComponent<HealthComponent>()?.TakeDamage(m_damage);

            
        }

        if (!m_explosive) {
            if (m_explosion != null)
            {
                m_explosion.transform.parent = null;
                m_explosion.Play();
            }

            Destroy(gameObject);
        }


        m_collisions++;
    }

    private void Explode() {
        if (m_explosion != null) {
            //Instantiate(m_explosion, transform.position, Quaternion.identity);
            m_explosion.Play();
        }

        Collider[] enemies = Physics.OverlapSphere(transform.position, m_explosionRange, m_layer);

        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].GetComponent<HealthComponent>().TakeDamage(m_damage);

            Rigidbody enemyRB = enemies[i].GetComponent<Rigidbody>();
            KinematicController kc = enemies[i].GetComponent<KinematicController>();

            if (enemyRB) {
                kc.DisableKinematic();
                enemyRB.AddExplosionForce(m_explosionForce, transform.position, m_explosionRange);
            }

        }

        //MeshRenderer[] meshs = gameObject.GetComponentsInChildren<MeshRenderer>();

        //foreach (MeshRenderer meshRenderer in meshs) {
        //    meshRenderer.enabled = false;
        //}

        //gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_explosionRange);
    }
}
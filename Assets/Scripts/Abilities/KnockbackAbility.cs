using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu]
public class KnockbackAbility : AbilityBase {
    [SerializeField] private float m_maxAngleDegrees = 45.0f;
    [SerializeField] private float m_distance = 20.0f;
    [SerializeField] private float m_force = 50.0f;
    [SerializeField] private float m_upwardsForce = 20.0f;
    [SerializeField] private float m_enableKinematicAfterTime = 1.0f;
    [SerializeField] private LayerMask m_layer;

    [SerializeField] private ParticleSystem m_particleSystem;

    private List<GameObject> enemiesHit = new List<GameObject>();
    public override void Activate(GameObject parent) {
        if (m_particleSystem) {
            m_particleSystem?.Play();
        }
        
        enemiesHit.Clear();
        Vector3 forward = parent.transform.TransformDirection(Vector3.forward);

        for (float currentAngle = 0.0f; currentAngle < m_maxAngleDegrees; currentAngle++)
        {
            Vector3 positiveRotatedVector = Quaternion.Euler(0.0f, currentAngle, 0.0f) * forward;
            Vector3 negativeRotatedVector = Quaternion.Euler(0.0f, -currentAngle, 0.0f) * forward;
            Debug.DrawRay(parent.transform.position, positiveRotatedVector * m_distance, Color.blue);
            Debug.DrawRay(parent.transform.position, negativeRotatedVector * m_distance, Color.blue);
            RaycastHit hit;
            if (Physics.Raycast(parent.transform.position, positiveRotatedVector, out hit, m_distance, m_layer, QueryTriggerInteraction.Collide)) {
               OnHit(hit, positiveRotatedVector);
            }

            if (Physics.Raycast(parent.transform.position, negativeRotatedVector, out hit, m_distance, m_layer, QueryTriggerInteraction.Collide)) {
                OnHit(hit, negativeRotatedVector);
            }
        }
    }

    private void OnHit(RaycastHit hit, Vector3 direction) {
        if (!enemiesHit.Contains(hit.transform.gameObject) && hit.transform.gameObject.CompareTag("Enemy")) {
            Rigidbody enemyRB = hit.transform.GetComponent<Rigidbody>();
            KinematicController kc = hit.transform.GetComponent<KinematicController>();

            if (kc) {
                kc.DisableKinematic();
                enemyRB.AddForce(m_force * direction + Vector3.up * m_upwardsForce, ForceMode.Impulse);
            }

            enemiesHit.Add(hit.transform.gameObject);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu]
public class NormalShoot : AbilityBase {
    [SerializeField] private GameObject bullet;

    private Transform m_aimLocation;
    public override void Activate(GameObject parent) {
        AbilityHolder abilityHolder = parent.GetComponent<AbilityHolder>();
        
        if (!abilityHolder.AbilityLocations[0]) return;
        if (!abilityHolder.AimLocation) return;
        m_aimLocation = abilityHolder.AimLocation;

        Vector3 shootDirection = GetShootDirection(parent, abilityHolder.AbilityLocations[0]);
        GameObject spawnedBullet = Instantiate(bullet, abilityHolder.AbilityLocations[0].position, Quaternion.LookRotation(shootDirection) * bullet.transform.localRotation);
        
        Projectile projectileComponent = spawnedBullet.GetComponent<Projectile>();
        projectileComponent.Direction = shootDirection;
    }
    private Vector3 GetShootDirection(GameObject parent, Transform shootLocation) {
        
        //Debug.DrawRay(m_aimLocation.position, m_aimLocation.forward, Color.blue, 5.0f);

        RaycastHit hit;
        Vector3 target;
        Ray ray = new Ray(m_aimLocation.transform.position, m_aimLocation.forward);
        if (Physics.Raycast(ray, out hit)) {
            target = hit.point;
        }
        else {
            target = ray.GetPoint(1000.0f);
        }

        Vector3 lookAtDirection = target - shootLocation.position;
        // Debug.DrawRay(shootLocation.position, lookAtDirection, Color.red, 5.0f);

        return lookAtDirection.normalized;
    }
}

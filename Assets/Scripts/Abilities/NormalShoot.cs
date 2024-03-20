using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NormalShoot : AbilityBase {
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float damage;
    public override void Activate(GameObject parent) {
        AbilityHolder abilityHolder = parent.GetComponent<AbilityHolder>();
        
        if (!abilityHolder.AbilityLocations[0]) return;
        Debug.LogError("Shooooooot");
        GameObject spawnedBullet = Instantiate(bullet, abilityHolder.AbilityLocations[0].position, Quaternion.LookRotation(abilityHolder.AbilityLocations[0].forward) * bullet.transform.localRotation);
        
        Projectile projectileComponent = spawnedBullet.GetComponent<Projectile>();
        projectileComponent.Direction = abilityHolder.AbilityLocations[0].forward;
    }
}

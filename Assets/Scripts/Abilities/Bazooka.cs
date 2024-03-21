using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Bazooka : AbilityBase
{
    [SerializeField] private GameObject bullet;

    public override void Activate(GameObject parent) {
        AbilityHolder abilityHolder = parent.GetComponent<AbilityHolder>();

        if (!abilityHolder.AbilityLocations[1]) return;
        GameObject spawnedBullet = Instantiate(bullet, abilityHolder.AbilityLocations[1].position, Quaternion.LookRotation(abilityHolder.AbilityLocations[1].forward) * bullet.transform.localRotation);

        Projectile projectileComponent = spawnedBullet.GetComponent<Projectile>();
        projectileComponent.Direction = abilityHolder.AbilityLocations[1].forward;
    }
}

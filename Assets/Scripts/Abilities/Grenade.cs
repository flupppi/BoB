using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Grenade : AbilityBase
{
    [SerializeField] private GameObject bullet;

    public override void Activate(GameObject parent)
    {
        AbilityHolder abilityHolder = parent.GetComponent<AbilityHolder>();

        if (!abilityHolder.AbilityLocations[2]) return;
        GameObject spawnedBullet = Instantiate(bullet, abilityHolder.AbilityLocations[1].position, Quaternion.LookRotation(abilityHolder.AbilityLocations[2].forward) * bullet.transform.localRotation);

        Projectile projectileComponent = spawnedBullet.GetComponent<Projectile>();
        projectileComponent.Direction = abilityHolder.AbilityLocations[2].forward;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BasicShoot : AbilityBase
{
    public override void Activate(GameObject parent) {
        Debug.Log(abilityName);
    }
}

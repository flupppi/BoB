using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBase : ScriptableObject
{
    private string name;
    private float cooldown;
    private float activeTime;
    private AbilityBase[] Upgrades;
    private Animation animation;

    public virtual void Activate(GameObject parent) {}
}

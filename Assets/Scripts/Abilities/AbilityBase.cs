using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBase : ScriptableObject
{
    public string abilityName;
    [TextArea] public string description;
    public float cooldown;
    public float activeTime;
    public AbilityBase[] Upgrades;
    public Animation animation;
    public Image icon;

    public virtual void Activate(GameObject parent) {}
}

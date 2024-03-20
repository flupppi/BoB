
using UnityEngine;

public class BigAttackState : IState
{
    private EnemyBrain brain;

    public BigAttackState(EnemyBrain brain)
    {
        this.brain = brain;
    }

    public void OnEnter()
    {
        brain.navMeshAgent.enabled = false;
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
        brain.transform.LookAt(new Vector3(brain.target.position.x, brain.transform.position.y, brain.target.position.z));
        Attack();
    }

    public void Attack()
    {
        if (!brain.readyToAttack || brain.attacking) return;
        brain.readyToAttack = false;
        brain.attacking = true;
        Debug.Log("BigEnemy Attack!");
        brain.InvokeResetAttack();
    }

    public Color GizmoColor()
    {
        return Color.red;
    }
}
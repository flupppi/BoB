
using UnityEngine;
using UnityEngine.AI;

public class MidAttackState : IState
{
    private EnemyBrain brain;

    public MidAttackState(EnemyBrain brain)
    {
        this.brain = brain;
    }

    public void OnEnter()
    {
        brain.navMeshAgent.enabled = false;
        brain.GetComponent<NavMeshObstacle>().enabled = true;
    }

    public void OnExit()
    {
        brain.GetComponent<NavMeshObstacle>().enabled = false;
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
        Debug.Log("MidEnemy Attack!");
        brain.InvokeResetAttack();
    }

    public Color GizmoColor()
    {
        return Color.red;
    }
}
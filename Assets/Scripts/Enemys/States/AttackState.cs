/*

using UnityEngine;

public class AttackState : IState
{
    private EnemyBrain brain;

    public AttackState(EnemyBrain brain)
    {
        this.brain = brain;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
        if (brain.distanceToTarget > brain.attackRange)
        {
            brain.target = null;
            return;
        }
        AttackTarget();
    }

    public void AttackTarget()
    {
        if (!stats.readyToAttack || stats.attacking) return;

        stats.readyToAttack = false;
        stats.attacking = true;

        //Calculate direction from firePoint to targetPoint
        Vector3 direction = stats.target.position + new Vector3(0, 1, 0) - spiderBrain.shootPosition.position;

        //Instantiate projectile
        GameObject projectileObj = GameObject.Instantiate(spiderBrain.projectile, spiderBrain.shootPosition.position, Quaternion.identity);
        //Rotate projectile to shoot direction
        projectileObj.transform.forward = direction.normalized;
        //Add forces to projectile
        projectileObj.GetComponent<Rigidbody>().AddForce(direction.normalized * 20, ForceMode.Impulse);

        stats.InvokeResetAttack();
    }

    public Color GizmoColor()
    {
        return Color.red;
    }
}*/
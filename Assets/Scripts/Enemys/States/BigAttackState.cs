
using UnityEngine;

public class BigAttackState : IState
{
    private BigEnemyBrain brain;

    public BigAttackState(BigEnemyBrain brain)
    {
        this.brain = brain;
    }

    public void OnEnter()
    {
        brain.navMeshAgent.enabled = false;
        brain.beam.enabled = true;
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
        
        brain.beam.enabled = true;
        Ray ray  = new Ray(brain.muzzlePoint.position, brain.muzzlePoint.forward);
        bool cast = Physics.Raycast(ray, out RaycastHit hit, brain.beamMaxLength);
        Vector3 hitPosition = cast ? hit.point : brain.muzzlePoint.position + brain.muzzlePoint.forward * brain.beamMaxLength;

        brain.beam.SetPosition(0, brain.muzzlePoint.position);
        brain.beam.SetPosition(1, hitPosition);


        brain.InvokeResetAttack();
    }

    public Color GizmoColor()
    {
        return Color.red;
    }
}

using System.Collections;
using UnityEngine;

public class BigAttackState : IState
{
    private BigEnemyBrain brain;
    private float timerDuration;
    private float timerDelay;
    public BigAttackState(BigEnemyBrain brain)
    {
        this.brain = brain;
    }

    public void OnEnter()
    {
        brain.navMeshAgent.enabled = false;
        brain.laserpointer.enabled = true;

        if(brain.DoneAttack1 && brain.DoneAttack2){
            timerDuration = brain.bigBeamDuration;
        }
        else{
            timerDuration = brain.smallBeamDuration;
        }
        timerDelay = brain.beamDelay;
        
    }

    public void OnExit()
    {
        brain.smallBeam.enabled = false;
        brain.bigBeam.enabled = false;
        brain.laserpointer.enabled = false;
    }

    public void Tick()
    {
        if(timerDelay > 0f){
            timerDelay -= Time.deltaTime;
        }
        else{
            Attack();
        }
    }

    public void Attack()
    {
        if(!brain.DoneAttack1){
            SmallBeam();
        }
        else if(!brain.DoneAttack2){
            SmallBeam();
        }
        else{
            BigBeam();
        }
    }

    private void SmallBeam(){
        if(timerDuration  > 0f){
            brain.smallBeam.enabled = true;
            timerDuration -= Time.deltaTime;
            Ray ray  = new Ray(brain.muzzlePoint.position, brain.muzzlePoint.forward);
            bool cast = Physics.Raycast(ray, out RaycastHit hit, brain.beamMaxLength, brain.hitMaskBeam);
            Vector3 hitPosition = cast ? hit.point : brain.muzzlePoint.position + brain.muzzlePoint.forward * brain.beamMaxLength;
            brain.smallBeam.SetPosition(0, brain.muzzlePoint.position);
            brain.smallBeam.SetPosition(1, hitPosition);

            if(cast && hit.collider.TryGetComponent(out HealthComponent healthComponent)){
                healthComponent.TakeDamage(brain.smallBeamDamage * Time.fixedDeltaTime);
            }
        }
        else{
            if(!brain.DoneAttack1) brain.DoneAttack1 = true;
            else if(!brain.DoneAttack2) brain.DoneAttack2 = true;
            brain.Attack = false;
        }
    }

    private void BigBeam(){
        if(timerDuration  > 0f){
            brain.bigBeam.enabled = true;
            timerDuration -= Time.deltaTime;
            Ray ray  = new Ray(brain.muzzlePoint.position, brain.muzzlePoint.forward);
            bool cast = Physics.Raycast(ray, out RaycastHit hit, brain.beamMaxLength, brain.hitMaskBeam);
            Vector3 hitPosition = cast ? hit.point : brain.muzzlePoint.position + brain.muzzlePoint.forward * brain.beamMaxLength;
            brain.bigBeam.SetPosition(0, brain.muzzlePoint.position);
            brain.bigBeam.SetPosition(1, hitPosition);

            if(cast && hit.collider.TryGetComponent(out HealthComponent healthComponent)){
                healthComponent.TakeDamage(brain.bigBeamDamage * Time.fixedDeltaTime);
            }
        }
        else{
            brain.DoneAttack1 = false;
            brain.DoneAttack2 = false;
            brain.Attack = false;
        }
    }

    public Color GizmoColor()
    {
        return Color.red;
    }
}
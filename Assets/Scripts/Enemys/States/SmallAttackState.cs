
using UnityEngine;

public class SmallAttackState : IState
{
    private EnemyBrain brain;

    public SmallAttackState(EnemyBrain brain)
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
        Explode();
    }

    public void Explode()
    {
        Debug.Log("Jetzt Explosion!");
        brain.healthComponent.Kill();
    }

    public Color GizmoColor()
    {
        return Color.red;
    }
}
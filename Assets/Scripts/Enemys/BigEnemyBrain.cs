using System;
using UnityEngine;

public class BigEnemyBrain : EnemyBrain
{
    private float health = 1f;
    void Start()
    {
        stateMachine = new StateMachine();

        //STATES
        var idleState = new IdleState(this);
        var moveToTargetState = new MoveToTargetState(this);
        var deadState = new DeadState(this);

        //TRANSITIONS
        Any(deadState, Dead());
        At(idleState, moveToTargetState, HasTarget());
        
        //START STATE
        stateMachine.SetState(idleState);

        //CONDITIONS & FUNCTIONS
        Func<bool> HasTarget() => () => target != null;
        Func<bool> Dead() => () => health <= 0f;

        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to, condition);
    }
}

using System;
using UnityEngine;

public class MidEnemyBrain : EnemyBrain
{
    void Start()
    {
        stateMachine = new StateMachine();

        //STATES
        var idleState = new IdleState(this);
        var moveToTargetState = new MoveToTargetState(this);
        var attackState = new MidAttackState(this);
        var deadState = new DeadState(this);

        //TRANSITIONS
        Any(deadState, Dead());
        At(idleState, moveToTargetState, HasTarget());
        At(moveToTargetState, attackState, InAttackRange());
        At(attackState, moveToTargetState, NotInAttackRange());
        
        //START STATE
        stateMachine.SetState(idleState);

        //CONDITIONS & FUNCTIONS
        Func<bool> HasTarget() => () => target != null;
        Func<bool> Dead() => () => healthComponent.Health <= 0f;
        Func<bool> InAttackRange() => () => distanceToTarget <= attackRange;
        Func<bool> NotInAttackRange() => () => distanceToTarget > attackRange;


        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to, condition);
    }
}

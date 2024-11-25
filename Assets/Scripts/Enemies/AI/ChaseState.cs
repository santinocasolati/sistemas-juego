using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    private EnemyAI _enemyAI;

    public ChaseState(EnemyAI enemyAI)
    {
        _enemyAI = enemyAI;
    }

    public void Enter()
    {
        _enemyAI.SetAnimation("Run");
    }

    public void Execute()
    {
        _enemyAI.MoveTowardsPlayer();

        if (_enemyAI.IsPlayerInAttackRange() && _enemyAI.canAttack)
        {
            _enemyAI.ChangeState(new AttackState(_enemyAI));
        }
    }

    public void Exit()
    {
        _enemyAI.StopMovement();
    }
}

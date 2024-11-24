using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private EnemyAI _enemyAI;

    public IdleState(EnemyAI enemyAI)
    {
        _enemyAI = enemyAI;
    }

    public void Enter()
    {
        _enemyAI.SetAnimation("Idle");
        _enemyAI.StopMovement();
    }

    public void Execute()
    {
        if (_enemyAI.IsPlayerInDetectionRange())
        {
            _enemyAI.ChangeState(new ChaseState(_enemyAI));
        }
    }

    public void Exit()
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private EnemyAI _enemyAI;

    public AttackState(EnemyAI enemyAI)
    {
        _enemyAI = enemyAI;
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        if (!_enemyAI.IsPlayerInAttackRange())
        {
            _enemyAI.ChangeState(new ChaseState(_enemyAI));
        } else
        {
            _enemyAI.StartAttack();
        }
    }

    public void Exit()
    {
    }
}

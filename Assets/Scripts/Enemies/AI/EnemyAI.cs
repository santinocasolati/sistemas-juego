using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyDataSO enemyData;

    private StateMachine _stateMachine;
    private Transform _player;
    private Animator _animator;
    private Rigidbody _rigidbody;

    public bool canAttack = true;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        _stateMachine = new StateMachine();
        _stateMachine.ChangeState(new IdleState(this));
    }

    private void OnEnable()
    {
        if (_stateMachine != null)
            _stateMachine.ChangeState(new IdleState(this));
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void ChangeState(IState newState)
    {
        _stateMachine.ChangeState(newState);
    }

    public bool IsPlayerInDetectionRange()
    {
        return Vector3.Distance(transform.position, _player.position) <= enemyData.detectionRange;
    }

    public bool IsPlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, _player.position) <= enemyData.attackRange;
    }

    public void MoveTowardsPlayer()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        _rigidbody.velocity = direction * enemyData.moveSpeed;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }
    }

    public void StartAttack()
    {
        if (!canAttack) return;

        canAttack = false;

        SetAnimation("Attack");

        _player.gameObject.GetComponent<IHealth>().Damage(enemyData.attackDamage);

        Invoke(nameof(ResetAttackCooldown), enemyData.attackCooldown);
    }

    private void ResetAttackCooldown()
    {
        canAttack = true;
    }

    public void SetAnimation(string animationState)
    {
        _animator.Play(animationState, -1, 0f);
    }

    public void StopMovement()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackDamage = 5f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackCooldown = 2f;

    private StateMachine _stateMachine;
    private Transform _player;
    private Animator _animator;
    private Rigidbody _rigidbody;

    private bool _canAttack = true;

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
        return Vector3.Distance(transform.position, _player.position) <= detectionRange;
    }

    public bool IsPlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, _player.position) <= attackRange;
    }

    public void MoveTowardsPlayer()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        _rigidbody.velocity = direction * moveSpeed;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }
    }

    public void StartAttack()
    {
        if (!_canAttack) return;

        _canAttack = false;

        SetAnimation("Attack");

        _player.gameObject.GetComponent<IHealth>().Damage(attackDamage);

        Invoke(nameof(ResetAttackCooldown), attackCooldown);
    }

    private void ResetAttackCooldown()
    {
        _canAttack = true;
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

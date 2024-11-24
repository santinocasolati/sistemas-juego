using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPDecorator : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHP;

    private HP _HP;

    private void Awake()
    {
        _HP = new HP(maxHP);
        _HP.OnDeath += HandleDeath;
    }

    public void Heal(float amount)
    {
        _HP.Heal(amount);
    }

    public void Damage(float amount)
    {
        _HP.Damage(amount);
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}

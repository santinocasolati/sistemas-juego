using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPDecorator : MonoBehaviour, IHealth
{
    [SerializeField] private EnemyDataSO enemyData;

    private HP _HP;

    private void Awake()
    {
        _HP = new HP(enemyData.maxHP);
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
        ServiceLocator.Instance.AccessService<ScoreService>().AddScore(1);
        ServiceLocator.Instance.AccessService<EnemyFactoryService>().StoreItem(enemyData.enemyName, gameObject);
    }

    public void Reset()
    {
        _HP.Reset();
    }
}

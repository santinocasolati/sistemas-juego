using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    public string enemyName;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackDamage = 5f;
    public float moveSpeed = 3f;
    public float attackCooldown = 2f;
    public float maxHP;
}

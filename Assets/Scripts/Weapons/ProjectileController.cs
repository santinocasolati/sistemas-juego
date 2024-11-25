using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private string projectileName = "default";
    [SerializeField] private float speed = 5f;
    [SerializeField] private float damage = 30f;
    [SerializeField] private float lifeTime = 5f;

    private Vector3 _direction;
    private bool _enabled = false;
    private float _counter = 0;

    public void Setup(Vector3 direction)
    {
        _direction = direction;
        _counter = 0;
        _enabled = true;
    }

    private void Update()
    {
        if (_enabled)
        {
            transform.position += _direction * speed * Time.deltaTime;

            _counter += Time.deltaTime;

            if (_counter >= lifeTime)
            {
                EliminateProjectile();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IHealth health = other.gameObject.GetComponent<IHealth>();

        if (health != null)
            health.Damage(damage);

        EliminateProjectile();
    }

    private void EliminateProjectile()
    {
        ServiceLocator.Instance.AccessService<ProjectileFactoryService>().StoreProjectile(projectileName, gameObject);
    }
}

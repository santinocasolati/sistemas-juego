using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected Transform gunTip;
    [SerializeField] protected float shootDelay;
    [SerializeField] protected Vector3 offset;
    [SerializeField] protected Vector3 rotation;
    [SerializeField] protected AudioClip shootSFX;

    protected bool _canShoot = true;
    protected bool _isShooting = false;
    public bool IsShooting
    {
        set
        {
            _isShooting = value;
        }
    }

    public void Setup()
    {
        transform.localPosition = offset;
        transform.localRotation = Quaternion.Euler(rotation);
    }

    public virtual void Shoot()
    {
        _canShoot = false;

        ServiceLocator.Instance.AccessService<AudioService>().PlayAudio(shootSFX);

        Invoke(nameof(ResetShoot), shootDelay);
    }

    private void Update()
    {
        if (_isShooting)
            Shoot();
    }

    private void ResetShoot()
    {
        _canShoot = true;
    }
}

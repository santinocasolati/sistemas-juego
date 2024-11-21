using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public bool CanShoot { set; }

    void Setup(Transform aimTarget);
    void Shoot();
}

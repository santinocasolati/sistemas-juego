using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public bool IsShooting { set; }

    void Setup();
    void Shoot();
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HP : IHealth
{
    private float _maxHP;
    private float _currentHP;

    private float CurrentHP
    {
        get
        {
            return _currentHP;
        }
        set
        {
            _currentHP = Mathf.Clamp(value, 0, _maxHP);
            OnHPModified?.Invoke((_currentHP / _maxHP) * 100f);
        }
    }

    public float CurrentHPPercentage
    {
        get
        {
            return (_currentHP / _maxHP) * 100f;
        }
    }

    public Action OnDeath;
    public Action<float> OnHPModified;

    public HP(float maxHP)
    {
        _maxHP = maxHP;
        _currentHP = maxHP;
    }

    public void Heal(float amount)
    {
        CurrentHP += amount;
    }

    public void Damage(float amount)
    {
        CurrentHP -= amount;

        if (CurrentHP <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        OnDeath?.Invoke();
    }

    public void Reset()
    {
        _currentHP = _maxHP;
    }
}

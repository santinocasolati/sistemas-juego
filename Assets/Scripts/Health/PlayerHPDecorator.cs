using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPDecorator : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHP;
    [SerializeField] private Slider hpSlider;

    private HP _HP;

    private void Awake()
    {
        _HP = new HP(maxHP);

        _HP.OnHPModified += value => hpSlider.value = value;
    }

    public void Heal(float amount)
    {
        _HP.Heal(amount);
    }

    public void Damage(float amount)
    {
        _HP.Damage(amount);
    }
}

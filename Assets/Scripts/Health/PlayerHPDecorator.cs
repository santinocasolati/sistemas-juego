using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHPDecorator : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHP;
    [SerializeField] private Slider hpSlider;

    private HP _HP;

    public bool isInvincible = false;

    private void Start()
    {
        _HP = new HP(maxHP);

        _HP.OnHPModified += value => hpSlider.value = value;
        hpSlider.value = _HP.CurrentHPPercentage;

        _HP.OnDeath += Death;
    }

    private void Death()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Heal(float amount)
    {
        _HP.Heal(amount);
    }

    public void Damage(float amount)
    {
        if (!isInvincible)
            _HP.Damage(amount);
    }

    public void Reset()
    {
        _HP.Reset();
    }
}

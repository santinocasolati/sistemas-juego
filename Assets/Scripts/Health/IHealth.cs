using System.Collections;
using System.Collections.Generic;

public interface IHealth
{
    void Heal(float amount);
    void Damage(float amount);
    void Reset();
}

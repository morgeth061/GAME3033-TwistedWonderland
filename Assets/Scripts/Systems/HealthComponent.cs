using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float currentHealth;
    public float CurrentHealth => currentHealth;

    [SerializeField]
    private float maxHealth;
    public float MaxHealth => maxHealth;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = MaxHealth;
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    public virtual void Heal(int healValue)
    {
        if (currentHealth < MaxHealth && currentHealth > MaxHealth - healValue)
        {
            currentHealth = MaxHealth;
        }
        else if (currentHealth < MaxHealth - 100)
        {
            currentHealth += healValue;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //Game Loss
            Destroy();
        }
    }


}

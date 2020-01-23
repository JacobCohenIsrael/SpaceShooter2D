using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    public DamageTakenEvent OnDamageTaken;

    private int currentHealth;

    private void Awake()
    {
        OnDamageTaken = new DamageTakenEvent();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
        OnDamageTaken.Invoke(damageTaken, currentHealth);
    }

}

public class DamageTakenEvent : UnityEvent<int, int>
{

}

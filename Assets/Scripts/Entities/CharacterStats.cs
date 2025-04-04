using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int defense = 10;

    public void TakeDamage(int damage)
    {
        int actualDamage = Mathf.Max(damage - defense, 0);
        Debug.Log($"{gameObject.name} took {actualDamage} damage. Remaining health: {maxHealth - actualDamage}");
        maxHealth -= actualDamage;
        if (maxHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        gameObject.SetActive(false);
    }
}

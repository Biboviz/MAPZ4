using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int defense = 10;
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
    public void DecreaseDefense(int ammount)
    {
        defense -= ammount;
    }
    public void AddDefense(int ammount)
    {
        defense += ammount;
    }
    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        gameObject.SetActive(false);
    }
    public int def
    {
        get { return defense; }
        set { defense = value; }
    }
}

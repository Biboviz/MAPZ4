using System;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{

    [SerializeField] protected int Health = 100;
    protected int MaxHealth;

    [SerializeField] private int defense = 10;
    private int maxDefense;

    [SerializeField] private Image currentHealth;
    [SerializeField] private Image currentDefense;

    private void Start()
    {
        MaxHealth = Health;
        maxDefense = defense;

        UpdateHealthBar();
        UpdateDefenseBar();
    }

    public virtual void TakeDamage(int damage)
    {
        int actualDamage = Mathf.Max(damage - defense, 0);
        Health -= actualDamage;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        Debug.Log($"{gameObject.name} took {actualDamage} damage. Remaining health: {Health}");

        UpdateHealthBar();

        if (Health <= 0)
        {
            Die();
        }
    }
    public void DecreaseDefense(int amount)
    {
        defense = Mathf.Max(defense - amount, 0);
        UpdateDefenseBar();
    }
    public void AddDefense(int amount)
    {
        defense = Mathf.Min(defense + amount, maxDefense);
        UpdateDefenseBar();
    }
    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        gameObject.SetActive(false);
    }
    public int GetHealth()
    {
        return Health;
    }
    public int def
    {
        get { return defense; }
        set
        {
            defense = Mathf.Clamp(value, 0, maxDefense);
            UpdateDefenseBar();
        }
    }
    private void UpdateState()
    {
        UpdateDefenseBar();
        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        if (currentHealth != null)
            currentHealth.fillAmount = (float)Health / MaxHealth;
    }
    private void UpdateDefenseBar()
    {
        if (currentDefense != null)
            currentDefense.fillAmount = (float)defense / maxDefense;
    }
    internal void SetHealth(int previousHealth)
    {
        Health = previousHealth;
        UpdateHealthBar();

    }
    internal void SetDefense(int defense)
    {
        this.defense = defense;
        UpdateDefenseBar();
    }
    internal int GetDefense()
    {
        return defense;
    }
    public void SetState(StatsState state)
    {
        Health = state.Health;
        defense = state.Defense;
        UpdateState();
    }
    public StatsState GetState()
    {
        return new StatsState(Health, defense);
    }
}
public class StatsState 
{
    public StatsState(int health, int defense) 
    {
        Health = health;
        Defense = defense;
    }
    public int Health {  get; private set; }
    public int Defense { get; private set; }
}

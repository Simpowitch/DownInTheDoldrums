using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterData : MonoBehaviour
{
    [SerializeField] HealthBar healthBar = null;

    public int health;
    public int maxHealth;
    public float healthRegen;
    public int stamina;
    public int armor;
    public int damage;
    public float movementSpeed;

    private void Start()
    {
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthBar();
        if (health <= 0)
        {
            print("dead");
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);
    }
}

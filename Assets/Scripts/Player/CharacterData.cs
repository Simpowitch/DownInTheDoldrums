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

    public List<Item> items = new List<Item>();

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

    public void AddItem(Item newItem)
    {
        items.Add(newItem);
        foreach (Effect newEffect in newItem.effects)
        {
            switch (newEffect.effectType)
            {
                case EffectType.Damage:
                    damage += newEffect.change;
                    break;
                case EffectType.MovementSpeed:
                    movementSpeed += newEffect.change;
                    break;
                case EffectType.MaxHP:
                    maxHealth += newEffect.change;
                    break;
                case EffectType.InstantHeal:
                    health += newEffect.change;
                    break;
            }
        }

        //Update UI to show changes
        UpdateHealthBar();
    }
}

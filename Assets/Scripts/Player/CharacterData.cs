using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterData : MonoBehaviour
{
    public int health;
    public float healthRegen;
    public int stamina;
    public int armor;
    public int damage;
    public float movementSpeed;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            print("dead");
        }
    }
}

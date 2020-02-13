using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/New Character Data", order = 1)]
public class CharacterData : ScriptableObject
{
    public int health;
    public float healthRegen;
    public int stamina;
    public int armor;
    public int damage;
    public float movementSpeed;
}

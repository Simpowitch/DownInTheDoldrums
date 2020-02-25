using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/New Weapon")]
public class Weapon : ScriptableObject
{
    public float damage;
    public float cooldown;
    public GameObject spawnableObject;

}

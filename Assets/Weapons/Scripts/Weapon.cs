using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/New Weapon")]
public class Weapon : ScriptableObject
{
    public float damage;
    public float cooldown;
    public GameObject spawnableObject;
    public List<Effect> onHitEffects = new List<Effect>();

    public float GetExpectedRange()
    {
        WeaponSpawnedObject weaponSpawnedObject = spawnableObject.GetComponent<WeaponSpawnedObject>();
        return weaponSpawnedObject.speed * weaponSpawnedObject.objectLifetime;
    }
}

using System.Collections.Generic;
using UnityEngine;


public class WeaponSpawnedObject : MonoBehaviour
{
    public List<Effect> effects = new List<Effect>();
    public float duration;
    public float speed;

    public bool attachToCharacter;
    public string ignoreTag;

    void Start()
    {
        Destroy(this.gameObject, duration);
    }

    
}

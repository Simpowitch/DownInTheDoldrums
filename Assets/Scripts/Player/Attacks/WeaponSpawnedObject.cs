using UnityEngine;

public class WeaponSpawnedObject : MonoBehaviour
{
    public int damage;
    public float duration;
    public bool attachedToPlayer;

    void Start()
    {
        Destroy(this.gameObject, duration);
    }
}

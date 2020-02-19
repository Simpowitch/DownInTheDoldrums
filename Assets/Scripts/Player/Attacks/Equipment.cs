using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public int damage;
    public float duration;
    public float cooldown;
    public bool attachedToPlayer;
    public bool readyToUse = true;

    private float cooldownTimer = 0;

    private void Update()
    {
        if (readyToUse)
        {
            return;
        }
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            readyToUse = true;
        }
    }

    public bool TryToUse()
    {
        if (readyToUse)
        {
            cooldownTimer = cooldown;
            readyToUse = false;
            return true;
        }
        else
        {
            return false;
        }
    }
}

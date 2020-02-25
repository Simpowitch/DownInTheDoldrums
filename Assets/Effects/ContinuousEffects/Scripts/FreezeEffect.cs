using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEffect : Effect
{
    public override void ApplyEffect(CharacterData target)
    {
        target.movementSpeed *= 0.5f;
    }
}

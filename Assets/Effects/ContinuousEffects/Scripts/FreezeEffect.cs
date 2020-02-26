using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/New Freeze Effect", fileName = "New Freeze Effect")]
public class FreezeEffect : Effect
{
    [SerializeField][Range(0,99)] float slowPercentage;
    [SerializeField] float minSpeed;
    public override void ApplyEffect(CharacterData target)
    {
            base.ApplyEffect(target);
            target.movementSpeed *= 1 - slowPercentage * 0.01f;
    }

    public override void RemoveEffect(CharacterData target)
    {
        target.movementSpeed /= 1 - slowPercentage * 0.01f;
    }
}

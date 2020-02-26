using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/New Slow Effect", fileName = "New Slow Effect")]
public class SlowEffect : Effect
{
    [SerializeField][Range(0,99)] float slowPercentage = 10;
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

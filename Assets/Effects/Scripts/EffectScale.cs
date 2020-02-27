using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/New Scale Effect", fileName = "New Scale Effect")]
public class EffectScale : Effect
{
    [SerializeField] int damage = 0;
    [SerializeField] float scaleRate;

    public override void ApplyEffect(CharacterData target)
    {
        target.gameObject.transform.localScale += Vector3.one * scaleRate;
        target.TakeDamage(damage);
    }
}

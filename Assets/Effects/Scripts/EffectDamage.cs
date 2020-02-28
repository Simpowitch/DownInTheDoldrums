using UnityEngine;

[CreateAssetMenu(menuName = "Effect/New Damage Effect", fileName = "New Damage Effect")]
public class EffectDamage : Effect
{
    [SerializeField] int damage = 0;
    public override void ApplyEffect(CharacterData target)
    {
        target.TakeDamage(damage);
    }
}

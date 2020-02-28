using UnityEngine;
public enum EffectType { Instant, Continuous, LimitedTime}

public class Effect : ScriptableObject
{
    public EffectType effectType;

    public bool isTickBased;
    public float tickRate;

    public float duration;

    public virtual void ApplyEffect(CharacterData target){}
    public virtual void RemoveEffect(CharacterData target){}
    public EffectType GetEffectType()
    {
        return effectType;
    }
}

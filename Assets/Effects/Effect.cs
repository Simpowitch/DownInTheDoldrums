
using UnityEngine;
public enum EffectType { Instant, Continuous, LimitedTime}


public class Effect : ScriptableObject, IEffect
{

    public EffectType effectType;

    public virtual void ApplyEffect(CharacterData target)
    {

    }
    public EffectType GetEffectType()
    {
        return effectType;
    }
}

public interface IEffect
{
    EffectType GetEffectType();
    void ApplyEffect(CharacterData target);
}

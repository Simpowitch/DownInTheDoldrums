public enum EffectType { Damage, MovementSpeed, MaxHP, InstantHeal }

[System.Serializable]
public class Effect
{
    public EffectType effectType;
    public int change;
}

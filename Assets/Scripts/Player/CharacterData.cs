using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterData : MonoBehaviour
{
    [SerializeField] HealthBar healthBar = null;

    public int health;
    public int maxHealth;
    public float healthRegen;
    public int stamina;
    public int armor;
    public int damage;
    public float movementSpeed;

    public CharacterWeaponHolder equipSlotOne;
    public CharacterWeaponHolder equipSlotTwo;
    public CharacterWeaponHolder equipSlotThree;
    public CharacterWeaponHolder equipSlotFour;

    public CharacterWeaponHolder selectedWeaponHolder;

    public List<Item> items = new List<Item>();

    private void Start()
    {
        UpdateHealthBar();
    }
    private void Update()
    {
        //ContinuousEffects();
        //LimitedTimeEffects();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthBar();
        if (health <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(health);
        }
    }

    void Die()
    {
        if (gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
        else
        {
            print("Dead");
        }
    }
    public void AddItem(Item item)
    {

    }


    #region Effects
    List<Effect> continuousEffects = new List<Effect>(); //Used to store all continuous effects
    List<Effect> limitedTimeEffects = new List<Effect>(); //Used to store all temporary, but time-lingering, effects

    public void AddEffect(Effect effect)
    {
        switch (effect.GetEffectType())
        {
            case EffectType.Instant:
                effect.ApplyEffect(this);
                break;

            case EffectType.Continuous:
                if (continuousEffects.Contains(effect))
                {
                    break;
                }
                continuousEffects.Add(effect);
                StartCoroutine(DoContinuousEffect(effect));
                break;

            case EffectType.LimitedTime:
                if (limitedTimeEffects.Contains(effect))
                {
                    break;
                }
                limitedTimeEffects.Add(effect);
                StartCoroutine(DoLimitedTimeEffect(effect, effect.duration));
                break;

            default:
                break;
        }
    }
    
    IEnumerator DoContinuousEffect(Effect effect)
    {
        if (effect.isTickBased)
        {
            effect.ApplyEffect(this);
            yield return new WaitForSeconds(effect.tickRate);
            //Recursion
            StartCoroutine(DoContinuousEffect(effect));
        }
        else //One time effect - lingering with possible listeners or checks from the list
        {
            effect.ApplyEffect(this);
        }
    }

    IEnumerator DoLimitedTimeEffect(Effect effect, float timeRemaining)
    {
        if (timeRemaining <= 0)
        {
            limitedTimeEffects.Remove(effect);
            effect.RemoveEffect(this);
            yield return null;
        }
        else if (effect.isTickBased)
        {
            effect.ApplyEffect(this);
            yield return new WaitForSeconds(effect.tickRate);
            //Recursion
            StartCoroutine(DoLimitedTimeEffect(effect, timeRemaining-effect.tickRate));
        }
        else //One time effect - wait - remove effect
        {
            effect.ApplyEffect(this);
            yield return new WaitForSeconds(timeRemaining);
            effect.RemoveEffect(this);
            limitedTimeEffects.Remove(effect);
        }
    }
    #endregion
}

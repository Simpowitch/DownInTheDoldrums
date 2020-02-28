using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingAI : MonoBehaviour
{
    private enum State { Idle, RunToPlayer, PrepareCharge, Charge, Attack, RunAwayFromPlayer, Dying }
    private State currentState;

    GameObject playerObject;
    CharacterData characterData;
    CharacterWeaponSelect weaponSelector;

    [SerializeField] float detectionRange = 5;
    [SerializeField] float chargeRange = 2;

    float attackRange = 0;
    [SerializeField] float minimumAttackRange = 0.5f;

    private void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        weaponSelector = GetComponent<CharacterWeaponSelect>();
        characterData = GetComponent<CharacterData>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeWeapon(0); //Enemy start with weapon slot one as default

        //Set patrol points for idle
    }

    private void ChangeWeapon(int index)
    {
        switch (index)
        {
            case 0:
                weaponSelector.SelectWeapon(characterData.equipSlotOne);
                break;
            case 1:
                weaponSelector.SelectWeapon(characterData.equipSlotTwo);
                break;
            case 2:
                weaponSelector.SelectWeapon(characterData.equipSlotThree);
                break;
            case 3:
                weaponSelector.SelectWeapon(characterData.equipSlotFour);
                break;
            default:
                break;
        }
        CalculateAttackRange();
    }


    private void CalculateAttackRange()
    {
        attackRange = Mathf.Max(minimumAttackRange, characterData.selectedWeaponHolder.myWeapon.GetExpectedRange());
    }

    public bool IsCharacterWithinRange(float range)
    {
        return (Vector2.Distance(this.transform.position, playerObject.transform.position) < range);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Idle:

                if (IsCharacterWithinRange(detectionRange))
                {
                    //TODO: Set player as target for pathfinding
                    currentState = State.RunToPlayer;
                }
                else
                {
                    //TODO Walk small distances patrol
                }
                break;
            case State.RunToPlayer:
                if (IsCharacterWithinRange(detectionRange))
                {
                    if (IsCharacterWithinRange(chargeRange))
                    {
                        //TODO: Check that there are no obstacle between character and target. Go to prepare charge.
                        currentState = State.PrepareCharge;
                    }
                    else
                    {

                        //TODO: Use pathfinding to move to target
                    }
                }
                else
                {
                    currentState = State.Idle;
                }
                break;
            case State.PrepareCharge:
                //Short timer and animation before charge


                //Set endpos for charge

                //Go to charge
                break;
            case State.Charge:
                //Move a certain distance quickly towards a set position, then attack in direction which it moved
                //Go to attack
                break;
            case State.Attack:
                //Attack in the direction the character dashed
                //Go to run away from player
                break;
            case State.RunAwayFromPlayer:
                //Move away from the player for x seconds
                //Go to idle
                break;
            case State.Dying:
                //Dead state, can not go to other states
                break;
        }
    }

    public void Die()
    {
        currentState = State.Dying;
    }
}

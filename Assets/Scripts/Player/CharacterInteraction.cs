using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{

    float xDirection;
    float yDirection;


    const float COOLDOWNTIME = 0.4f;
    float cooldown;

    public GameObject weapon;
    CharacterMovement myMovement;


    void Start()
    {
        myMovement = GetComponent<CharacterMovement>(); 
    }
    void Update()
    {
        xDirection = Input.GetAxis("HorizontalSecondary");
        yDirection = Input.GetAxis("VerticalSecondary");

        if (xDirection != 0 || yDirection != 0)
        {
            BasicAttack(new AttackDirection(GetAttackDirection()), weapon);
        }
    }

    Direction GetAttackDirection()
    {
        if (xDirection < 0)
        {
            return Direction.Left;
        }
        if (xDirection > 0)
        {
            return Direction.Right;
        }
        if (yDirection < 0)
        {
            return Direction.Down;
        }
        if (yDirection > 0)
        {
            return Direction.Up;
        }
        return Direction.Down;
    }

    void BasicAttack(AttackDirection attackDirection, GameObject ability)
    {   
        Instantiate(ability, transform.position + attackDirection.direction, attackDirection.rotation);
        cooldown = COOLDOWNTIME;
    }
}

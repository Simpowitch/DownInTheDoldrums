using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Direction facing;

    public Sprite attackSprite;

    public CharacterData myStats;
    float xMovement;
    float yMovement;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck();

        transform.position += new Vector3(xMovement, yMovement, 0) * Time.deltaTime * myStats.movementSpeed;
    }
    void InputCheck()
    {
        xMovement = Input.GetAxis("Horizontal");
        yMovement = Input.GetAxis("Vertical");

        facing = Movement();

        if (Input.GetAxis("Fire1") != 0)
        {
            Attack();
        }
    }


    Direction Movement()
    {
        if (xMovement < 0)
        {
            return Direction.Left;
        }
        if (xMovement > 0)
        {
            return Direction.Right;
        }
        if (yMovement < 0)
        {
            return Direction.Down;
        }
        if (yMovement > 0)
        {
            return Direction.Up;
        }
        return facing;
    }


    public void Attack()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Direction facing;

    public Sprite attackSprite;

    public CharacterData myStats;

    Vector2 noramlizedMovement;
    float xMovement;
    float yMovement;

    public Rigidbody2D myRigidbody;



    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck();
    }

    private void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + noramlizedMovement * myStats.movementSpeed * Time.fixedDeltaTime);
    }
    void InputCheck()
    {
        xMovement = Input.GetAxis("Horizontal");
        yMovement = Input.GetAxis("Vertical");

        noramlizedMovement = new Vector2(xMovement, yMovement);

        if (noramlizedMovement.magnitude > 1)
        {
            noramlizedMovement /= noramlizedMovement.magnitude;
        }

        //float normalizedLength = (xMovement + yMovement) / 2;
        //noramlizedMovement *= Mathf.Abs(normalizedLength);

        facing = Movement();
        print(facing);
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
        switch (facing)
        {
            case Direction.Left:
                
                break;
            case Direction.Right:
                break;
            case Direction.Up:
                break;
            case Direction.Down:
                break;
            default:
                break;
        }
    }
}

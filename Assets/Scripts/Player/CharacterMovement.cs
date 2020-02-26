using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Direction facing;

    public Sprite attackSprite;
    Rigidbody2D myRigidbody;

    CharacterData myStats;

    Vector2 noramlizedMovement;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myStats = GetComponent<CharacterData>();
    }

    private void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + noramlizedMovement * myStats.movementSpeed * Time.fixedDeltaTime);
    }
    public void InputCheck(float xMovement, float yMovement)
    {
        noramlizedMovement = new Vector2(xMovement, yMovement);

        if (noramlizedMovement.magnitude > 1)
        {
            noramlizedMovement /= noramlizedMovement.magnitude;
        }
        facing = Utility.GetDirection(xMovement, yMovement);
    }
}

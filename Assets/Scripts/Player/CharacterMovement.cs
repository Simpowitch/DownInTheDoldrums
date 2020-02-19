using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Direction facing;

    public Sprite attackSprite;
    Rigidbody2D myRigidbody;

    CharacterData myStats;

    float xMovement;
    float yMovement;
    Vector2 noramlizedMovement;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myStats = GetComponent<CharacterData>();
    }

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
        facing = Utility.GetDirection(xMovement, yMovement);
    }
}

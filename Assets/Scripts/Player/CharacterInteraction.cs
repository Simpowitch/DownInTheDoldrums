using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
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
        print(cooldown);
        if (cooldown <= 0)
        {
            if (Input.GetAxis("Fire1") != 0)
            {
                switch (myMovement.facing)
                {
                    case Direction.Left:
                        Instantiate(weapon, transform.position + Vector3.left * 0.5f, Quaternion.Euler(0, 0, 180));
                        break;
                    case Direction.Right:
                        Instantiate(weapon, transform.position + Vector3.right * 0.5f, Quaternion.Euler(0, 0, 0));
                        break;
                    case Direction.Up:
                        Instantiate(weapon, transform.position + Vector3.up * 0.8f, Quaternion.Euler(0, 0, 90));
                        break;
                    case Direction.Down:
                        Instantiate(weapon, transform.position + Vector3.down * 0.8f, Quaternion.Euler(0, 0, 270));
                        break;
                    default:
                        break;
                }
                cooldown = COOLDOWNTIME;
            }
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }
}

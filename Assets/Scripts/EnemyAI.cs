using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State { Idle, Move, Attack}
    State currentState = State.Idle;

    Transform player = null;

    [SerializeField] float movementSpeed = 0.5f;
    [SerializeField] float attackDistance = 0.75f;
    [SerializeField] float detectionRange = 10f;

     [SerializeField] float attackCooldown = 3;
     float attackCooldownTimer = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        currentState = FSM(currentState);
    }

    private State FSM(State inputState)
    {
        CooldownTimers();

        switch (inputState)
        {
            case State.Idle:
                if (LookForPlayer())
                {
                    return State.Move;
                }
                break;
            case State.Move:
                if (CheckIfInAttackRange())
                {
                    return State.Attack;
                }
                MoveToPlayer();
                break;
            case State.Attack:
                if (!CheckIfInAttackRange())
                {
                    return State.Move;
                }
                Attack();
                break;
        }
        return inputState;
    }

    private bool LookForPlayer()
    {
        return (Vector2.Distance(this.transform.position, player.transform.position) < detectionRange);
    }

    private void MoveToPlayer()
    {
        //Find target
        Vector3 target = player.transform.position;

        //Find direction
        Vector3 moveDir = (target - transform.position).normalized;

        //Move
        this.transform.position += (moveDir * movementSpeed * Time.deltaTime);
    }

    private bool CheckIfInAttackRange()
    {
        return (Vector2.Distance(this.transform.position, player.transform.position) < attackDistance);
    }

    private void Attack()
    {
        if (attackCooldownTimer <= 0)
        {
            //Attack player
            Debug.Log("Player attacked");
            attackCooldownTimer = attackCooldown;
        }
    }

    private void CooldownTimers()
    {
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
    }
}

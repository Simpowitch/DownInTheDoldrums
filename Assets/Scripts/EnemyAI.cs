using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteAnimation))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    private enum State { Idle, Move, Attack}
    State currentState = State.Idle;

    GameObject playerObject = null;
    SpriteAnimation characterVisualUpdater;
    Rigidbody2D rb;

    [SerializeField] float movementSpeed = 1;
    [SerializeField] float attackDistance = 0.75f;
    [SerializeField] float detectionRange = 10f;

    [SerializeField] float attackCooldown = 3;
    float attackCooldownTimer = 0;

    [SerializeField] int damagePerAttack = 1;
    [SerializeField] int hp = 10;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        characterVisualUpdater = GetComponent<SpriteAnimation>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject == null)
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
                characterVisualUpdater.SetIsWalking(false);
                break;
            case State.Move:
                if (CheckIfInAttackRange())
                {
                    return State.Attack;
                }
                characterVisualUpdater.SetIsWalking(true);
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
        return (Vector2.Distance(this.transform.position, playerObject.transform.position) < detectionRange);
    }

    private void MoveToPlayer()
    {
        //Find target
        Vector3 target = playerObject.transform.position;

        //Find direction
        Vector2 moveDir = (target - transform.position).normalized;

        //Move
        rb.MovePosition(new Vector2(this.transform.position.x, this.transform.position.y) + (moveDir * movementSpeed * Time.deltaTime));


        //Update sprite direction
        float deltaX = moveDir.x;
        float deltaY = moveDir.y;

        if (deltaX * deltaX > deltaY * deltaY)
        {
            if (deltaX >= 0.01f)
            {
                characterVisualUpdater.SetDirection(Direction.Right);
                characterVisualUpdater.SetIsWalking(true);
            }
            if (deltaX <= -0.01f)
            {
                characterVisualUpdater.SetDirection(Direction.Left);
                characterVisualUpdater.SetIsWalking(true);
            }
        }
        else
        {
            if (deltaY >= 0.01f)
            {
                characterVisualUpdater.SetDirection(Direction.Up);
                characterVisualUpdater.SetIsWalking(true);
            }
            if (deltaY <= -0.01f)
            {
                characterVisualUpdater.SetDirection(Direction.Down);
                characterVisualUpdater.SetIsWalking(true);
            }
        }
    }

    private bool CheckIfInAttackRange()
    {
        return (Vector2.Distance(this.transform.position, playerObject.transform.position) < attackDistance);
    }

    private void Attack()
    {
        if (attackCooldownTimer <= 0)
        {
            //Attack player
            Debug.Log("Player attacked");
            playerObject.GetComponent<CharacterData>().TakeDamage(damagePerAttack);
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

    public IEnumerator SlowDown(float duration, float slowFactor)
    {
        movementSpeed *= (1 - slowFactor);
        yield return new WaitForSeconds(duration);
        movementSpeed /= (1 - slowFactor);
    }

    public void TakeDamage(int damageIn)
    {
        hp -= damageIn;

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject.Destroy(this.gameObject);
    }
}

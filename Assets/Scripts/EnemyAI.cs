using UnityEngine;

[RequireComponent(typeof(SpriteAnimation))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    private enum State { Idle, Move, Attack }
    State currentState = State.Idle;

    GameObject playerObject = null;
    SpriteAnimation characterVisualUpdater;
    Rigidbody2D rigidBody;

    float attackRange = 0;
    [SerializeField] float minimumAttackRange = 0.5f;
    [SerializeField] float detectionRange = 10f;
    CharacterData characterData;
    CharacterWeaponSelect weaponSelector;

    private void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");

        characterData = GetComponent<CharacterData>();
        characterVisualUpdater = GetComponent<SpriteAnimation>();
        rigidBody = GetComponent<Rigidbody2D>();
        weaponSelector = GetComponent<CharacterWeaponSelect>();
    }


    private void Start()
    {
        ChangeWeapon(0); //Enemy start with weapon slot one as default
    }

    private void CalculateAttackRange()
    {
        attackRange = Mathf.Max(minimumAttackRange, characterData.selectedWeaponHolder.myWeapon.GetExpectedRange());
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
                if (IsInAttackRange())
                {
                    return State.Attack;
                }
                characterVisualUpdater.SetIsWalking(true);
                MoveToPlayer();
                break;
            case State.Attack:
                if (!IsInAttackRange())
                {
                    return State.Move;
                }
                Attack(playerObject);
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
        rigidBody.MovePosition(new Vector2(this.transform.position.x, this.transform.position.y) + (moveDir * characterData.movementSpeed * Time.deltaTime));

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

    private bool IsInAttackRange()
    {
        return (Vector2.Distance(this.transform.position, playerObject.transform.position) < attackRange);
    }

    private void Attack(GameObject target)
    {
        characterData.selectedWeaponHolder.Attack(new RotationDirection(GetDirectionToTarget(target.transform.position)), "Enemy");
    }

    private Direction GetDirectionToTarget(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        return Utility.GetDirection(dir.x, dir.y);
    }
    //Remove?!?!?!?
    private void Die()
    {
        GameObject.Destroy(this.gameObject);
    }
}

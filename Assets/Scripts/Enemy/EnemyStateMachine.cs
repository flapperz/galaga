using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State : int
{
    MovingToGrid,
    AtGrid,
    MovingToAttack,
    Attack,
    WaitAttackFinish
}

public class EnemyStateMachine : MonoBehaviour
{
    private State state;
    public State State { get { return state; } set { state = value; } }

    [SerializeField]
    private float speed;

    [SerializeField]
    private IEnemyAttack enemyAttack;

    private Vector2 attackTarget;

    private EnemyManager enemyManager;
    private Rigidbody2D rigidbody;
    

    public void SetAttack()
    {
        state = State.MovingToAttack;
        attackTarget = enemyAttack.GetAttackPosition();
    }

    public void SetFinishAttack()
    {
        state = State.MovingToGrid;
    }

    void Awake()
    {
        enemyManager = EnemyManager.Instance;
        rigidbody = GetComponent<Rigidbody2D>();
        enemyAttack = GetComponent<IEnemyAttack>();

        state = State.MovingToGrid;
    }

    void Update()
    {
        switch (state)
        {
            case State.MovingToGrid:
                MovingToGrid();
                break;

            case State.AtGrid:
                AtGrid();
                break;

            case State.MovingToAttack:
                MovingToAttack();
                break;

            case State.Attack:
                Attack();
                break;
        }
    }

    void MovingToGrid()
    {
        Vector2 target = enemyManager.GetGridPosition(gameObject);
        rigidbody.position = Vector2.MoveTowards(rigidbody.position, target, speed * Time.deltaTime);

        if (rigidbody.position == target)
        {
            rigidbody.velocity = Vector2.zero;
            state = State.AtGrid;
        }
    }

    void AtGrid()
    {
        rigidbody.position = enemyManager.GetGridPosition(gameObject);
    }

    void MovingToAttack()
    {
        rigidbody.position = rigidbody.position = Vector2.MoveTowards(rigidbody.position, attackTarget, speed * Time.deltaTime);

        if (rigidbody.position == attackTarget)
        {
            rigidbody.velocity = Vector2.zero;
            state = State.Attack;
        }
    }

    void Attack()
    {
        enemyAttack.Attack();
        Invoke("FinishAttack", enemyAttack.GetAttackDelay());
        state = State.WaitAttackFinish;
    }

    void FinishAttack()
    {
        state = State.MovingToGrid;
    }


    void OnDestroy()
    {
        enemyManager.Enemy2Coord.Remove(gameObject);
    }
}

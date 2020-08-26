using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameCharacter : MonoBehaviour
{
    // This is the parent class of all characters in the game
    [Header("Gameplay Attributes")]
    public int health;
    public int armor;
    public float moveSpeed;
    public float attackSpeed = 0.5f;
    public float attackTimer;
    public Vector2 moveDirection;
    public Vector2 faceDirection;

    public Rigidbody2D rbRef;

    // Action state machine - a little redundant in this use
    public delegate void CharacterAction();
    public CharacterAction action;

    public Transform projectilesParent;

    public virtual void Awake()
    {
        rbRef = gameObject.GetComponent<Rigidbody2D>();
        action = ReadInput;
        projectilesParent = GameObject.FindGameObjectWithTag("ProjectileManager").transform;
    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        if (attackTimer < attackSpeed)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > attackSpeed)
            {
                attackTimer = attackSpeed;
            }
        }


        action();
    }

    // Delegate actions
    public virtual void Move()
    {
        // 2
        if (moveDirection != Vector2.zero)
        {
            faceDirection = moveDirection;
        }
        rbRef.MovePosition(rbRef.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        action = ReadInput;
    }
    public virtual void Attack()
    {

    }
    public virtual void ReadInput()
    {
        // 1
        action = Move;

    }
    // end delegated actions

    public virtual void TakeDamage(int damageToTake)
    {
        if (damageToTake > armor)
        {
            health -= (damageToTake - armor);
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public virtual void TakeDamage(int damageToTake, Players source)
    {

    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }


}

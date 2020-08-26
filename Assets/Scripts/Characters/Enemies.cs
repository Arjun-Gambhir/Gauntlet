using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : GameCharacter
{
    public int scoreValue;
    public int damage;
    public float attackRange;
    public GameObject[] allPlayers;
    public GameObject closestPlayer;
    public float distanceToClosestPlayer;

    public GameObject myGenerator;

    public bool doesDieOnHit = false;
    public bool useProjectile = false;
    public GameObject projectilePrefab = null;

    public override void Awake()
    {
        base.Awake();
        allPlayers = GameObject.FindGameObjectsWithTag("Player");
    }

    public override void Update()
    {
        MoveTowardsPlayer();
        base.Update();
    }

    // find the cloest player
    public GameObject findClosestPlayer()
    {
        closestPlayer = allPlayers[0];
        distanceToClosestPlayer = Vector2.Distance(transform.position, allPlayers[0].transform.position);
        for (int i = 0; i < allPlayers.Length; i++)
        {
            if (Vector2.Distance(transform.position, allPlayers[i].transform.position) < distanceToClosestPlayer) // can flip sign to change order of priority on players at same distance
            {
                closestPlayer = allPlayers[i];
            }
        }
        distanceToClosestPlayer = Vector2.Distance(transform.position, closestPlayer.transform.position);
        return closestPlayer;
    }

    // sets the direction to be the closest player
    public void MoveTowardsPlayer()
    {
        Vector2 direction;
        direction = findClosestPlayer().transform.position - this.gameObject.transform.position;
        direction = direction.normalized;
        SetMoveDirection(direction);
    }

    public override void Die()
    {
        // not copying base (deactivate)
        if (myGenerator != null)
        {
            myGenerator.GetComponent<Generators>().children.Remove(this.gameObject);
        }
        Destroy(this.gameObject);

    }
    // overloaded so it gives points to whatever kills it
    public override void TakeDamage(int damageToTake, Players source)
    {
        if (damageToTake > armor)
        {
            health -= (damageToTake - armor);
            if (health <= 0)
            {
                source.AddScore(scoreValue);
                Die();
            }
        }
    }

    // checks if the player is within range of being hit
    public bool isPlayerWithinAttackRange()
    {
        if (distanceToClosestPlayer <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // if the player is in range, attack. Else, move towards them.
    public override void ReadInput()
    {
        if (isPlayerWithinAttackRange())
        {
            Attack();
        }
        else
        {
            base.ReadInput();
        }
    }


    public override void Attack()
    {
        if (useProjectile)
        {
            if (attackTimer == attackSpeed)
            {
                Vector2 direction = findClosestPlayer().transform.position - this.gameObject.transform.position;
                direction = direction.normalized;
                GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, projectilesParent);
                newProjectile.GetComponent<Projectiles>().moveDirection = direction;
                newProjectile.GetComponent<Projectiles>();

                attackTimer = 0;
            }
        }
        else
        {
            if (attackTimer >= attackSpeed)
            {
                closestPlayer.GetComponent<Players>().TakeDamage(damage);
                attackTimer = 0;
                if (doesDieOnHit)
                {
                    Destroy(this.gameObject);
                }
            }
        }

    }
}

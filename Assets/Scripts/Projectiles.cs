using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    // checks if it collides with walls, checks if it is owned by the player or by an enemy

    public Rigidbody2D rbRef;
    public bool doesCollide = false;
    public int damage;
    public float moveSpeed;
    public Players myPlayer = null;
    public Vector2 moveDirection;
    public bool playerProjectile;

    private void Awake()
    {
        rbRef = gameObject.GetComponent<Rigidbody2D>();
        doesCollide = !gameObject.GetComponent<Collider2D>().isTrigger;
        
    }

    private void Start()
    {
        rbRef.velocity = moveDirection * moveSpeed;
        Invoke("TimeOut", 8f);
    }

    // used if collider is trigger, can go over walls
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!doesCollide)
        {
            if (collision.CompareTag("Enemy"))
            {
                if (playerProjectile)
                {
                    collision.GetComponent<Enemies>().TakeDamage(damage);
                    myPlayer.AddScore(collision.GetComponent<Enemies>().scoreValue);
                    Destroy(this.gameObject);
                }
            }
            if (collision.CompareTag("Generator"))
            {
                if (playerProjectile)
                {
                    myPlayer.AddScore(100);
                    Destroy(collision.gameObject);
                    Destroy(this.gameObject);
                }
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                if (!playerProjectile)
                {
                    collision.gameObject.GetComponent<Players>().TakeDamage(damage);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    // used if collider is not static, goes over walls
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (doesCollide)
        {
            if (playerProjectile)
            {
                if (collision.gameObject.CompareTag("Enemy"))
                {
                 collision.gameObject.GetComponent<Enemies>().TakeDamage(damage);
                 myPlayer.AddScore(collision.gameObject.GetComponent<Enemies>().scoreValue);
                 Destroy(this.gameObject);
                }
            }
        }
        if (collision.gameObject.CompareTag("Generator"))
        {
            if (playerProjectile)
            {
                myPlayer.AddScore(100);
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!playerProjectile)
            {
                collision.gameObject.GetComponent<Players>().TakeDamage(damage);
                Destroy(this.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Untagged"))
        {
            Destroy(this.gameObject);
        }
    }

    // projectiles will automatically time out if nothing destroys them
    public void TimeOut()
    {
        Destroy(this.gameObject);
    }
}

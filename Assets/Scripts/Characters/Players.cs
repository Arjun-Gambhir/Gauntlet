using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : GameCharacter
{
    // parent class of all of the players
    [SerializeField]
    private int playerIndex = 0;
    public int score = 0;
    public int keyCount = 0;
    public int potionCount = 0;
    public GameObject projectile;

    public int potionDamage = 5;

    // player index is for the input managers
    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    // player attack is fire a projectile in the current direction of movement
    public override void Attack()
    {
        base.Attack();

        if (attackTimer == attackSpeed)
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity, projectilesParent);
            newProjectile.GetComponent<Projectiles>().moveDirection = faceDirection;
            newProjectile.GetComponent<Projectiles>().myPlayer = this;

            attackTimer = 0;
        }
    }

    public override void TakeDamage(int damageToTake)
    {
        base.TakeDamage(damageToTake);
        UpdateMyUI();
    }

    // this is called by other objects to add score
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateMyUI();
    }

    // calls singleton UI manager
    public void UpdateMyUI()
    {
        UIManager.instance.UpdateUIText(playerIndex);
    }

    // if we have potions, damage the enemies for 5 damage. Deals 999 if the wizard does it (value set in inspector)
    public void UsePotion()
    {
        if (potionCount > 0)
        {
            potionCount--;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemies>().TakeDamage(potionDamage, this);
            }
            UpdateMyUI();
        }
    }

}

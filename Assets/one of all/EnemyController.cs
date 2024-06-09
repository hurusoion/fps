using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 10; // Maximum health points
    private int currentHealth; // Current health points

    public Animator animator; // Reference to the Animator component for playing animations
    public int attackDamage = 1; // Amount of damage the enemy deals
    public float attackRange = 1f; // Range within which the enemy can attack
    public float attackInterval = 1f; // Interval between attacks
    public float moveSpeed = 5f; // Movement speed towards the player during attack
    public float maxMoveDistance = 10f; // Maximum distance the enemy can move towards the player

    private Transform player; // Reference to the player's transform
    private bool canAttack = true; // Flag to control attack cooldown

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maxHealth

        // Find the player GameObject by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player GameObject has the 'Player' tag.");
        }

        // Start the attack routine
        StartCoroutine(AttackRoutine());
    }

    void Update()
    {
        // Move towards the player
        if (canAttack)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0f; // Ignore vertical component

            // Calculate the distance to move based on the move speed and time
            float distanceToMove = moveSpeed * Time.deltaTime;
            // Limit the distance to move to the maximum move distance
            distanceToMove = Mathf.Min(distanceToMove, maxMoveDistance);

            // Move towards the player by the calculated distance
            transform.position += directionToPlayer.normalized * distanceToMove;

            // Flip the sprite based on movement direction
            if (directionToPlayer.x < 0)
            {
                // Enemy is moving left, flip the sprite
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (directionToPlayer.x > 0)
            {
                // Enemy is moving right, restore original scale
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            // Check if the enemy can attack
            if (canAttack)
            {
                Attack(); // Attack the player
                canAttack = false; // Set canAttack to false to prevent further attacks

                // Start the attack cooldown coroutine
                StartCoroutine(AttackCooldownRoutine());
            }
            yield return null;
        }
    }

    // Method to apply damage to the enemy
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Subtract damageAmount from current health

        // Check if the enemy's health points have reached zero
        if (currentHealth <= 0)
        {
            Die(); // Call the Die method to trigger death animation and destroy the enemy GameObject
        }
    }

    // Method to play death animation and destroy the enemy GameObject
    void Die()
    {
        // Play death animation
        if (animator != null)
        {
            animator.SetTrigger("Dead"); // Trigger the "Dead" animation if an Animator component is assigned
        }

        // Destroy the enemy GameObject after a delay
        Destroy(gameObject, 1f); // Adjust the delay time as needed to match the duration of the death animation
    }

    // Method to handle enemy attacks
    void Attack()
    {
        // Play attack animation
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Check if the player is within attack range
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            // Deal damage to the player
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }

    // Coroutine to control the attack cooldown
    IEnumerator AttackCooldownRoutine()
    {
        // Wait for the specified attack interval
        yield return new WaitForSeconds(attackInterval);

        // After the cooldown period, set canAttack to true to allow the enemy to attack again
        canAttack = true;
    }

    // Visualize attack range in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

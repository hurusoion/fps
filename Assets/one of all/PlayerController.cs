using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDist;

    public LayerMask terrainLayer;
    public Animator animator;
    public Rigidbody rb;
    public SpriteRenderer sr;

    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public int attackDamage = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movement
        Move();

        // Attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Move()
    {
        // Horizontal movement (X-axis)
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 horizontalMovement = new Vector3(horizontalInput, 0, 0) * speed;

        // Vertical movement (Z-axis)
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 verticalMovement = new Vector3(0, 0, verticalInput) * speed;

        // Combine horizontal and vertical movement
        Vector3 movement = horizontalMovement + verticalMovement;

        // Apply movement to the Rigidbody
        rb.velocity = movement;

        // Update Animator parameters based on movement
        if (Mathf.Abs(verticalInput) > 0)
        {
            animator.SetFloat("Speed", 0f); // Stop horizontal movement animation
            animator.SetFloat("VerticalSpeed", Mathf.Abs(verticalInput)); // Set vertical movement speed parameter

            if (verticalInput > 0)
            {
                animator.SetFloat("PositiveVerticalSpeed", Mathf.Abs(verticalInput)); // Set positive vertical movement speed parameter
                animator.SetFloat("NegativeVerticalSpeed", 0f); // Stop negative vertical movement animation
            }
            else
            {
                animator.SetFloat("PositiveVerticalSpeed", 0f); // Stop positive vertical movement animation
                animator.SetFloat("NegativeVerticalSpeed", Mathf.Abs(verticalInput)); // Set negative vertical movement speed parameter
            }
        }
        else
        {
            animator.SetFloat("Speed", horizontalMovement.magnitude); // Set horizontal movement speed parameter
            animator.SetFloat("VerticalSpeed", 0f); // Stop vertical movement animation
            animator.SetFloat("PositiveVerticalSpeed", 0f); // Stop positive vertical movement animation
            animator.SetFloat("NegativeVerticalSpeed", 0f); // Stop negative vertical movement animation
        }

        // Flip character sprite if moving horizontally
        if (horizontalInput != 0)
        {
            sr.flipX = (horizontalInput < 0);
        }
    }

    void Attack()
    {
        // Trigger attack animation
        animator.SetTrigger("Attack");

        // Check for enemies within attack range
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        // Deal damage to each enemy within attack range
        foreach (Collider enemy in hitEnemies)
        {
            // Check if the collider belongs to an enemy
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                // Deal damage to the enemy
                enemyController.TakeDamage(attackDamage);
            }
        }
    }

    // Visualize attack range in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public float speed = 10f;

    void Update()
    {
        // Move the bullet horizontally
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a target
        if (collision.gameObject.CompareTag("Target"))
        {
            // Apply damage to the target if needed

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}

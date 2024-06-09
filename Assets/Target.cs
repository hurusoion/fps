using UnityEngine;
public class Target : MonoBehaviour
{
    public float health = 50f;  // amount of health the target starts with
    public void TakeDamage(float amount)
    {
        health -= amount;  // reduce health by the damage amount
        if (health <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);  // destroy the target object
    }
}
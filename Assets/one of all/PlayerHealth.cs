using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10; 
    private int currentHealth; 
    public Animator animator;
    void Start()
    {
        currentHealth = maxHealth; 
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; 

  
        if (currentHealth <= 0)
        {
            Die(); 
        }
    }
    void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Dead"); 
        }
    }
}

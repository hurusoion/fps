using UnityEngine;

public class Resource : MonoBehaviour
{
    public float amount;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            playerStats.EatFood(amount);
            Destroy(gameObject);
        }
    }
}

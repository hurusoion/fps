using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float hunger = 100f;
    private float hungerDecreaseRate = 1f;

    void Update()
    {
        hunger -= hungerDecreaseRate * Time.deltaTime;
        if (hunger <= 0)
        {
            // Handle player death or critical state due to starvation
            Debug.Log("Player has starved");
        }
    }

    public void EatFood(float amount)
    {
        hunger += amount;
        if (hunger > 100)
            hunger = 100;
    }
}

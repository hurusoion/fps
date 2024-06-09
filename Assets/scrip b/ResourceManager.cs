
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int food = 100;
    public int water = 100;

    public void CollectResource(string resourceType, int amount)
    {
        switch (resourceType)
        {
            case "Food":
                food += amount;
                break;
            case "Water":
                water += amount;
                break;
        }
        Debug.Log($"{amount} {resourceType} collected.");
    }

    public void ConsumeResource(string resourceType, int amount)
    {
        switch (resourceType)
        {
            case "Food":
                food -= amount;
                if (food < 0) food = 0;
                break;
            case "Water":
                water -= amount;
                if (water < 0) water = 0;
                break;
        }
        Debug.Log($"{amount} {resourceType} consumed.");
    }
}

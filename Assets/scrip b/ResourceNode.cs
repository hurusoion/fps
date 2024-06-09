
using UnityEngine;

public class ResourceNode : MonoBehaviour, Interactable
{
    public string resourceType;
    public int resourceAmount;

    public void Interact()
    {
        FindObjectOfType<ResourceManager>().CollectResource(resourceType, resourceAmount);
        Destroy(gameObject); // Remove the resource node after collection
    }
}

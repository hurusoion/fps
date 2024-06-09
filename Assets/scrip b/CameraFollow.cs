
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player・s transform
    public Vector3 offset; // Offset position from the player

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned in the CameraFollow script.");
            return;
        }

        // Set an initial offset if one is not provided
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        // Update the camera position to follow the player with the offset
        transform.position = player.position + offset;
    }
}

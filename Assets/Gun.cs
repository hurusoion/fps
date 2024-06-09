using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float Damage = 10f; // The damage inflicted by the bullet
    public float Range = 100f; // The distance the bullet can travel
    public Camera FPS_Cam; // The first-person view camera
    public Image MuzzleFlashImage; // The flash created on shooting using an image
    public Image ImpactEffectImage; // The impact effect created on hitting an object using an image
    public float FireRate = 1f; // The number of bullets shot per second
    private float _timeToFire = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= _timeToFire) // Input for the fire control
        {
            _timeToFire = Time.time + 1f / FireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        StartCoroutine(ShowMuzzleFlash()); // Play the muzzle flash effect
        RaycastHit hit; // Information about what was hit

        // Shoot a ray from the camera in the forward direction
        if (Physics.Raycast(FPS_Cam.transform.position, FPS_Cam.transform.forward, out hit, Range))
        {
            Debug.Log(hit.transform.name); // Log the name of the object hit

            // Check for a target script on the object hit
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(Damage); // If a target script exists, call the TakeDamage function
            }

            // Show the impact effect at the hit location
            StartCoroutine(ShowImpactEffect(hit.point));

            // If the hit object has a Rigidbody component
            if (hit.rigidbody != null)
            {
                // Calculate the force vector with direction and magnitude
                Vector3 force = hit.normal * -10f;
                // Add force to target object at the hit point
                hit.rigidbody.AddForceAtPosition(force, hit.point);
            }
        }
    }

    private IEnumerator ShowMuzzleFlash()
    {
        MuzzleFlashImage.enabled = true; // Show the muzzle flash image
        yield return new WaitForSeconds(0.1f); // Wait for a short duration
        MuzzleFlashImage.enabled = false; // Hide the muzzle flash image
    }

    private IEnumerator ShowImpactEffect(Vector3 position)
    {
        // Position the impact effect image at the hit point
        ImpactEffectImage.transform.position = position;
        ImpactEffectImage.enabled = true; // Show the impact effect image
        yield return new WaitForSeconds(0.5f); // Wait for a short duration
        ImpactEffectImage.enabled = false; // Hide the impact effect image
    }
}

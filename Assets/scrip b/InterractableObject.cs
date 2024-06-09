using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class Interaction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);

        if (other.CompareTag("Enemy"))
        {
            GameManager.instance.PrepareCombat();
        }
        if (other.CompareTag("Chest"))
        {
            GameManager.instance.OpenChest();
        }
    }
}

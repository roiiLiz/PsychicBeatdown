using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCleaner : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ThrownObjects") || other.gameObject.layer == LayerMask.NameToLayer("Projectiles"))
        {
            if (other.GetComponent<HealthComponent>() != null)
            {
                other.GetComponent<HealthComponent>().Die();
            } else
            {
                Destroy(other.gameObject);
            }
        } else
        {
            return;
        }
    }
}

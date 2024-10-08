using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Time after which the bullet should be destroyed (optional)
    public float lifeTime = 5f;

    void Start()
    {
        // Automatically destroy the bullet after a certain lifetime
        Destroy(gameObject, lifeTime);
    }

    // This will be called when the bullet collides with something
    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the bullet when it collides with any object
        Destroy(gameObject);
    }

    // Alternatively, if you are using triggers (for example, if the bullet's collider is set as "Is Trigger")
    private void OnTriggerEnter(Collider other)
    {
        // Destroy the bullet when it enters a trigger collider
        Destroy(gameObject);
    }
}

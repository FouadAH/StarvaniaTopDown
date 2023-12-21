using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public LayerMask damageableMask;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up *  projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageableMask.Contains(collision.gameObject.layer))
        {
            Debug.Log("Damage");
            Destroy(gameObject);
        }    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damageAmount;
    public float projectileSpeed;
    public LayerMask damageableMask;
    public LayerMask obstacleMask;

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
            if (collision.GetComponent<IDamageable>() != null)
            {
                collision.GetComponent<IDamageable>().TakeDamage(damageAmount, transform.position);
                Destroy(gameObject);
            }
        }   
        else if (obstacleMask.Contains(collision.gameObject.layer))
        {
            Destroy(gameObject);
        }
    }
}

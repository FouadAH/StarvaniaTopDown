using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damageAmount;
    public LayerMask damageableMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageableMask.Contains(collision.gameObject.layer))
        {
            if (collision.GetComponent<IDamageable>() != null)
            {
                collision.GetComponent<IDamageable>().TakeDamage(damageAmount, transform.parent.position);
            }
        }
    }
}

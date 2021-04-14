using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 5;
    private void OnTriggerEnter(Collider other)
    {
        var enemyHealth = other.GetComponentInParent<EnemyHealth>();
        if (enemyHealth != null)
        {
            Debug.Log("enemy hit for " + damage.ToString());
            enemyHealth.TakingDamage(damage);
        }
        
        Destroy(this.gameObject);
    }
}

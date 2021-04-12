using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackDmg = 3;
    public int attackDmgV2 = 6;
    public bool isBoss = false;
    
    public Collider attackCollider;

    public void Attacking()
    {
        if (attackCollider != null)
        {
            attackCollider.GetComponent<PlayerHealth>().TakeDamage(attackDmg);
        }
    }

    public void AttackingV2()
    {
        if(attackCollider != null && isBoss)
        {
            attackCollider.GetComponent<PlayerHealth>().TakeDamage(attackDmgV2);
        }
    }
}

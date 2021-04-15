using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour
{
    public int health = 20;
    public GameObject ammoDrop;
    public GameObject healthDrop;

    public void TakingDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        //int randomDrop = Random.Range(0, 30);
        //Instantiate(healthDrop);
        Instantiate(ammoDrop, this.transform.position, this.transform.rotation);
        Debug.Log("enemy killed");
        Destroy(this.gameObject);
    }
}

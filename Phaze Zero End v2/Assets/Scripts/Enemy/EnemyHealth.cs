using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

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
        int randomDrop = Random.Range(0, 30);
        if (randomDrop >= 23) Instantiate(healthDrop);
        if (randomDrop >= 10 && randomDrop <= 21) Instantiate(ammoDrop);
        Destroy(this.gameObject);
    }
}

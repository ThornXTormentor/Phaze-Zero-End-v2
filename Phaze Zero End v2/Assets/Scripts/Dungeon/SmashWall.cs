using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashWall : MonoBehaviour
{
    public float hp = 5;
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("PistolBullet") || other.collider.CompareTag("RifleBullet"))
        {
            hp--;
        }
    }

    private void Update()
    {
        if (hp <= 0)
        {
            //Instantiate(smashedWall, transform.position, transform.rotation);
            Destroy(this);
        }
    }
}

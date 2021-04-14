using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrewCannon : MonoBehaviour
{
    public GameObject lazerPrefab;
    public GameObject firePoint;
    public AudioSource audioSource;
    public AudioClip drewSound;

    public Collider smashCollider;

    private GameObject spawnedLazer;
    private bool lazerActive;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Update()
    {
        if (lazerActive)
        {
            UpdateLazer();
        }
    }

    public void TriggerPulled()
    {
        //pistolAnimator.SetTrigger("Fire");
        FireLazer();
        lazerActive = true;
    }

    public void FireLazer()
    {
        spawnedLazer = Instantiate(lazerPrefab, firePoint.transform.position, firePoint.transform.rotation);
        audioSource.PlayOneShot(drewSound);
        
    }

    public void UpdateLazer()
    {
        if (firePoint != null)
        {
            spawnedLazer.transform.position = firePoint.transform.position;
            spawnedLazer.transform.rotation = firePoint.transform.rotation;
        }
    }

    public void DisableLazer()
    {
        Destroy(spawnedLazer);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.XR.Interaction.Toolkit;

public class SemiAutoPistols : MonoBehaviour
{
    public float bulletSpeed = 60;
    public GameObject bulletObj;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip pistolShotAudio;
    public AudioClip triggerClickAudio;
    public AudioClip magazineSlide;

    public Animator pistolAnimator;
    public VisualEffect muzzleFlash;

    public Magazine pistolMagazine;
    public XRBaseInteractor socketInteractor;

    private bool hasSlide = true;

    public void AddMagazine(XRBaseInteractable interactable)
    {
        pistolMagazine = interactable.GetComponent<Magazine>();
        hasSlide = false;
    }

    public void RemoveMagazine(XRBaseInteractable interactable)
    {
        pistolMagazine = null;
        audioSource.PlayOneShot(magazineSlide);
    }

    public void Slide()
    {
        hasSlide = true;
    }
    
    public void Start()
    {
        if (pistolAnimator == null) pistolAnimator = GetComponentInChildren<Animator>();
        if (barrel == null) barrel = transform;

        socketInteractor.onSelectEntered.AddListener(AddMagazine);
        socketInteractor.onSelectExited.AddListener(RemoveMagazine);
    }

    public void TriggerPulled()
    {
        //Debug.Log("TriggerPulled Called");
        if (pistolMagazine && pistolMagazine.numberOfBullets > 0 && hasSlide)
        {
            pistolAnimator.SetTrigger("Fire");
            FirePistol();
        }
        else
        {
            pistolAnimator.SetTrigger("Empty");
            audioSource.PlayOneShot(triggerClickAudio);
        }
    }

    public void FirePistol()
    {
        pistolMagazine.numberOfBullets--;
        GameObject spawnedBullet = Instantiate(bulletObj, barrel.position, barrel.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = bulletSpeed * barrel.forward;
        muzzleFlash.Play();
        audioSource.PlayOneShot(pistolShotAudio);
        Destroy(spawnedBullet, 2);

        if (pistolMagazine.numberOfBullets == 0)
        {
            pistolAnimator.SetTrigger("FireLast");
        }
    }
}

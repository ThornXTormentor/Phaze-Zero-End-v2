using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.XR.Interaction.Toolkit;

public class SemiAutoRifle : MonoBehaviour
{
    public Transform barrelLocation;
    public Magazine rifleMagazine;
    public XRBaseInteractor socketInteractor;
    
    public VisualEffect muzzleFlash;
    public float bulletSpeed = 90;
    public GameObject bulletObj;
    public AudioSource audioSource;
    public AudioClip pistolShotAudio;
    public AudioClip triggerClickAudio;
    public AudioClip magazineSlide;
    
    private bool hasSlide;
    
    // Start is called before the first frame update
    void Start()
    {
        if (barrelLocation == null) barrelLocation = transform;

        socketInteractor.onSelectEntered.AddListener(AddMagazine);
        socketInteractor.onSelectExited.AddListener(RemoveMagazine);
    }

    public void AddMagazine(XRBaseInteractable interactable)
    {
        rifleMagazine = interactable.GetComponent<Magazine>();
        hasSlide = false;
    }

    public void RemoveMagazine(XRBaseInteractable interactable)
    {
        rifleMagazine = null;
    }

    public void Slide()
    {
        hasSlide = true;
    }
    
    public void TriggerPulled()
    {
        //Debug.Log("TriggerPulled Called");
        if (rifleMagazine && rifleMagazine.numberOfBullets > 0 && hasSlide)
        {
            //pistolAnimator.SetTrigger("Fire");
            FireRifle();
        }
        else
        {
            //pistolAnimator.SetTrigger("Empty");
            audioSource.PlayOneShot(triggerClickAudio);
        }
    }

    public void FireRifle()
    {
        rifleMagazine.numberOfBullets--;
        GameObject spawnedBullet = Instantiate(bulletObj, barrelLocation.position, barrelLocation.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = bulletSpeed * barrelLocation.forward;
        muzzleFlash.Play();
        audioSource.PlayOneShot(pistolShotAudio);
        Destroy(spawnedBullet, 2);

        if (rifleMagazine.numberOfBullets == 0)
        {
            //pistolAnimator.SetTrigger("FireLast");
        }
    }
}

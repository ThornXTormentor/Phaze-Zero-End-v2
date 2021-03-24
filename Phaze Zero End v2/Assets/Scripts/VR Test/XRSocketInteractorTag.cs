using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketInteractorTag : XRSocketInteractor
{
    public string targetTag;

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        if (this.CompareTag("SemiAutoPistolMag"))
        {
            targetTag = "SemiAutoPistolMag";
        }
        else if (this.CompareTag("SemiAutoRifleMag"))
        {
            targetTag = "SemiAutoRifleMag";
        }
        
        return base.CanSelect(interactable) && interactable.CompareTag(targetTag);
    }
}

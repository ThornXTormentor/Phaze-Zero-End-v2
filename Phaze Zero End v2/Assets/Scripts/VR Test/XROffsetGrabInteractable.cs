using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    private Vector3 initialAttachLocalPosition;
    private Quaternion initialAttachLocalRotation;
    
    void Start()
    {
        if (!attachTransform)
        {
            GameObject grab = new GameObject("Grab Pivot");
            grab.transform.SetParent(transform,false);
            attachTransform = grab.transform;
        }

        initialAttachLocalPosition = attachTransform.localPosition;
        initialAttachLocalRotation = attachTransform.localRotation;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        if (interactor is XRDirectInteractor)
        {
            attachTransform.position = interactor.transform.position;
            attachTransform.rotation = interactor.transform.rotation;
        }
        else
        {
            attachTransform.position = initialAttachLocalPosition;
            attachTransform.rotation = initialAttachLocalRotation;
        }
        base.OnSelectEntered(interactor);
    }
}

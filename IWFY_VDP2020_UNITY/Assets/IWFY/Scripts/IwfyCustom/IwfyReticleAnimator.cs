using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Animates the reticle pointer: the Clickable Object owns a reference to the IwfyReticlePointer
// and when hit with a raycast from the IwfyCameraPointer it bounces back the signal to this script,
// that just animates the reticlePointer.
//
// PointingClickableObject is the boolean value that handles the transitions between animations.
//
// TODO: tweak the timings of the animations so that they feel more natural.

public class IwfyReticleAnimator : MonoBehaviour
{
    public Animator animator;
    public TextMesh tooltip;
    
    void Animate()
    {
        animator.SetBool("PointingClickableObject", true);
    }
    void OnClickableObjectEnter(System.Object[] id)
    {
        Animate();
        // TOOLTIP
        // Assign name received by clickable Object
        tooltip.text = id[0].ToString();
        // Assign colour received by clickable Object
        tooltip.color = (Color) id[1];
    }

    void OnClickableObjectExit()
    {
        //Debug.Log("[CustomReticlePointer.cs] ReticlePointerExited a Clickable Object");
        animator.SetBool("PointingClickableObject", false);
        tooltip.text = "";
    }
}

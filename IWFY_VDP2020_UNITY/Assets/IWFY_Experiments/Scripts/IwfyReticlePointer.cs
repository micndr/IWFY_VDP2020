using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IwfyReticlePointer : MonoBehaviour
{
    public Animator animator;
    void OnClickableObjectEnter()
    {
        Debug.Log("[CustomReticlePointer.cs] ReticlePointerEntered a Clickable Object");
        animator.SetBool("PointingClickableObject", true);
    }

    void OnClickableObjectExit()
    {
        Debug.Log("[CustomReticlePointer.cs] ReticlePointerExited a Clickable Object");
        animator.SetBool("PointingClickableObject", false);
    }
}

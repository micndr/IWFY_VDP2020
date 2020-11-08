using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IwfyReticlePointer : MonoBehaviour
{
    void OnClickableObjectEnter()
    {
        Debug.Log("[CustomReticlePointer.cs] ReticlePointerEntered a Clickable Object");
    }

    void OnClickableObjectExit()
    {
        Debug.Log("[CustomReticlePointer.cs] ReticlePointerExited a Clickable Object");
    }
}

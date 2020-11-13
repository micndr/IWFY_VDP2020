using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All interactive objects should inherit from this:
// OnPointerEnter -> Called when the pointer enters the object
// OnPointerExit -> Called when the pointer exits the object
// OnPointerClick -> Called when the pointer clicks the object

// Receives the messages from the IwfyCameraPointer and bounces the signal back to the IwfyReticlePointer
// (of which it needs to be linked to in the unity editor)

public class IwfyClickableObject : MonoBehaviour
{
    [SerializeField] private GameObject _reticlePointer;
    private string id = "ClickableCube1";
    public void OnPointerEnter()
    {
        //Debug.Log("[ClickableObject.cs] AAAH, a ray has hit me, clickable object.");
        _reticlePointer.SendMessage("OnClickableObjectEnter", id);
    }

    public void OnPointerExit()
    {
        //Debug.Log("[ClickableObject.cs] Bye bye evil ray");
        _reticlePointer.SendMessage("OnClickableObjectExit");
    }

    public void OnPointerClick()
    {
        //Debug.Log("This element has been clicked");
    }
}

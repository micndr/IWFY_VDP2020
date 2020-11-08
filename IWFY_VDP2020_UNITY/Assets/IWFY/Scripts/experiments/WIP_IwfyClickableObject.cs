using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WIP_IwfyClickableObject : MonoBehaviour
{
    [SerializeField] private GameObject _reticlePointer;
    public void OnPointerEnter()
    {
        Debug.Log("[ClickableObject.cs] AAAH, a ray has hit me, clickable object.");
        _reticlePointer.SendMessage("OnClickableObjectEnter");
    }

    public void OnPointerExit()
    {
        Debug.Log("[ClickableObject.cs] Bye bye evil ray");
        _reticlePointer.SendMessage("OnClickableObjectExit");
    }

    public void OnPointerClick()
    {
        Debug.Log("This element has been clicked");
    }
}

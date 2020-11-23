using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// Script that handles the behaviour of the popup.

public class PopupLogic : IwfyClickableObjectNoPopup
{
    // Keep track of wether the pointer is on the popup or on the object which instantiated the popup
    public bool over = false;
    public bool overParent = false;

    public override void Start()
    {
        base.Start();
        
        // By default, the popup disappears after a second if the pointer no longer is on it or on its parent object.
        StartCoroutine(DeathCount());
    }

    public void OnPointerClick()
    {
        // When it is clicked, the popup disappears and notifies its object
        Destroy(gameObject);
        SendMessageUpwards("OnPopupClick");
    }

    public override void OnPointerEnter()
    {
        base.OnPointerEnter(); // Enlarges pointer
        over = true;
    }

    public override void OnPointerExit()
    {
        base.OnPointerExit(); // Collapses pointer
        StartCoroutine(DeathCount());
        over = false;
    }
    
    // FUNC: This coroutine checks if the pointer is on either the popup or the parent object AFTER ONE SECOND it has been called
    //       If not on either one, it destroys the popup
    // CALLED: - when the popup is created
    //         - when the pointer exits the popup
    //         - when the pointer exits the parent
    private IEnumerator DeathCount()
    {
        yield return new WaitForSeconds(1);
        if(!over && !overParent) Destroy(gameObject);
        yield return null;
    }

    // JUST TO KEEP TRACK OF THE POINTER ON THE PARENT OBJECT
    public void OnParentEnter()
    {
        overParent = true;
    }

    public void OnParentExit()
    {
        overParent = false;
        StartCoroutine(DeathCount());
    }

    // UTILITY
    
    // When the popup is destroyed, it notifies the parent that it must set its reference to null (or bad things happen)
    // And behave as if the pointer exited the object (as it disappears before the pointer)
    private void OnDestroy()
    {
        if (transform.parent != null)
        {
            SendMessageUpwards("NullifyInstance");   
        }
    }

    // Setters for the modifiable popup properties
    public void SetMessage(string message)
    {
        gameObject.GetComponentInChildren<Text>().text = message;
    }

    public void SetBackgroundColor(Color color)
    {
        gameObject.GetComponentInChildren<Image>().color = color;
    } 
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Script that handles the behaviour of the popup.

public class PopupLogic : IwfyClickableObjectNoPopup
{

    public float activationRange = 4;
    public bool activated = false;
    public bool over = false;
    public bool overParent = false;
    
    public QuestLock qlock;
    public ItemPickup pickup;

    public static void NAME()
    {

    }

    public override void Start()
    {
        base.Start();
        StartCoroutine(DeathCount());
    }

    public void OnPointerClick()
    {
        if (qlock != null)
        {
            qlock.main.NextState(qlock);
        }

        if (pickup != null)
        {
            pickup.GetItems();
        }
        Destroy(gameObject);
        SendMessageUpwards("OnPopupClick");
    }

    public override void OnPointerEnter()
    {
        base.OnPointerEnter();
        over = true;
    }

    public override void OnPointerExit()
    {
        base.OnPointerExit();
        StartCoroutine(DeathCount());
        over = false;
    }

    public void OnParentEnter()
    {
        Debug.Log("OverParent set to true");
        overParent = true;
    }

    public void OnParentExit()
    {
        Debug.Log("OverParent set to False");
        overParent = false;
        StartCoroutine(DeathCount());
    }


IEnumerator DeathCount()
    {
        yield return new WaitForSeconds(1);
        if(!over && !overParent) Destroy(gameObject);
        yield return null;
    }

    private void OnDestroy()
    {
        SendMessageUpwards("NullifyInstance");
    }
}

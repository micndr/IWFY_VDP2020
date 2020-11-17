using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// Script that handles the behaviour of the popup.

public class PopupLogic : IwfyClickableObjectNoPopup
{

    public float activationRange = 4;
    public bool activated = false;
    public bool over = false;
    public bool overParent = false;
    
    public QuestLock qlock;
    public ItemPickup pickup;

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
        overParent = true;
    }

    public void OnParentExit()
    {
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

    public void SetMessage(string message)
    {
        gameObject.GetComponentInChildren<Text>().text = message;
    }

    public void SetBackgroundColor(Color color)
    {
        gameObject.GetComponentInChildren<Image>().color = color;
    } 
}

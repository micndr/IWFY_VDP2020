using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

// All interactive objects which need a popup should inherit from this:
// OnPointerEnter -> Called when the pointer enters the object
// OnPointerExit -> Called when the pointer exits the object
// NullifyInstance -> Called when the popup destroys itself. It triggers OnPointerExit and resets the _popupPrefabInstance reference to null
// OnPopupClick -> In case some code relative to the object is needed to be executed here at the popup click

// REMEMBER TO LINK THE POPUP PREFAB

public class IwfyClickableObject : IwfyClickableObjectNoPopup
{
    public GameObject _popupPrefab;
    [SerializeField] private string _popupMessage;
    [SerializeField] private Color _popupBackgroundColor;
    
    private GameObject _popupPrefabInstance;
    //               id = {name, Color}
    [SerializeField] private string _name;
    [SerializeField] private Color _color;
    private object [] id = new object[2];

    // Initializes calling 
    public override void Start()
    {
        base.Start();
        
        // Initializes object specific properties
        id = new object[] {_name, _color};

        _popupPrefab = (GameObject) Resources.Load("PopupPrefab");
        // Retrieve the prefab
        //_popupPrefab = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/IWFY/Prefabs/PopupPrefab.prefab", typeof(GameObject));
    }
    
    public override void OnPointerEnter()
    {
        base.OnPointerEnter(); // Triggers pointer animation.
        base.getReticlePointer().SendMessage("OnClickableObjectEnter", id);
        
        foreach (Transform child in transform)
            DestroyImmediate(child.gameObject);

        if (transform.childCount == 0)
        {
            _popupPrefabInstance = null;
        }
        
        _popupPrefabInstance = Instantiate(_popupPrefab, transform);
        _popupPrefabInstance.GetComponent<PopupLogic>().SetMessage(_popupMessage);
        _popupPrefabInstance.GetComponent<PopupLogic>().SetBackgroundColor(_popupBackgroundColor);
        
        // Notifies the popup not to destroy itself as long as the pointer is on the parent.
        if(_popupPrefabInstance) _popupPrefabInstance?.SendMessage("OnParentEnter");
    }

    public override void OnPointerExit()
    {
        base.OnPointerExit();
        
        // Notifies that the pointer is no longer on the parent, the popup can go destroy itself.
        if(_popupPrefabInstance) _popupPrefabInstance?.SendMessage("OnParentExit");
    }

    public void OnPopupClick()
    {
        // THIS METHOD IS OVERRIDDEN BY ANY OBJECT THAT IMPLEMENTS SOME FUNCTIONALITY LINKED TO THE POPUP
        Debug.Log("This element's popup has been clicked");
    }
}

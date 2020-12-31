using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openNote : MonoBehaviour
{
    [SerializeField] private Image note;

    public void showMessage()
    {
        if (!note.enabled)
        {
            note.enabled = true;
            Debug.Log("show");
        }

        else if (note.enabled)
        {
            Debug.Log("disabilita");
            note.enabled = false;
        }
    }
}

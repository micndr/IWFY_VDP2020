using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openNote : MonoBehaviour
{
    [SerializeField] private Image note;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            note.enabled = true;
            
        }
            
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.U))
            if (note.enabled)
                note.enabled = false;
    }
}

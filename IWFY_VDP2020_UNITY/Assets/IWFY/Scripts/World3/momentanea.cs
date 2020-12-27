using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class momentanea : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            animator.SetBool("attivo", true);
            Debug.Log("è vero");
        }
    }
}

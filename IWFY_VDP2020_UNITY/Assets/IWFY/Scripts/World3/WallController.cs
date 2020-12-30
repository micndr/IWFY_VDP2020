using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
     private bool attivo = false;
    [SerializeField] private GameObject toBeDesrtoyed;

    public void OnTriggerEnter(Collider collider)
    {
        // if (collider.CompareTag("ponte") && attivo)
        if (collider.CompareTag("ponte"))
        {
            Debug.Log("distrutta area del ponte " + this.gameObject);
            Destroy(toBeDesrtoyed.gameObject);
            Debug.Log("distrutta area del ponte " + this.gameObject);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("ponte"))
            attivo = true;
        Debug.Log("fiume bloccato, da " + this.gameObject);
    }
}
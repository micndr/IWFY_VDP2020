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
        if (collider.CompareTag("ponte") && attivo)
        {
            Destroy(toBeDesrtoyed);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("ponte"))
            attivo = true;
    }
}
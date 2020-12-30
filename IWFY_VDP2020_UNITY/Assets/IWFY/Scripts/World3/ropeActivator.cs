using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeActivator : MonoBehaviour
{
    [SerializeField] private GameObject Rope;

    // public void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("ray") && !Rope.activeSelf)
    //         Rope.SetActive(true);
    // }


    public void activateRope()
     {
         Rope.SetActive(true);
     }
}

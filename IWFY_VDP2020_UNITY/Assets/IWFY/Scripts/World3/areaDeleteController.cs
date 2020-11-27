using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaDeleteController : MonoBehaviour
{
    [SerializeField] private GameObject casuale;
    [SerializeField] private GameObject casuale1;
    [SerializeField] private GameObject casuale2;
   public void OnTriggerEnter(Collider other)
   {
       if (other.CompareTag("Player"))
       {
           Destroy(this);
           if (casuale)
               Destroy(casuale);
           if (casuale1)
               Destroy(casuale1);
           if (casuale2)
               Destroy(casuale2);
       }
   }
}

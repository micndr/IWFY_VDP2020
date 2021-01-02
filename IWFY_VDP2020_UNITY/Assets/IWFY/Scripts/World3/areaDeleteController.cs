using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaDeleteController : MonoBehaviour
{
    [SerializeField] private GameObject casuale;
    [SerializeField] private GameObject casuale1;
    [SerializeField] private GameObject casuale2;
    [SerializeField] private GameObject toBeBuild;
   public void OnTriggerEnter(Collider other)
   {
       if (other.CompareTag("Player"))
       {
           Debug.Log("collisione altalena");
           Triggerer triggerer = GetComponent<Triggerer>(); 
           if (triggerer) triggerer.Trigger();
           Destroy(this.gameObject);
           if (casuale)
               Destroy(casuale.gameObject);
           if (casuale1)
               Destroy(casuale1);
           if (casuale2)
               Destroy(casuale2);
       }
   }
}

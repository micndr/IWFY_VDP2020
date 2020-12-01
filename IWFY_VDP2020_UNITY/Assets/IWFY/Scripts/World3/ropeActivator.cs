using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeActivator : MonoBehaviour
{
    [SerializeField] private GameObject Rope;
    
    public void activateRope()
     {
         Rope.SetActive(true);
     }
}

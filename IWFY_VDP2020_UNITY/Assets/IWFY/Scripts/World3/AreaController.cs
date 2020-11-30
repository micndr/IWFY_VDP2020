using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    private bool active = false;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("mirror"))
            if (active == false)
            {
                Debug.Log("Aggiunto");
                active = true;
            }
    }
    
}
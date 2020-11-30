using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("ponte"))
        {
            Destroy(this);
            Debug.Log("area distrutta");
        }
    }
}

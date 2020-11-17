using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public GameObject Eia;
    
    // [SerializeField] private float _xcoord;
    // [SerializeField] private float _ycoord;
    // [SerializeField] private float _zcoord;
    
    
    [SerializeField] private string _newscene;


    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("prova"))
        {
            StartCoroutine(teleport());
            
        }
    }

    IEnumerator teleport()
    {
        yield return new WaitForSeconds(0);
        Application.LoadLevel(_newscene);
    }
}

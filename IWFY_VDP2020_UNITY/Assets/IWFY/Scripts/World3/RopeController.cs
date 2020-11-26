using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField] private GameObject father;
    [SerializeField] private GameObject son;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("prova"))
        {
            Debug.Log("collisione");
            this.transform.SetParent(father.transform);
        }

        if (collider.CompareTag("miniponte"))
        {
            son.transform.SetParent(this.transform);
        }
    }
    
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            son.transform.SetParent(null);
        }

        if (Input.GetButton("Fire2"))
        {
            this.transform.SetParent(null);
        }
        
        if (Input.GetButton("k"))
        {
            this.transform.localScale = this.transform.localScale + new Vector3(0f, 0f, 3f * Time.fixedDeltaTime);
        }

        if (Input.GetButton("h"))
        {
            this.transform.localScale = this.transform.localScale - new Vector3(0f, 0f, 3f * Time.fixedDeltaTime);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    private bool presa = false;
    [SerializeField] private Animator animator;

    public void OnTriggerEnter(Collider collider)
    {
        
        if (collider.CompareTag("ponte"))
        {
            //Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            Debug.Log("dentro primo if");
            // RaycastHit hit;
            // if (Physics.Raycast(ray, out hit, 30f) && presa == false)
            // Debug.Log("dentro secondo if");
                // if (hit.transform.gameObject.CompareTag("ponte"))
                // {
                //     Debug.Log("dentro terzo if");
                //     presa = true;
                    //this.transform.localScale = new Vector3(0f, 0f, 0f);
                    // animator.SetTrigger("amt");
                    animator.SetBool("attivo", true);
                    for (int i = 0; i < 10; i++)
                    // {
                    //     this.transform.localScale = this.transform.localScale - new Vector3(0f, 6f * Time.fixedDeltaTime, 0f);
                    //     if (this.transform.localScale.z < 2)
                    //         break;
                    // }
                    Destroy(this.gameObject);
             //   }
            
        }
        else if (collider.CompareTag("prova"))
            Debug.Log("collisione ok");
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            this.transform.localScale = this.transform.localScale + new Vector3(0f, 6f * Time.fixedDeltaTime, 0f);
        }
        else
            this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
}
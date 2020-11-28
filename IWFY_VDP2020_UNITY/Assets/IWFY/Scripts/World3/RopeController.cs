using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    [SerializeField] private GameObject father;
    [SerializeField] private GameObject son;
    private bool presa = false;
    [SerializeField] private Animator animator;
    
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("ponte"))
        {
            son.transform.SetParent(this.transform);
           
        }
    }
    
    
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            this.transform.SetParent(null);
        }
        
        if (Input.GetMouseButton(0))
        {
            this.transform.localScale = this.transform.localScale + new Vector3(0f, 0f, 3f * Time.fixedDeltaTime);
        }

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 12f) && presa == false)
        {
            if (hit.transform.gameObject.CompareTag("ponte"))
            {
                presa = true;
                animator.SetTrigger("amt");
            }
        }
    }

    public void activateRope()
    {
        this.transform.SetParent(father.transform);
        this.transform.localPosition = new Vector3(0f, -0.1f, 0f);
    }

}

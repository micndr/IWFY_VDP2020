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
            animator.SetTrigger("amt");
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
    }

    public void activateRope()
    {
        this.transform.SetParent(father.transform);
        this.transform.localPosition = new Vector3(0f, -0.1f, 0f);
    }

}

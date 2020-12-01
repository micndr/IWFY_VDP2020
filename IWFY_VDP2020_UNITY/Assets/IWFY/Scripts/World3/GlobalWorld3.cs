using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalWorld3 : MonoBehaviour
{
    private int mirror;
    [SerializeField] private GameObject ropeRepresentation;
    [SerializeField] private GameObject realRope;
    public Triggerer trigger;

    void Start()
    {
        if (ropeRepresentation.activeSelf)
        {
            ropeRepresentation.SetActive(false);

        }
    }

    public int getMirror()
    {
        return mirror;
    }

    public void increaseMirror()
    {
        mirror++;
        if (mirror == 3)
        {
            Debug.Log("corda aggiunta");
            // trigger.Trigger();
            ropeRepresentation.SetActive(true);
            //realRope.SetActive(true);
        }
    }
}
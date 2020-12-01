using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalWorld3 : MonoBehaviour
{
    private int mirror;
    [SerializeField] private GameObject rope;
    public Triggerer trigger;

    void Start()
    {
        if (rope.activeSelf)
        {
            rope.SetActive(false);

        }
    }

    public int getMirror()
    {
        return mirror;
    }

    public void increaseMirror()
    {
        mirror++;
        Debug.Log("aggiunta");
        if (mirror == 3)
        {
            trigger.Trigger();
        }
    }
}
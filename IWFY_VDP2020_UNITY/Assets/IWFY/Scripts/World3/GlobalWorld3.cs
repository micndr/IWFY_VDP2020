using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalWorld3 : MonoBehaviour
{
    public static int mirror;
    [SerializeField] private GameObject rope;

    void Start()
    {
        if (rope.activeSelf)
            rope.SetActive(false);
    }
    
    public static int getMirror()
    {
        return mirror;
    }

    public static void increaseMirror()
    {
        mirror++;
    }

    void Update()
    {
        if (mirror == 3)
        {
            rope.SetActive(true);
        }
            
    }
    
}

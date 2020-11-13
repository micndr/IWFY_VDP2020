using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class color_button : MonoBehaviour
{
    //public GameObject myObject;
    public Color active;
    public Color iactive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnMouseEnter()
    {
        gameObject.GetComponent<Image>().color = active;
    }

    void OnMouseExit()
    {
        gameObject.GetComponent<Image>().color = iactive;
    }

    void OnMouseDown()
    {
        gameObject.GetComponent<Image>().color = iactive;
    }
}

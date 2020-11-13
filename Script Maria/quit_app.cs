using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quit_app : MonoBehaviour
{
    public void OnMouseDown()
    {
        Debug.Log("has quit game");
        Application.Quit();
    }
}

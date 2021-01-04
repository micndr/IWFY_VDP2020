using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteDisappear : MonoBehaviour
{
    public Image image;
    public openNote note;

    bool inhibitFirst = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("chiudi nota");
            //note.setAttivo();
            //Debug.Log(note.getAttivo());
            if (note.getAttivo()) {
                if (inhibitFirst) {
                    inhibitFirst = false;
                } else {
                    if (image.enabled)
                        image.enabled = false;
                    inhibitFirst = true;
                }
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openNote : MonoBehaviour
{
    [SerializeField] private Image note;
    private bool attivo = true;

    public int index = -1;

    public void showMessage()
    {
        if (!note.enabled)
        {
            // setto a 1 la lettera nell'array globale
            GlobalState gs = FindObjectOfType<GlobalState>();
            gs.letters[index] = 1;

            note.enabled = true;
            Debug.Log("show");
        }

        else if (note.enabled)
        {
            Debug.Log("disabilita");
            note.enabled = false;
        }
    }

    public bool getAttivo()
    {
        return note.enabled;
    }

    public void setAttivo()
    {
        attivo = !attivo;
    }
}

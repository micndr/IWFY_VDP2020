using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This handles the behaviour of a single ghiera.
public class GhieraController : IwfyClickableObjectNoPopup
{
    // The number must be the index from zero starting from the innermost ghiera.
    public int ghieraNumber;
    [SerializeField] private GameObject _scrigno;

    // When pointer clicks a ghiera it notifies the scrigno with the number of clicked ghiera.
    public override void OnPointerClick()
    {
        base.OnPointerClick();
        _scrigno.SendMessage("GhieraClicked", ghieraNumber);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhieraController : IwfyClickableObjectNoPopup
{
    // The number must be the index from zero starting from the innermost ghiera.
    public int ghieraNumber;
    [SerializeField] private GameObject _scrigno;

    public override void OnPointerClick()
    {
        base.OnPointerClick();
        _scrigno.SendMessage("GhieraClicked", ghieraNumber);
    }
}

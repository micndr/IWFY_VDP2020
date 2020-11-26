using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSetup : MonoBehaviour
{

    [SerializeField] private GameObject[] flowersToSwitchOn;
    [SerializeField] private GameObject controllerFlower;

    public bool semplified;

    // Start is called before the first frame update
    void Start()
    {
        controllerFlower.SendMessage("SetSimplified", semplified);
        for (int i = 0; i < flowersToSwitchOn.Length; i++)
        {
            flowersToSwitchOn[i].SendMessage("SwitchColor");
        }
    }
}

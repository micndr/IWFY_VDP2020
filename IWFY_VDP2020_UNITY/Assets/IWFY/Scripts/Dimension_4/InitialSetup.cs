using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSetup : MonoBehaviour
{

    [SerializeField] private GameObject[] flowers;
    [SerializeField] private GameObject controllerFlower;
    private bool _initialized;

    public bool semplified;

    // Start is called before the first frame update
    void Start()
    {
        _initialized = false;
    }

    private void Update()
    {
        if (!_initialized)
        {
            _initialized = true;
            controllerFlower.SendMessage("SetSimplified", semplified);

            for (int i = 0; i < flowers.Length; i++)
            {
                flowers[i].SendMessage("SwitchColor");
            }
            
            flowers[20].SendMessage("OnFlowerClick");
            
            flowers[15].SendMessage("SwitchColor");
            flowers[17].SendMessage("SwitchColor");
            flowers[18].SendMessage("SwitchColor");
            flowers[19].SendMessage("SwitchColor");
        
            flowers[20].SendMessage("SwitchColor");
            flowers[22].SendMessage("SwitchColor");
            flowers[23].SendMessage("SwitchColor");
            flowers[24].SendMessage("SwitchColor");
            
            flowers[20].SendMessage("OnFlowerClick");
            
        }
    }
    
}

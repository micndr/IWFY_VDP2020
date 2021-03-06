﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVolume : MonoBehaviour {
    public Triggerer triggerer;

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            triggerer.Trigger();
        }
    }
}

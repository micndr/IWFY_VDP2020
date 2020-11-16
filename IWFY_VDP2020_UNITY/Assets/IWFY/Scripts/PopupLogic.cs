﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupLogic : MonoBehaviour {

    GameObject player;

    public float activationRange = 4;

    public bool activated = false;

    public bool pressed = false;
    public bool over = false;

    void Start() {
        player = GameObject.Find("Player");
    }

    void Update() {
        Vector3 playerDiff = transform.position - player.transform.position;
        if (playerDiff.sqrMagnitude < activationRange * activationRange) {
            activated = true;
        } else { activated = false; }
    }

    public void OnPointerClick() {
        if (activated) {
            pressed = true;
        }
    }
    public void OnPointerEnter() {
        over = true;
    }
    public void OnPointerExit() {
        over = false;
    }
}

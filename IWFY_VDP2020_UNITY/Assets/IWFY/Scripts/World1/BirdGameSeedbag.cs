using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGameSeedbag : MonoBehaviour {

    public int seedtype = 0;
    public Material mat;
    BirdGameMain main;

    void Awake() {
        main = FindObjectOfType<BirdGameMain>();
    }

    public virtual void OnPointerClick() { 
        main.PickupSeed(this);
    }
}
